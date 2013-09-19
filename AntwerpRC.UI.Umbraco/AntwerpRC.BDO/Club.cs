using System.Collections.Generic;

namespace AntwerpRC.BDO
{
    public class Club
    {
        public long ClubId { get; set; }
        public string ClubName { get; set; }
        public string Colour { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public bool HomeClub { get; set; }
    }
}