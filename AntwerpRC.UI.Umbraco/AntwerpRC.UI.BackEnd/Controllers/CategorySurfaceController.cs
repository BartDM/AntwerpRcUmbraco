using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;

namespace AntwerpRC.UI.BackEnd.Controllers
{
    [PluginController("CalendarAdmin")]
    public class CategorySurfaceController : SurfaceController
    {
        private BLL.CategoryManager categoryManager;

        public CategorySurfaceController()
        {
            categoryManager = new BLL.CategoryManager();
        }

        public ActionResult Index()
        {
            var categories = categoryManager.GetAllCategories();
            return View(categories);
        }

        public ActionResult Detail(long id)
        {
            var category = categoryManager.GetCategoryById(id);
            return View(category);
        }

    }
}
