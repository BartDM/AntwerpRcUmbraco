using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AntwerpRC.BDO;

namespace AntwerpRC.DAL
{
    public partial class Seasons
    {
        /// <summary>
        /// Converts an existing DAL.Seasons to its BDO representation.
        /// </summary>
        public static explicit operator BDO.Season(Seasons dalSeasons)
        {
            if (dalSeasons == null)
                return null;

            return new BDO.Season
                {
                    Description = dalSeasons.Description,
                    EndDate = dalSeasons.EndDate,
                    SeasonId = dalSeasons.SeasonId,
                    StartDate = dalSeasons.StartDate
                };
        }

        /// <summary>
        /// Converts an existing DAL.Seasons to its BDO representation.
        /// </summary>
        public static explicit operator BDO.FullSeason(Seasons dalSeasons)
        {
            if (dalSeasons == null)
                return null;

            var bdoSeason = new BDO.FullSeason
                {
                    Description = dalSeasons.Description,
                    EndDate = dalSeasons.EndDate,
                    SeasonId = dalSeasons.SeasonId,
                    StartDate = dalSeasons.StartDate,
                    Categories = new List<Category>()
                };

            //dalSeasons.Ca

            return bdoSeason;
        }

        /// <summary>
        /// Converts an existing BDO.Season to its DAL representation.
        /// </summary>
        public static explicit operator Seasons(BDO.Season bdoSeasons)
        {
            Seasons dalSeasons = new Seasons();
            Fill(dalSeasons, bdoSeasons);
            return dalSeasons;
        }

        /// <summary>
        /// Overwrites non-audit fields of the given DAL.Seasons with values of the given BDO.Season.
        /// </summary>
        public static void Fill(Seasons dalSeasons, BDO.Season bdoSeason)
        {
            if (dalSeasons == null || bdoSeason == null)
                return;

            // Implement properties WITHOUT id here

            //return dalSeasons;
            throw new NotImplementedException("Fill mmethod for Seasons is not implemented yet.");
        }
    }


    public partial class Categories
    {
        /// <summary>
        /// Converts an existing DAL.Categories to its BDO representation.
        /// </summary>
        public static explicit operator BDO.Category(Categories dalCategories)
        {
            if (dalCategories == null)
                return null;

            return new BDO.Category
                {
                    CategoryId = dalCategories.CategoryId,
                    Description = dalCategories.Description,
                    DefaultGameTime = dalCategories.DefaultGameTime.HasValue ? dalCategories.DefaultGameTime.Value : 0,
                    DefaultStartTime = dalCategories.DefaultStartTime.HasValue ? new DateTime(2000, 1, 1, dalCategories.DefaultStartTime.Value.Hours, dalCategories.DefaultStartTime.Value.Minutes, 0) : new DateTime(0, 0, 0, 0, 0, 0)
                };
        }

        /// <summary>
        /// Converts an existing BDO.Category to its DAL representation.
        /// </summary>
        public static explicit operator Categories(BDO.Category bdoCategories)
        {
            Categories dalCategories = new Categories();
            Fill(dalCategories, bdoCategories);
            return dalCategories;
        }

        /// <summary>
        /// Overwrites non-audit fields of the given DAL.Categories with values of the given BDO.Category.
        /// </summary>
        public static void Fill(Categories dalCategories, BDO.Category bdoCategory)
        {
            if (dalCategories == null || bdoCategory == null)
                return;

            // Implement properties WITHOUT id here

            //return dalCategories;
            throw new NotImplementedException("Fill mmethod for Categories is not implemented yet.");
        }
    }

    public partial class CalenderItems
    {
        /// <summary>
        /// Converts an existing DAL.CalenderItems to its BDO representation.
        /// </summary>
        public static explicit operator BDO.CalenderOverviewItem(CalenderItems dalCalenderItems)
        {
            if (dalCalenderItems == null)
                return null;

            return new BDO.CalenderOverviewItem
                {
                     Category = dalCalenderItems.Categories.Description,
                     EndDate = dalCalenderItems.EndDate,
                     AwayTeam = dalCalenderItems.AwayTeam.TeamName,
                     FullDays = dalCalenderItems.FullDays,
                     HomeTeam = dalCalenderItems.HomeTeam.TeamName,
                     Id = dalCalenderItems.CalenderItemId,
                     StartDate = dalCalenderItems.StartDate,
                     UniqueId = Guid.NewGuid()
                };
        }

        /// <summary>
        /// Converts an existing BDO.CalenderOverviewItem to its DAL representation.
        /// </summary>
        public static explicit operator CalenderItems(BDO.CalenderOverviewItem bdoCalenderOverviewItem)
        {
            CalenderItems dalCalenderItems = new CalenderItems();
            Fill(dalCalenderItems, bdoCalenderOverviewItem);
            return dalCalenderItems;
        }

        /// <summary>
        /// Overwrites non-audit fields of the given DAL.CalenderItems with values of the given BDO.CalenderOverviewItem.
        /// </summary>
        public static void Fill(CalenderItems dalCalenderItems, BDO.CalenderOverviewItem bdoCalenderOverviewItem)
        {
            if (dalCalenderItems == null || bdoCalenderOverviewItem == null)
                return;

            // Implement properties WITHOUT id here

            //return dalCalenderItems;
            throw new NotImplementedException("Fill mmethod for CalenderItems is not implemented yet.");
        }
    }










}
