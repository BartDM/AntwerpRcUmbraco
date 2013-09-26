using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using log4net;
using log4net.Filter;

namespace AntwerpRC.BLL
{
    public class ScoreManager
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScoreManager));


        public IEnumerable<BDO.ScoreTable> GetAllScoreTables()
        {
            var returnValue = new List<BDO.ScoreTable>();
            try
            {
                using (var context = new DAL.AntwerpRCEntities())
                {
                    long seasonId = GetCurrentSeasonId(context);
                    if (seasonId >= 0)
                    {
                        var teams = context.Team.Where(t => !t.AuditDeleted && t.SeasonId == seasonId).ToList();

                        foreach (var team in teams)
                        {
                            returnValue.Add(GetScoreTableForTeam(team.CategoryId));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching all score tables", ex);
            }
            return returnValue;
        }

        /// <summary>
        /// Will fetch all results and return a ScoreTable object with a list of all 
        /// teams of that category with their points, games, ...
        /// </summary>
        /// <param name="categoryid">The ID of the category you want to show</param>
        /// <returns>A ScoreTable obejct or null if an error occured</returns>
        public BDO.ScoreTable GetScoreTableForTeam(long categoryid)
        {
            try
            {
                Log.DebugFormat("Getting score table for category {0}", categoryid);
                using (var context = new DAL.AntwerpRCEntities())
                {
                    //First get current season
                    long seasonId = GetCurrentSeasonId(context);
                    if (seasonId >= 0)
                    {
                        var table = context.ScoreTable.OrderByDescending(t => t.CreatedOn).FirstOrDefault(t => t.Team.CategoryId == categoryid && t.Team.SeasonId == seasonId);
                        if (table != null)
                        {
                            table.ScoreTableLine = table.ScoreTableLine.OrderBy(t => t.InternalOrder).ToList();
                            var bdo = Mapper.Map<DAL.ScoreTable, BDO.ScoreTable>(table);
                            return bdo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching score table", ex);
            }
            return null;
        }

        public IEnumerable<BDO.ScoreTableOverview> GetScoreTableForOverview()
        {
            var scoreTables = new List<BDO.ScoreTableOverview>();
            try
            {
                using (var context = new DAL.AntwerpRCEntities())
                {
                    long seasonId = GetCurrentSeasonId(context);
                    if (seasonId >= 0)
                    {
                        var teams = context.Team.Where(t => !t.AuditDeleted && t.SeasonId == seasonId).ToList();

                        foreach (var team in teams)
                        {
                            var table =
                                context.ScoreTable.OrderByDescending(t => t.CreatedOn)
                                    .FirstOrDefault(
                                        t => t.Team.CategoryId == team.CategoryId && t.Team.SeasonId == seasonId);

                            if (table != null)
                            {
                                var bdo = new BDO.ScoreTableOverview()
                                {
                                    Team = Mapper.Map<DAL.Team, BDO.Team>(team),
                                };
                                int currentOrder = 0;

                                var lines = table.ScoreTableLine.OrderBy(l => l.InternalOrder).ToList();
                                //Find homeTeam
                                if (lines.Any())
                                {
                                    var homeTeam = lines.FirstOrDefault(l => l.TeamClub.Club.HomeClub && !l.AuditDeleted);
                                    if (homeTeam != null)
                                    {
                                        bdo.Order = homeTeam.Order;
                                        currentOrder = homeTeam.Order;

                                        bdo.SharedPlace = lines.Count(l => l.Order == homeTeam.Order) > 1;

                                        bdo.Points = homeTeam.TotalPoints;
                                        if (lines.IndexOf(homeTeam) > 0)
                                        {
                                            var teamAbove = lines[lines.IndexOf(homeTeam) - 1];
                                            bdo.TeamAbove = string.Format("{0} ({1}{2})", teamAbove.TeamClub.Club.ClubName, teamAbove.TotalPoints - homeTeam.TotalPoints > 0 ? "+" : "", teamAbove.TotalPoints - homeTeam.TotalPoints);
                                        }
                                        else
                                        {
                                            bdo.TeamAbove = string.Empty;
                                        }
                                        if (lines.IndexOf(homeTeam) < lines.Count - 1)
                                        {
                                            var teamBelow = lines[lines.IndexOf(homeTeam) + 1];
                                            bdo.TeamBelow = string.Format("{0} ({1}{2})", teamBelow.TeamClub.Club.ClubName,
                                                homeTeam.TotalPoints - teamBelow.TotalPoints > 0 ? "-" : "",
                                                homeTeam.TotalPoints - teamBelow.TotalPoints);
                                        }
                                        else
                                        {
                                            bdo.TeamBelow = string.Empty;
                                        }

                                    }
                                }
                                //Get table where order is different
                                var oldTable = context.ScoreTable.FirstOrDefault(t => t.Team.CategoryId == team.CategoryId && t.Team.SeasonId == seasonId && t.ScoreTableLine.Any(tl => tl.TeamClub.Club.HomeClub && !tl.AuditDeleted && tl.Order != currentOrder));
                                if (oldTable != null)
                                {
                                    var oldLines = oldTable.ScoreTableLine.OrderBy(l => l.InternalOrder).ToList();
                                    var homeTeam = oldLines.FirstOrDefault(l => l.TeamClub.Club.HomeClub && !l.AuditDeleted);
                                    if (homeTeam != null)
                                    {
                                        bdo.Evolution = homeTeam.Order - currentOrder;
                                        bdo.EvolutionDate = oldTable.CreatedOn;
                                    }
                                }
                                if (!bdo.EvolutionDate.HasValue)
                                {
                                    bdo.Evolution = 0;
                                }
                                scoreTables.Add(bdo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching score table for overview", ex);
            }
            return scoreTables.OrderBy(t => t.Order).ThenByDescending(t => t.Points).ToList();
        }

        /// <summary>
        /// Will fetch all latest results (2 per category) to form an overview of al teams
        /// latest games. 
        /// </summary>
        /// <returns>A IEnumerable<BDO.GameResult>. Will be an empty list if an error occurs</returns>
        public IEnumerable<BDO.GameResult> GetLatestResultsForOverview()
        {
            Log.Debug("Fetching latest results for overview");
            var gameResults = new List<BDO.GameResult>();
            try
            {
                using (var context = new DAL.AntwerpRCEntities())
                {
                    long seasonId = GetCurrentSeasonId(context);
                    if (seasonId >= 0)
                    {
                        var teams = context.Team.Where(t => !t.AuditDeleted && t.SeasonId == seasonId);
                        foreach (var team in teams)
                        {
                            gameResults.AddRange(GetLatestResultsForTeam(team.CategoryId, 2));
                        }
                        gameResults.Sort((g1, g2) => g2.Date.CompareTo(g1.Date));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching results for overview", ex);
            }
            return gameResults;
        }

        /// <summary>
        /// Will fetch all latest results for a specific category to form an overview
        /// </summary>
        /// <param name="categoryId">The ID of the category you want to show</param>
        /// <param name="amountOfGames">The amount of results you want to return, default 10</param>
        /// <returns>A IEnumerable<BDO.GameResult>. Will be an empty list if an error occurs</returns>
        public IEnumerable<BDO.GameResult> GetLatestResultsForTeam(long categoryId, int amountOfGames = 10)
        {
            var gameResults = new List<BDO.GameResult>();
            try
            {
                Log.DebugFormat("Fetching latest results for category {0}", categoryId);
                using (var context = new DAL.AntwerpRCEntities())
                {
                    long seasonId = GetCurrentSeasonId(context);
                    if (seasonId >= 0)
                    {
                        var team =
                            context.Team.FirstOrDefault(
                                t => t.SeasonId == seasonId && t.CategoryId == categoryId && !t.AuditDeleted);
                        if (team != null)
                        {
                            var games =
                                context.Game.Where(
                                    g =>
                                        !g.AuditDeleted &&
                                        ((g.TeamClub.Team.CategoryId == categoryId && g.TeamClub.Club.HomeClub) ||
                                         (g.TeamClub1.Team.CategoryId == categoryId && g.TeamClub1.Club.HomeClub)) &&
                                        g.Date <= DateTime.Now).OrderByDescending(g => g.Date).Take(amountOfGames);
                            if (games.Any())
                            {
                                foreach (var game in games)
                                {
                                    var bdo = Mapper.Map<DAL.Game, BDO.GameResult>(game);
                                    bdo.Team = Mapper.Map<DAL.Team, BDO.Team>(team);
                                    if (bdo.HomeTeam == BDO.GameResult.Teams.Team1)
                                    {
                                        bdo.Team = Mapper.Map<DAL.Team, BDO.Team>(game.TeamClub.Team);
                                        if (bdo.Team1Score < bdo.Team2Score)
                                        {
                                            bdo.Winner = BDO.GameResult.Teams.Team2;
                                            if (bdo.Team1Score + 7 >= bdo.Team2Score)
                                                bdo.Bonus = 1;
                                        }
                                        else
                                        {
                                            bdo.Winner = BDO.GameResult.Teams.Team1;
                                            if (bdo.Bonus <= 0 && bdo.Team1Tries >= 4)
                                            {
                                                bdo.Bonus = 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        bdo.Team = Mapper.Map<DAL.Team, BDO.Team>(game.TeamClub1.Team);
                                        if (bdo.Team2Score < bdo.Team1Score)
                                        {
                                            bdo.Winner = BDO.GameResult.Teams.Team1;
                                            if (bdo.Team2Score + 7 >= bdo.Team1Score)
                                                bdo.Bonus = 1;
                                        }
                                        else
                                        {
                                            bdo.Winner = BDO.GameResult.Teams.Team2;
                                            if (bdo.Bonus <= 0 && bdo.Team2Tries >= 4)
                                            {
                                                bdo.Bonus = 1;
                                            }
                                        }
                                    }
                                    gameResults.Add(bdo);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error fetching latests results for team", ex);
            }
            return gameResults;
        }

        /// <summary>
        /// Internal helper to fetch the current season ID
        /// </summary>
        /// <param name="context">The database context</param>
        /// <returns>Will return the season ID or -1 if an error occured</returns>
        private long GetCurrentSeasonId(DAL.AntwerpRCEntities context)
        {
            var season =
                context.Season.FirstOrDefault(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now);
            if (season == null)
            {
                season = context.Season.OrderByDescending(s => s.StartDate)
                    .FirstOrDefault(s => s.EndDate < DateTime.Now);
            }

            if (season != null)
                return season.SeasonId;

            return -1;
        }
    }
}
