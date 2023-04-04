using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Exchanges
{
    public class Ticker
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public double Volume { get; set; }
        public string Trade_URL { get; set; }
        public string Coin_Id { get; set; }
    }
}
