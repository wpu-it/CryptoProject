using CryptoTool.Models;
using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CryptoTool.ViewModels
{
    public class DetailsViewModel
    {
        private Asset _asset;

        public Asset Asset { get; set; }
        public DetailsViewModel(Asset asset)
        {
            Asset = asset;
            SetInitialData();
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
                    string url = $"https://api.coingecko.com/api/v3/exchanges/{Asset.AssetMarkets[i].MarketImage}";
                    string json = await client.GetStringAsync(url);
                    var change = JsonConvert.DeserializeObject<Exchange>(json);
                    Asset.AssetMarkets[i].MarketImage = change.Image;
               }
            }
        }
    }
}
