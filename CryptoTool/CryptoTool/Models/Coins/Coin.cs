using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Coins
{
    public class Coin
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }
        public MarketData Market_Data { get; set; }
        public List<CoinTicker> Tickers { get; set; }
    }
}
