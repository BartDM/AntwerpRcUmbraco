using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace AntwerpRC.UI.BackEnd.Controllers
{
    [PluginController("CalendarAdmin")]
    public class SeasonSurfaceController : SurfaceController
    {
        private BLL.SeasonManager seasonManager;

        public SeasonSurfaceController()
        {
            seasonManager = new BLL.SeasonManager();
        }

        public ActionResult Index()
        {
            var seasons = seasonManager.GetSeasons();
            return View(seasons);
        }

        public ActionResult Edit(long id)
        {
            var season = seasonManager.GetSeasonById(id);
            return View(season);
        }

    }
}
