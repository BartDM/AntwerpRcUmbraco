using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntwerpRC.BDO
{
    public class Team
    {
        public long TeamId { get; set; }
        public List<Address> Addresses { get; set; }
    }

    public class Address
    {
        public long AddressId { get; set; }
        public string Street { get; set; }
        public string Nr { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }
    }

    public class Country
    {
        public long CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
