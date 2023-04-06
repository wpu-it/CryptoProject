using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Coins
{
    public class CoinTicker
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public Market Market { get; set; }
        public double Last { get; set; }
        public string Trade_URL { get; set; }
    }
}
