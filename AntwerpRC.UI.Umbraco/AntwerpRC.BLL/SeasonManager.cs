using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntwerpRC.DAL;
using log4net;

namespace AntwerpRC.BLL
{
    public class SeasonManager
    {
//        private static readonly ILog Log = LogManager.GetLogger(typeof(SeasonManager));

        public List<BDO.Season> GetSeasons()
        {
            try
            {
                var bdoSeasons = new List<BDO.Season>();
                using (var context = new CalendarEntities())
                {
                    var seasons = context.Seasons.Where(s => !s.AuditDeleted).ToList();
                    seasons.ForEach(s=> bdoSeasons.Add((BDO.Season)s));
                }
                return bdoSeasons;
            }
            catch (Exception ex)
            {
//                Log.Error("Error fetching all seasons from database", ex);
                throw;
            }
            return null;
        }

        public BDO.Season GetSeasonById(long id)
        {
            try
            {
                using (var context = new CalendarEntities())
                {
                    return (BDO.Season) context.Seasons.FirstOrDefault(s => !s.AuditDeleted && s.SeasonId == id);
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Error fetching season from database", ex);
                throw;
            }
            return null;
        }
    }
}
