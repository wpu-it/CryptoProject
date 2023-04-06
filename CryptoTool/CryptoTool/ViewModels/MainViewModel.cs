using CryptoTool.Models;
using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoTool.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private ObservableCollection<Asset> _assets;
        private string _blockText;
        private string _exchangeLogoUrl;
        private Asset _selectedAsset;

        public ObservableCollection<Asset> Assets
        {
            get { return _assets; }
            set
            {
                _assets = value;
                OnPropertyChanged("Assets");
            }
        }
        public string ExchangeLogoUrl 
        {
            get { return _exchangeLogoUrl; }
            set
            {
                _exchangeLogoUrl = value;
                OnPropertyChanged("ExchangeLogoUrl");
            }
        }
        public string BlockText {
            get { return _blockText; }
            set {
                _blockText = value;
                OnPropertyChanged("BlockText");
            }
        }
        public Asset SelectedAsset
        {
            get { return _selectedAsset; }
            set
            {
                if (value != _selectedAsset)
                {
                    _selectedAsset = value;
                    OnPropertyChanged(nameof(SelectedAsset));
                }
            }
        }

        public MainViewModel()
        {
            SetInitialData();
        }

        public async void SetInitialData()
        {
            await GetData("binance");
        }

        public async Task GetData(string exchangeParam)
        {
            BlockText = $"Top 5 currencies by trade volume on {exchangeParam}".ToUpper();
            // Getting popular tickers on Binance
            string url = $"https://api.coingecko.com/api/v3/exchanges/{exchangeParam.ToLower()}/tickers?include_exchange_logo=true&order=volume_desc";
            using(var client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);
                var exchange = JsonConvert.DeserializeObject<Exchange>(json);
                ExchangeLogoUrl = exchange.Tickers[0].Market.Logo;
                List<Asset> assets = new List<Asset>();
                for (int i = 0; i < exchange.Tickers.Count; i++)
                {
                    var found_element = assets.Find((ass) =>
                    {
                        return ass.Id == exchange.Tickers[i].Coin_Id;
                    });
                    if (found_element is null)
                    {
                        // Getting more data about selected tickers
                        url = $"https://api.coingecko.com/api/v3/coins/{exchange.Tickers[i].Coin_Id}?tickers=false&market_data=true&community_data=false&developer_data=false";
                        json = await client.GetStringAsync(url);
                        var coin = JsonConvert.DeserializeObject<Coin>(json);
                        assets.Add(new Asset
                        {
                            Id = coin.Id,
                            Volume = exchange.Tickers[i].Volume,
                            Symbol = coin.Symbol.ToUpper(),
                            Name = coin.Name,
                            Image = coin.Image.Small,
                            Price = coin.Market_Data.Current_Price.USD
                        });
                    }
                    if (assets.Count == 1) break;
                }
                Assets = new ObservableCollection<Asset>(assets);
            }
        }
    }
}
