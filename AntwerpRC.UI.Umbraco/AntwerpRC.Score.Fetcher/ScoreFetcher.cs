using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntwerpRC.DAL;
using AntwerpRC.Score.Fetcher.Helper;
using HtmlAgilityPack;

namespace AntwerpRC.Score.Fetcher
{
    internal class ScoreFetcher
    {
        private readonly BrowserSession browserSession;

        public ScoreFetcher()
        {

            browserSession = new BrowserSession();
            //Force a first load to the correct season, calls after this will go to the correct page
        }

        private void Initialize(string baseUrl = null)
        {
            if (!string.IsNullOrEmpty(baseUrl))
            {
                browserSession.GetDoc(baseUrl);
            }
        }

        public void FetchGamesForSeason(long seasonId, string user)
        {
            using (var context = new DAL.AntwerpRCEntities())
            {
                var season = context.Season.FirstOrDefault(s => s.SeasonId == seasonId && !s.AuditDeleted);
                if (season != null)
                {
                    if (!string.IsNullOrEmpty(season.CompetitionBaseUrl))
                    {
                        Initialize(season.CompetitionBaseUrl);
                    }
                    LoadGamesPerCategory(season.Team.ToList(), context, user);
                }
                else
                {
                    throw new Exception("Season doesn't exist, no games can be fetched");
                }
            }
        }


        private void LoadGamesPerCategory(IEnumerable<DAL.Team> teams, DAL.AntwerpRCEntities context, string user)
        {
            foreach (var team in teams)
            {
                var doc = browserSession.GetDoc(team.CompetitionUrl);

                var teamClubs = team.TeamClub.ToList();

                int take = 1;
                take = take + teamClubs.Count / 2;

                if (teamClubs.Count % 2 > 0)
                    take++;

                var table =
                    doc.DocumentNode.Descendants("table")
                       .FirstOrDefault(
                           t => t.Attributes.Contains("class") && t.Attributes["class"].Value.Contains("clubentrees"));
                if (table != null)
                {
                    int skip = 1;
                    var rows = table.Descendants("tr").Skip(skip).Take(take).ToList();

                    while (rows.Any())
                    {
                        var dateRow = rows[0];
                        DateTime? date = ProcessDateRow(dateRow);
                        if (date.HasValue)
                        {
                            foreach (var row in rows.Skip(1))
                            {
                                Game game = ProcessGameRow(row, date.Value, teamClubs, context, user);
                                if (game != null && game.GameId <= 0)
                                {
                                    //Add game
                                    context.Game.Add(game);
                                }
                            }
                            context.SaveChanges();
                        }
                        skip = skip + take + 1;
                        rows = table.Descendants("tr").Skip(skip).Take(take + 1).ToList();

                    }
                }

            }
        }

        private DateTime? ProcessDateRow(HtmlNode row)
        {
            string dateValue = row.Descendants("td").First().InnerText.CleanHtml().Trim();
            if (!string.IsNullOrEmpty(dateValue))
            {
                string datePart = dateValue.Contains(",") ? dateValue.Substring(0, dateValue.IndexOf(',')) : dateValue;
                if (!string.IsNullOrEmpty(datePart))
                {
                    DateTime result;
                    if (DateTime.TryParse(datePart, out result))
                    {
                        return result;
                    }
                }
            }
            return null;
        }

        private Game ProcessGameRow(HtmlNode row, DateTime date, List<TeamClub> teamClubs, AntwerpRCEntities context, string user)
        {
            Game game = null;
            string gameNumber;
            //Find the game number
            var gameNrNode = row.Descendants("td").FirstOrDefault();
            if (gameNrNode != null && !string.IsNullOrEmpty(gameNrNode.InnerText.CleanHtml().Trim()))
            {
                gameNumber = gameNrNode.InnerText.CleanHtml();
                game = GetGameByGameNumber(gameNumber, context);
            }
            else
            {
                return null;
            }

            if (game == null)
            {
                game = new Game {GameNumber = gameNumber, Date = date, AuditCreatedBy = user, AuditCreatedOn = DateTime.Now};
                //Check the date

                var newDateNode = row.Descendants("td").LastOrDefault();
                if (newDateNode != null && !string.IsNullOrEmpty(newDateNode.InnerText))
                {
                    DateTime newDate;
                    if (DateTime.TryParse(newDateNode.InnerText.CleanHtml(), out newDate))
                        game.Date = newDate;
                }

                //Find the first team
                var team1Node = row.Descendants("td").Skip(1).Take(1).FirstOrDefault();
                if (team1Node != null && !string.IsNullOrEmpty(team1Node.InnerText))
                {
                    var teamClub1 = teamClubs.FirstOrDefault(t => t.Club.ClubName == team1Node.InnerText.CleanHtml().Trim() || t.Club.ClubAlias.Any(a => a.Alias == team1Node.InnerText.CleanHtml().Trim()));
                    if (teamClub1 != null)
                    {
                        game.TeamClub1Id = teamClub1.TeamClubId;
                        game.TeamClub1AddressId = teamClub1.Club.Address.First().AddressId;
                    }
                }

                //find the second team
                var team2Node = row.Descendants("td").Skip(2).Take(1).FirstOrDefault();
                if (team2Node != null && !string.IsNullOrEmpty(team2Node.InnerText))
                {
                    var teamClub2 = teamClubs.FirstOrDefault(t => t.Club.ClubName == team2Node.InnerText.CleanHtml().Trim() || t.Club.ClubAlias.Any(a => a.Alias == team2Node.InnerText.CleanHtml().Trim()));
                    if (teamClub2 != null)
                        game.TeamClub2Id = teamClub2.TeamClubId;
                }
            }

            //Find the score
            var scoreNode = row.Descendants("td").Skip(4).Take(1).FirstOrDefault();
            int team1Score = -999;
            int team2Score = -999;

            if (scoreNode != null && !string.IsNullOrEmpty(scoreNode.InnerText.CleanHtml().Trim()))
            {
                string[] scores = scoreNode.InnerText.CleanHtml().Trim().Split('-');
                if (!int.TryParse(scores[0].Trim(), out team1Score))
                {
                    //Check for FF
                    if (scores[0].Trim().Equals("FF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        team1Score = -1;
                    }
                }
                if (!int.TryParse(scores[1].Trim(), out team2Score))
                {
                    //Check for FF
                    if (scores[1].Trim().Equals("FF", StringComparison.InvariantCultureIgnoreCase))
                    {
                        team2Score = -1;
                    }
                }
            }
            game.TeamClub1Score = team1Score;
            game.TeamClub2Score = team2Score;


            //Find the try count
            int team1Try = 0;
            int team2Try = 0;

            var tryNode = row.Descendants("td").Skip(5).Take(1).FirstOrDefault();
            if (tryNode != null && !string.IsNullOrEmpty(tryNode.InnerText.CleanHtml().Trim()))
            {
                string[] scores = tryNode.InnerText.CleanHtml().Trim().Split('-');
                int.TryParse(scores[0].Trim(), out team1Try);
                int.TryParse(scores[1].Trim(), out team2Try);
            }
            game.TeamClub1Tries = team1Try;
            game.TeamClub2Tries = team2Try;

            return game;
        }

        private Game GetGameByGameNumber(string gameNumber, AntwerpRCEntities context)
        {
            return context.Game.FirstOrDefault(g => g.GameNumber == gameNumber);
        }
    }
}
