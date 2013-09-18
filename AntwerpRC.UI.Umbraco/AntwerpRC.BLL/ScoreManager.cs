using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace AntwerpRC.BLL
{
    public class ScoreManager
    {
        public ScoreManager()
        {
        }

        public BDO.ScoreTable GetScoreTableForTeam(long categoryid)
        {


            using (var context = new DAL.AntwerpRCEntities())
            {
                //First get current season
                long seasonId = GetCurrentSeasonId(context);
                if (seasonId >= 0)
                {
                    var table = context.ScoreTable.OrderByDescending(t=>t.CreatedOn).FirstOrDefault(t => t.Team.CategoryId == categoryid && t.Team.SeasonId == seasonId);
                    if (table != null)
                    {
                        table.ScoreTableLine = table.ScoreTableLine.OrderByDescending(t => t.TotalPoints)
                            .ThenBy(t => t.NegativePoints - t.PositivePoints).ToList();
                        var bdo = Mapper.Map<DAL.ScoreTable, BDO.ScoreTable>(table);
                        return bdo;
                    }
                }
            }
            return null;
        }

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
