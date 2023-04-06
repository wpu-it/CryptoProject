using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Exchanges
{
    public class Ticker
    {
        public double Volume { get; set; }
        public string Coin_Id { get; set; }
        public Market Market { get; set; }
    }
}
