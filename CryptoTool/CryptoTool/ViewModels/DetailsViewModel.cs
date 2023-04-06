﻿using CryptoTool.Models;
using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CryptoTool.ViewModels
{
    public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private Asset _asset;

        public Asset Asset 
        {
            get { return _asset; }
            set
            {
                _asset = value;
                OnPropertyChanged(nameof(Asset));
            }
        }

        public DetailsViewModel(Asset asset)
        {
            Asset = asset;
            //SetInitialData();
        }

        public async void SetInitialData()
        {
            await GetAssetMarketsImage();
        }

        public async Task GetAssetMarketsImage()
        {
            using (var client = new HttpClient())
            {
               for(int i = 0; i < Asset.AssetMarkets.Count; i++)
               {
                    // Getting data about exhchanges for every asset and finalizing work with model for details page
                    string url = $"https://api.coingecko.com/api/v3/exchanges/{Asset.AssetMarkets[i].MarketImage}";
                    string json = await client.GetStringAsync(url);
                    var change = JsonConvert.DeserializeObject<Exchange>(json);
                    Asset.AssetMarkets[i].MarketImage = change.Image;
               }
            }
        }

        public async Task GetAsset(string searchValue)
        {
            using(var client = new HttpClient())
            {
                string url = $"https://api.coingecko.com/api/v3/coins/{exchange.Tickers[i].Coin_Id}?tickers=true&market_data=true&community_data=false&developer_data=false";
            }
        }
    }
}
