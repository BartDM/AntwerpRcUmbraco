using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntwerpRC.DAL;

namespace AntwerpRC.Score.Fetcher
{
    public class Calculator
    {

        public void CalculateTablesForSeason(long seasonId, string user, DateTime calculationDate)
        {
            using (var context = new DAL.AntwerpRCEntities())
            {
                var season = context.Season.FirstOrDefault(s => s.SeasonId == seasonId && !s.AuditDeleted);
                if (season != null)
                {
                    //Fetch all teams for season
                    LoadTableForTeams(season.Team, context, user,calculationDate);
                }
            }
        }

        private void LoadTableForTeams(IEnumerable<DAL.Team> teams, DAL.AntwerpRCEntities context, string user, DateTime calculationDate)
        {
            try
            {
                foreach (var team in teams)
                {
                    var table = new DAL.ScoreTable()
                    {
                        AuditCreatedBy = user,
                        AuditCreatedOn = DateTime.Now,
                        TeamId = team.TeamId,
                        SelfCalculated = true,
                        CreatedOn = calculationDate
                    };
                    //team.ScoreTable.Add(table);
                    //context.SaveChanges();

                    var lines = LoadTableForTeam(team, table, context, user,calculationDate);

                    //Get the last table and compare
                    bool saveNewTable = true;
                    var oldTable =
                        context.ScoreTable.AsNoTracking().OrderByDescending(t => t.CreatedOn)
                            .FirstOrDefault(t => t.TeamId == team.TeamId);
                    if (oldTable != null)
                    {
                        if (table.Equals(oldTable))
                        {
                            saveNewTable = false;
                        }
                    }
                    if (saveNewTable)
                    {
                        table.ScoreTableLine.Clear();
                        context.ScoreTable.Add(table);
                        context.SaveChanges();
                        foreach (var scoreTableLine in lines)
                        {
                            scoreTableLine.ScoreTable = table;
                            context.ScoreTableLine.Add(scoreTableLine);
                            context.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ICollection<ScoreTableLine> LoadTableForTeam(Team team, DAL.ScoreTable table, DAL.AntwerpRCEntities context, string user, DateTime calculationDate)
        {
            try
            {
                var returnLines = new List<ScoreTableLine>();

                var teamClubs = team.TeamClub.Where(tc => !tc.AuditDeleted);
                foreach (var teamClub in teamClubs)
                {
                    var tableLine = new ScoreTableLine();
                    tableLine.TeamclubId = teamClub.TeamClubId;
                    tableLine.TeamClub = teamClub;
                    tableLine.AuditCreatedBy = user;
                    tableLine.AuditCreatedOn = DateTime.Now;

                    var gamesForTeam =
                        context.Game.Where(
                            g =>
                                (g.TeamClub1Id == teamClub.TeamClubId || g.TeamClub2Id == teamClub.TeamClubId) &&
                                g.Date < calculationDate);
                    foreach (var game in gamesForTeam)
                    {
                        //Check for forfait
                        if (game.TeamClub1Score == -1 || game.TeamClub2Score == -1)
                        {
                            if (game.TeamClub1Id == teamClub.TeamClubId)
                            {
                                if (game.TeamClub1Score == -1)
                                {
                                    //The current team is givving forfait
                                    tableLine.Forfait++;
                                    tableLine.Points -= 2;
                                    tableLine.NegativePoints += 25;
                                    tableLine.PositivePoints += 0;
                                }
                                else
                                {
                                    //The current team is NOT givving forfait
                                    tableLine.Won++;
                                    tableLine.Points += 4;
                                    tableLine.Bonus += 1;
                                    tableLine.PositivePoints += 25;
                                    tableLine.NegativePoints += 0;
                                }
                            }
                            else
                            {
                                if (game.TeamClub2Score == -1)
                                {
                                    //This team is giving forfait
                                    tableLine.Forfait++;
                                    tableLine.Points -= 2;
                                    tableLine.NegativePoints += 25;
                                    tableLine.PositivePoints += 0;
                                }
                                else
                                {
                                    tableLine.Won++;
                                    tableLine.Points += 4;
                                    tableLine.Bonus += 1;
                                    tableLine.PositivePoints += 25;
                                    tableLine.NegativePoints += 0;
                                }
                            }
                            tableLine.Games++;
                            continue;
                        }
                        if (game.TeamClub1Score >= 0 && game.TeamClub2Score >= 0)
                        {
                            if (game.TeamClub1Score == game.TeamClub2Score)
                            {
                                //Draw
                                tableLine.Points += 2;
                                tableLine.Draw++;
                                tableLine.PositivePoints += game.TeamClub1Score.Value;
                                tableLine.NegativePoints += game.TeamClub1Score.Value;

                                if (game.TeamClub1Id == teamClub.TeamClubId && game.TeamClub1Tries >= 4)
                                {
                                    tableLine.Bonus++;
                                }
                                if (game.TeamClub2Id == teamClub.TeamClubId && game.TeamClub2Tries >= 4)
                                {
                                    tableLine.Bonus++;
                                }
                            }
                            else if (game.TeamClub1Score > game.TeamClub2Score)
                            {
                                if (game.TeamClub1Id == teamClub.TeamClubId)
                                {
                                    //Team 1 won, check if it's the team we're calculating
                                    tableLine.Points += 4;
                                    tableLine.Won++;
                                    tableLine.PositivePoints += game.TeamClub1Score.Value;
                                    tableLine.NegativePoints += game.TeamClub2Score.Value;
                                    if (game.TeamClub1Tries >= 4)
                                        tableLine.Bonus++;
                                }
                                else
                                {
                                    tableLine.Lost++;
                                    tableLine.PositivePoints += game.TeamClub2Score.Value;
                                    tableLine.NegativePoints += game.TeamClub1Score.Value;
                                    if (game.TeamClub1Score - game.TeamClub2Score <= 7)
                                        tableLine.Bonus++;
                                    else if (game.TeamClub2Tries >= 4)
                                        tableLine.Bonus++;
                                }
                            }
                            else
                            {
                                //Team 2 won, check if it's the team we're calculating
                                if (game.TeamClub2Id == teamClub.TeamClubId)
                                {
                                    tableLine.Points += 4;
                                    tableLine.Won++;
                                    tableLine.PositivePoints += game.TeamClub2Score.Value;
                                    tableLine.NegativePoints += game.TeamClub1Score.Value;
                                    if (game.TeamClub2Tries >= 4)
                                        tableLine.Bonus++;
                                }
                                else
                                {
                                    tableLine.Lost++;
                                    tableLine.PositivePoints += game.TeamClub1Score.Value;
                                    tableLine.NegativePoints += game.TeamClub2Score.Value;
                                    if (game.TeamClub2Score - game.TeamClub1Score <= 7)
                                        tableLine.Bonus++;
                                    else if (game.TeamClub1Tries >= 4)
                                        tableLine.Bonus++;
                                }
                            }
                        }
                        tableLine.Games++;
                    }
                    tableLine.TotalPoints = tableLine.Points + tableLine.Bonus;
                    table.ScoreTableLine.Add(tableLine);
                    tableLine.ScoreTable = table;
                    returnLines.Add(tableLine);
                }
                return SetOrderInTable(returnLines);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ICollection<ScoreTableLine> SetOrderInTable(ICollection<ScoreTableLine> lines)
        {
            int order=0;
            int internalOrder = 0;
            int points = 0;
            bool skipped = false;

            lines =
                lines.OrderByDescending(l => l.TotalPoints)
                    .ThenByDescending(l=>l.Games)
                    .ThenBy(l => l.NegativePoints - l.PositivePoints)
                    .ThenBy(l => l.RedCards)
                    .ThenBy(l => l.TeamClub.Club.ClubName).ToList();

            foreach (var scoreTableLine in lines)
            {
                scoreTableLine.InternalOrder = internalOrder;
                internalOrder++;

                if (scoreTableLine.TotalPoints != points)
                {
                    if (skipped)
                    {
                        order = internalOrder;
                        skipped = false;
                    }
                    else
                    {
                        order++;
                    }
                    points = scoreTableLine.TotalPoints;
                }
                else
                {
                    skipped = true;
                }
                scoreTableLine.Order = order;
            }
            return lines;
        }
    }
}
