using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntwerpRC.BDO
{
    public class Season
    {
        public long SeasonId { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class FullSeason : Season
    {
        public List<Category> Categories { get; set; }
    }

    public class SeasonCategory:Season
    {
        public Category Category { get; set; }
        public List<Team> Teams { get; set; }
    }
}
