using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntwerpRC.BDO
{
    public class GameResult
    {
        public Team Team { get; set; }
        public string TeamUrl { get; set; }
        public DateTime Date { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public Teams HomeTeam { get; set; }
        public Teams Winner { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int Team1Tries { get; set; }
        public int Team2Tries { get; set; }
        public int Bonus { get; set; }

        public enum Teams
        {
            Team1,
            Team2
        }
    }
}
