namespace AntwerpRC.Score.Fetcher
{
    public class FetcherLogic
    {

        public void FetchTeamsForSeason(long seasonId, string user)
        {
            var teamFetcher = new TeamFetcher();
            teamFetcher.FetchTeamsForSeason(seasonId, user);
        }

        public void FetchGamesForSeason(long seasonId, string user)
        {
            var scoreFetcher = new ScoreFetcher();
            scoreFetcher.FetchGamesForSeason(seasonId,user);
        }

        public void CalculateTables(long seasonId, string user)
        {
            var tableCalculator = new Calculator();
            tableCalculator.CalculateTablesForSeason(seasonId,user);
        }
    }
}
