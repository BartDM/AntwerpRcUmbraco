using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AntwerpRC.BDO;
using AntwerpRC.UI.FrontEnd.Models;
using Umbraco.Web.Models;

namespace AntwerpRC.UI.FrontEnd.Controllers
{
    public class ScoreController : Umbraco.Web.Mvc.SurfaceController
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

        public ActionResult GetAllScoreTables()
        {
            var model = scoreManager.GetAllScoreTables();
            return PartialView(model);
        }

        public ActionResult GetScoreForOverview()
        {
            var model = scoreManager.GetScoreTableForOverview();
            var contentType = Services.ContentTypeService.GetContentType("Team");
            var teamPages = Services.ContentService.GetContentOfContentType(contentType.Id).ToList();

            if (teamPages.Any())
            {
                foreach (var scoreTableOverview in model)
                {
                    var page = teamPages.FirstOrDefault(t => t.HasProperty("teamId") && t.GetValue<int>("teamId") == scoreTableOverview.Team.Category.CategoryId);
                    if (page != null)
                    {

                        scoreTableOverview.TeamUrl = Umbraco.NiceUrl(page.Id);
                    }
                }
            }
            return PartialView(model);
        }

        public ActionResult GetLatestScoresForTeam(int teamId)
        {
            var model = scoreManager.GetLatestResultsForTeam(teamId);
            return PartialView(model);
        }

        public ActionResult GetLatestScoresForOverview()
        {
            var model = scoreManager.GetLatestResultsForOverview();
            var contentType = Services.ContentTypeService.GetContentType("Team");
            var teamPages = Services.ContentService.GetContentOfContentType(contentType.Id).ToList();
            if (teamPages.Any())
            {
                foreach (var gameResult in model)
                {
                    var page = teamPages.FirstOrDefault(t => t.HasProperty("teamId") && t.GetValue<int>("teamId") == gameResult.Team.Category.CategoryId);
                    if (page != null)
                    {
                        gameResult.TeamUrl = Umbraco.NiceUrl(page.Id);
                    }
                }
            }
            return PartialView(model);
        }
    }
}