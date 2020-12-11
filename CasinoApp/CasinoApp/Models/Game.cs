using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoApp.Models
{
    public class Game
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string NameLowerCase { get; set; }
        public int TotalPlayed { get; set; }
        public double TotalPayout { get; set; }
        public int Jackpots {get;set;}
        public double TotalPutIn { get; set; }
        public String JackpotChance { get; set; }
        public String PayoutPercent { get; set; }
    }
}
