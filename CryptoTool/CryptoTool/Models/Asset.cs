using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models
{
    public class Asset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
