using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntwerpRC.UI.FrontEnd.Models;
using Umbraco.Web.Models;

namespace AntwerpRC.UI.FrontEnd.Controllers
{
    public class ScoreController: Umbraco.Web.Mvc.SurfaceController
    {
        private BLL.ScoreManager scoreManager;

        public ScoreController()
        {
            scoreManager = new BLL.ScoreManager();
        }

        public ActionResult GetScoreForTeam(int teamId)
        {
            var model = scoreManager.GetScoreTableForTeam(teamId);
            return PartialView(model);
        }
    }
}