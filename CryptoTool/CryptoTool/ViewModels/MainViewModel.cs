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
        public ObservableCollection<Asset> Assets
        {
            get { return _assets; }
            set
            {
                _assets = value;
                OnPropertyChanged("Assets");
            }
        }

        private ListCollectionView _view;
        public ICollectionView View
        {
            get { return _view; }
        }
        public MainViewModel()
        {
            SetInitialData();
        }

        public async void SetInitialData()
        {
            await GetData();
            _view = new ListCollectionView(Assets);
        }

        public async Task GetData()
        {
            // Getting popular tickers on Binance
            string url = "https://api.coingecko.com/api/v3/exchanges/binance/tickers?order=volume_desc";
            using(var client = new HttpClient())
            {
                string json = await client.GetStringAsync(url);
                var exchange = JsonConvert.DeserializeObject<Exchange>(json);
                List<Asset> assets = new List<Asset>();
                for(int i = 0; i < exchange.Tickers.Count; i++)
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
                            Symbol = coin.Symbol,
                            Name = coin.Name,
                            Image = coin.Image.Small,
                            Price = coin.Market_Data.Current_Price.USD
                        });
                    }
                    if (assets.Count == 10) break;
                }
                Assets = new ObservableCollection<Asset>(assets);
            }
        }
    }
}
