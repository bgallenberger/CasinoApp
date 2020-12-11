using System;
using System.Collections.Generic;
using System.Text;

namespace CasinoApp.Models
{
    public class Mode
    {
        public string ID { get; set; }
        public string GameID { get; set; }
        public double Cost { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public int Played { get; set; }
        public double Payedout { get; set; }
        public int Jackpots { get; set; }
        public string JackpotChance { get; set; }
        public string PayoutPercent { get; set; }
        public double Win { get; set; }
        public string BannerText { get; set; }
    }
}
