using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntwerpRC.Score.Fetcher;

namespace AntwerpRC.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logic = new FetcherLogic();
//            logic.FetchTeamsForSeason(0,"Bart");
//            logic.FetchGamesForSeason(0,"Bart");
            logic.CalculateTables(0,"Bart");
        }
    }
}
