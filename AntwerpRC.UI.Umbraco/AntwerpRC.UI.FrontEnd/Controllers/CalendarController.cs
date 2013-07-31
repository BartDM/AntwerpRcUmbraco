using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AntwerpRC.BDO;
using AntwerpRC.BLL;

namespace AntwerpRC.UI.FrontEnd.Controllers
{
    public class CalendarController : Controller
    {
        private BLL.CalendarManager calendarManager;

        public CalendarController()
        {
            calendarManager = new CalendarManager();
        }


        public ActionResult Index(int? month, int? year)
        {
            ViewBag.Month = month.HasValue ? month : DateTime.Now.Month;

            ViewBag.Year = year.HasValue ? year : DateTime.Now.Year;


            List<CalenderOverviewItem> dates = calendarManager.GetCalendarItemsForMonth(ViewBag.Month, ViewBag.Year);


            var placeholdersToAdd = new List<CalenderOverviewItem>();

            //Adding placeholders to avoid overlapping of calendar items
            foreach (var item in dates)
            {
                if (item.EndDate.Date > item.StartDate.Date)
                {
                    foreach (var date in EachDay(item.StartDate, item.EndDate))
                    {
                        if (date.Date != item.StartDate.Date)
                            placeholdersToAdd.Add(new CalenderOverviewItem() { StartDate = date, EndDate = date, PlaceHolder = true });
                    }
                }
            }
            dates.AddRange(placeholdersToAdd);

            ViewBag.NGon.Dates = dates.Where(d=>!d.PlaceHolder).ToList();

            return View(dates);
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
