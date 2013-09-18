using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntwerpRC.DAL;

namespace AntwerpRC.Score.Fetcher
{
    public class Calculator
    {

        public void CalculateTablesForSeason(long seasonId, string user)
        {
            using (var context = new DAL.AntwerpRCEntities())
            {
                var season = context.Season.FirstOrDefault(s => s.SeasonId == seasonId && !s.AuditDeleted);
                if (season != null)
                {
                    //Fetch all teams for season
                    LoadTableForTeams(season.Team, context, user);
                }
            }
        }

        private void LoadTableForTeams(IEnumerable<DAL.Team> teams, DAL.AntwerpRCEntities context, string user)
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
                        CreatedOn = DateTime.Now
                    };
                    team.ScoreTable.Add(table);
                    context.SaveChanges();

                    LoadTableForTeam(team, table, context, user);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadTableForTeam(Team team, DAL.ScoreTable table, DAL.AntwerpRCEntities context, string user)
        {
            try
            {
                var teamClubs = team.TeamClub.Where(tc => !tc.AuditDeleted);
                foreach (var teamClub in teamClubs)
                {
                    var tableLine = new ScoreTableLine();
                    tableLine.TeamclubId = teamClub.TeamClubId;
                    tableLine.AuditCreatedBy = user;
                    tableLine.AuditCreatedOn = DateTime.Now;

                    var gamesForTeam =
                        context.Game.Where(
                            g =>
                                (g.TeamClub1Id == teamClub.TeamClubId || g.TeamClub2Id == teamClub.TeamClubId) &&
                                g.Date < DateTime.Now);
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
                }
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /*
                public Tables CalculateTableForDivisionWr(long divisionId, long seasonId)
                {
                    try
                    {
                        using (var context = new ScoresEntities())
                        {
                            //Fetch all teams for division/season
                            var teams =
                                context.TeamSeasonDivision.Where(tsd => tsd.SeasonId == seasonId && tsd.DivisionId == divisionId)
                                       .Select(tsd => tsd.Teams).ToList();
                            //fetch all games before now for this division and season
                            var games =
                                context.games.Where(
                                    g => g.SeasonId == seasonId && g.Divisionid == divisionId && g.Date <= DateTime.Now);

                            //Now process the games to calculate score
                            var table = new Tables();
                            table.CreatedOn = DateTime.Now;
                            table.DivisionId = divisionId;
                            table.SeasonId = seasonId;
                            table.SelfCalculated = true;

                            context.Tables.Add(table);

                            //Loop the teams and calculate score per game for that team
                            foreach (var team in teams)
                            {
                                var tableLine = new TableLines();
                                tableLine.TeamId = team.TeamId;
                                var teamGames = games.Where(g => g.Team1Id == team.TeamId || g.Team2Id == team.TeamId);
                                foreach (var teamGame in teamGames)
                                {
                                    //Check for forfait
                                    if (teamGame.Team1Score == -1 || teamGame.Team2Score == -1)
                                    {
                                        if (teamGame.Team1Id == team.TeamId)
                                        {
                                            if (teamGame.Team1Score == -1)
                                            {
                                                //This team is giving forfait
                                                tableLine.Forfait++;
                                                tableLine.Points -= 2;
                                                tableLine.NegativePoints += 25;
                                                tableLine.PositivePoints += 0;
                                            }
                                            else
                                            {
                                                //This team is not forfaiting
                                                tableLine.Won++;
                                                tableLine.Points += 4;
                                                tableLine.Bonus += 1;
                                                tableLine.PositivePoints += 25;
                                                tableLine.NegativePoints += 0;
                                            }
                                        }
                                        else
                                        {
                                            if (teamGame.Team2Score == -1)
                                            {
                                                //This team is giving forfait
                                                tableLine.Forfait++;
                                                tableLine.Points -= 2;
                                                tableLine.NegativePoints += 25;
                                                tableLine.PositivePoints += 0;
                                            }
                                            else
                                            {
                                                //This team is not forfaiting
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

                                    if (teamGame.Team1Score >= 0 && teamGame.Team2Score >= 0)
                                    {
                                        //Get score to see who won
                                        if (teamGame.Team1Score == teamGame.Team2Score)
                                        {
                                            //Draw => Every team gets 2 points
                                            tableLine.Points += 2;
                                            tableLine.Draw++;
                                            tableLine.PositivePoints += teamGame.Team1Score.Value;
                                            tableLine.NegativePoints += teamGame.Team1Score.Value;
                                            // no bonus in draw games
                                            if (teamGame.Team1Id == team.TeamId && teamGame.Team1Tries >= 4)
                                            {
                                                tableLine.Bonus++;
                                            }
                                            if (teamGame.Team2Id == team.TeamId && teamGame.Team2Tries >= 4)
                                            {
                                                tableLine.Bonus++;
                                            }
                                        }
                                        else if (teamGame.Team1Score > teamGame.Team2Score)
                                        {
                                            if (teamGame.Team1Id == team.TeamId)
                                            {
                                                //Team 1 won, check if it's the team we're calculating

                                                //Won, add 4 points
                                                tableLine.Points += 4;
                                                tableLine.Won++;
                                                tableLine.PositivePoints += teamGame.Team1Score.Value;
                                                tableLine.NegativePoints += teamGame.Team2Score.Value;
                                                //Check for winner bonus
                                                if (teamGame.Team1Tries >= 4)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                            }
                                            else
                                            {
                                                //Lost, no points to add
                                                tableLine.Lost++;
                                                tableLine.PositivePoints += teamGame.Team2Score.Value;
                                                tableLine.NegativePoints += teamGame.Team1Score.Value;
                                                //Check for looser bonus
                                                if (teamGame.Team1Score - teamGame.Team2Score <= 7)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                                else if (teamGame.Team2Tries >= 4)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Team 2 won, check if it's the team we're calculating
                                            if (teamGame.Team2Id == team.TeamId)
                                            {
                                                //Won, add 4 points
                                                tableLine.Points += 4;
                                                tableLine.Won++;
                                                tableLine.PositivePoints += teamGame.Team2Score.Value;
                                                tableLine.NegativePoints += teamGame.Team1Score.Value;
                                                //Check for winner bonus
                                                if (teamGame.Team2Tries >= 4)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                            }
                                            else
                                            {
                                                //Lost, no points to add
                                                tableLine.Lost++;
                                                tableLine.PositivePoints += teamGame.Team1Score.Value;
                                                tableLine.NegativePoints += teamGame.Team2Score.Value;
                                                //Check for looser bonus
                                                if (teamGame.Team2Score - teamGame.Team1Score <= 7)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                                else if (teamGame.Team1Tries >= 4)
                                                {
                                                    tableLine.Bonus++;
                                                }
                                            }
                                        }
                                        tableLine.Games++;
                                    }
                                }
                                tableLine.TotalPoints = tableLine.Points + tableLine.Bonus;
                                table.TableLines.Add(tableLine);
                            }
                            context.SaveChanges();
                            return table;
                        }
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(string.Format("Error calculating table: {0}", ex));
                        throw;
                    }
                }
        */

    }
}
