using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntwerpRC.BDO
{
    public class CalenderItem
    {
        public long CalenderItemId { get; set; }
        public Season Season { get; set; }
        public Category Category { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public Address HomeAddress { get; set; }
        public Address AwayAddress { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool VisibleForPublic { get; set; }
    }

    public class CalenderItemToInsert
    {
        public long CategoryId { get; set; }
        public long? HomeTeamId { get; set; }
        public long? AwayTeamId { get; set; }
        public long? HomeAddressId { get; set; }
        public long? AwayAddressId { get; set; }
        public string HomeTeamDescription { get; set; }
        public string AwayTeamDescription { get; set; }
        public bool Cancelled { get; set; }
        public string CancellationReason { get; set; }
        public bool VisibleForPublic { get; set; }
    }

    public class CalenderOverviewItem
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public bool FullDays { get; set; }
        public long Id { get; set; }
        public Guid UniqueId { get; set; }
        public bool PlaceHolder { get; set; }
        public int Order { get; set; }
        public bool Splitted { get; set; }
        public string Category { get; set; }
//        public long CategoryId { get; set; }

        private int partOrder = 1;

        public int PartOrder
        {
            get { return partOrder; }
            set { partOrder = value; }
        }

        private int totalParts = 1;

        public int TotalParts
        {
            get { return totalParts; }
            set { totalParts = value; }
        }



        public object Clone()
        {
            return new CalenderOverviewItem
            {
                AwayTeam = this.AwayTeam,
                FullDays = this.FullDays,
                Id = this.Id,
                HomeTeam = this.HomeTeam,
                UniqueId = Guid.NewGuid(),
                PlaceHolder = this.PlaceHolder,
                PartOrder = this.PartOrder,
                TotalParts = this.TotalParts,
                Splitted = this.Splitted,
                Category = this.Category
            };
        }

        public CalenderOverviewItem CalendarItemClone()
        {
            return (CalenderOverviewItem)Clone();
        }

        public int CompareTo(object obj)
        {
            if (obj.GetType() != typeof(CalenderOverviewItem))
            {
                throw new Exception("Must be of type CalendarItem");
            }
            return ((CalenderOverviewItem)obj).Id.CompareTo(this.Id);
        }
    }
}
