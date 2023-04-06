using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models
{
    public class AssetMarket
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public string MarketName { get; set; }
        public string MarketImage { get; set; }
        public double Price { get; set; }
        public string Trade_URL { get; set; }
    }
}
