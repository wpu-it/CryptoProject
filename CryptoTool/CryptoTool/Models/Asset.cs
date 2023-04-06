using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.Models
{
    public class Asset : INotifyPropertyChanged
    {
        private List<AssetMarket> _assetMarkets;
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public double Volume { get; set; }
        public double Price { get; set; }
        public string TotalVolume { get; set; }
        public double PriceChange24hPercentage { get; set; }
        public string MarketCap { get; set; }
        public string Image { get; set; }
        public List<AssetMarket> AssetMarkets 
        { 
            get { return _assetMarkets; }
            set
            {
                _assetMarkets = value;
                OnPropertyChanged(nameof(AssetMarkets));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
