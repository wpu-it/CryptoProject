using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Exchanges
{
    public class Exchange
    {
        public string Name { get; set; }
        public List<Ticker> Tickers { get; set; }
    }
}
