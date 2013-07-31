using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntwerpRC.BDO
{
    public class Category
    {
        public long CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime DefaultStartTime { get; set; }
        public int DefaultGameTime { get; set; }
    }
}
