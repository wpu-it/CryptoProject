using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models.Coins
{
    public class MarketData
    {
        public CurrentPrice Current_Price { get; set; }
        public TotalVolume Total_Volume { get; set; }
        public MarketCap Market_Cap { get; set; }
        public PriceChangeDayPercentage Price_Change_Percentage_24h_In_Currency { get; set; }
    }
}
