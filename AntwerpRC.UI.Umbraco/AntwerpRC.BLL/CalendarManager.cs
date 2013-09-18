using System;
using System.Collections.Generic;
using System.Linq;
using AntwerpRC.BDO;
using AntwerpRC.DAL;

namespace AntwerpRC.BLL
{
    public class CalendarManager
    {
//        public List<CalenderOverviewItem> GetCalendarItemsForMonth(int month, int year)
//        {
//            var dates = new List<CalenderOverviewItem>();
//            using (var context = new CalendarEntities())
//            {
//                IOrderedQueryable<CalenderItems> dalCalenderItems =
//                    context.CalenderItems.Where(
//                        c => !c.AuditDeleted && c.VisibleForPublic &&
//                             ((c.StartDate.Month == month && c.StartDate.Year == year) ||
//                              (c.EndDate.Month == month && c.EndDate.Year == year))).OrderBy(c => c.StartDate);
//                dalCalenderItems.ToList().ForEach(c => dates.Add((CalenderOverviewItem) c));
//            }
//
//            dates = ProcessForMonthCrossingItems(dates, month, year);
//            dates = ProcessForWeekCrossingItems(dates);
//            dates = AddPartInformation(dates);
//
//            //remove the parts that not will be displayed in this month
//            dates.RemoveAll(d => d.StartDate.Month != month && d.EndDate.Month != month);
//
//            return dates;
//        }

        private List<BDO.CalenderOverviewItem> ProcessForMonthCrossingItems(List<BDO.CalenderOverviewItem> dates, int month, int year)
        {
            //Filter the items for this month. If the item is corssing month borders
            //we have to create a new item for the current month and remove the old one.
            List<CalenderOverviewItem> datesCrossingMonths =
                dates.Where(d => d.StartDate.Month == month || d.EndDate.Month == month).ToList();

            var itemsCrossingMonthsToAdd = new List<CalenderOverviewItem>();
            var itemsCorssingMonthsIdsToRemove = new List<Guid>();

            //First check the ones that will be at the end of the month
            foreach (CalenderOverviewItem item in datesCrossingMonths.Where(d => d.EndDate.Month != month))
            {
                CalenderOverviewItem itemToAdd = item.CalendarItemClone();
                itemToAdd.StartDate = item.StartDate;
                itemToAdd.EndDate =
                    new DateTime(month != 12 ? year : year + 1, month != 12 ? month + 1 : 1, 1, item.EndDate.Hour,
                                 item.EndDate.Minute, item.EndDate.Second).AddDays(-1);

                itemsCrossingMonthsToAdd.Add(itemToAdd);

                //Add the second part so we can calculate the total parts later on
                CalenderOverviewItem secondItemToAdd = item.CalendarItemClone();
                secondItemToAdd.StartDate = itemToAdd.EndDate.AddDays(1);
                secondItemToAdd.EndDate = item.EndDate;

                itemsCrossingMonthsToAdd.Add(secondItemToAdd);

                itemsCorssingMonthsIdsToRemove.Add(item.UniqueId);
            }

            //Then check the ones that will be at the beginning of the month
            foreach (CalenderOverviewItem item in datesCrossingMonths.Where(d => d.StartDate.Month != month))
            {
                CalenderOverviewItem itemToAdd = item.CalendarItemClone();
                itemToAdd.StartDate = new DateTime(year, month, 1, item.StartDate.Hour, item.StartDate.Minute,
                                                   item.StartDate.Second);
                itemToAdd.EndDate = item.EndDate;

                itemsCrossingMonthsToAdd.Add(itemToAdd);

                //Add the second part so we can calculate the total parts later on
                CalenderOverviewItem secondItemToAdd = item.CalendarItemClone();
                secondItemToAdd.StartDate = item.StartDate;
                secondItemToAdd.EndDate = itemToAdd.StartDate.AddDays(-1);
                itemsCrossingMonthsToAdd.Add(secondItemToAdd);

                itemsCorssingMonthsIdsToRemove.Add(item.UniqueId);
            }

            //Add the new parts to the dates
            dates.AddRange(itemsCrossingMonthsToAdd);
            //Remove the original dates (those crossing month borders)
            dates.RemoveAll(d => itemsCorssingMonthsIdsToRemove.Contains(d.UniqueId));

            return dates;
        }
    
        private List<BDO.CalenderOverviewItem> ProcessForWeekCrossingItems(List<BDO.CalenderOverviewItem> dates)
        {
            var itemsToAdd = new List<CalenderOverviewItem>();
            var itemIdsToRemove = new List<Guid>();


            foreach (CalenderOverviewItem item in dates)
            {
                if (item.StartDate.DayOfWeek.CompareTo(DayOfWeek.Sunday) >= 0 &&
                    item.EndDate.DayOfWeek.CompareTo(DayOfWeek.Sunday) > 0)
                {
                    DateTime dateRunner = item.StartDate;
                    DateTime startDate = item.StartDate;
                    while (dateRunner < item.EndDate)
                    {
                        if (dateRunner.DayOfWeek == DayOfWeek.Sunday)
                        {
                            //If we come to a sunday, add a new item from
                            //the startdate until the sunday.
                            //At the end, we'll remove the current item
                            CalenderOverviewItem itemToAdd = item.CalendarItemClone();
                            itemToAdd.StartDate = startDate;
                            itemToAdd.EndDate = new DateTime(dateRunner.Year, dateRunner.Month, dateRunner.Day,
                                                             item.EndDate.Hour, item.EndDate.Minute, item.EndDate.Second);

                            //Set startdate to the monday after this sunday
                            startDate = dateRunner.AddDays(1);

                            //Set part info
                            itemToAdd.Splitted = true;

                            itemsToAdd.Add(itemToAdd);
                        }
                        dateRunner = dateRunner.AddDays(1);
                    }
                    // Check if last date is sunday, otherwise we have to
                    // add the last bit
                    if (item.EndDate.DayOfWeek != DayOfWeek.Sunday)
                    {
                        CalenderOverviewItem itemToAdd = item.CalendarItemClone();
                        itemToAdd.StartDate = startDate;
                        itemToAdd.EndDate = item.EndDate;
                        itemsToAdd.Add(itemToAdd);
                    }
                    itemIdsToRemove.Add(item.UniqueId);
                }
            }
            dates.AddRange(itemsToAdd);
            dates.RemoveAll(d => itemIdsToRemove.Contains(d.UniqueId));

            return dates;
        }
        
        private List<BDO.CalenderOverviewItem> AddPartInformation(List<BDO.CalenderOverviewItem> dates)
        {
            //Add part info to the dates that are splitted
            IEnumerable<IGrouping<long, CalenderOverviewItem>> items =
                dates.Where(d => !d.PlaceHolder).GroupBy(d => d.Id);

            foreach (var item in items.Where(i => i.Count() > 1).ToList())
            {
                int counter = 1;
                item.OrderBy(d => d.StartDate).ToList().ForEach(i =>
                {
                    i.Splitted = true;
                    i.TotalParts = item.Count();
                    i.PartOrder = counter;
                    counter++;
                });
            }
            return dates;
        }
    }
}