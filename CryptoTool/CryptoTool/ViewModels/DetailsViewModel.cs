using CryptoTool.Models;
using CryptoTool.Models.Chart;
using CryptoTool.Models.Coins;
using CryptoTool.Models.Exchanges;
using CryptoTool.Models.Search;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static System.Net.WebRequestMethods;

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
        private List<CandleChartModel> _priceDetails;

        public Asset Asset 
        {
            get { return _asset; }
            set
            {
                _asset = value;
                OnPropertyChanged(nameof(Asset));
            }
        }

        public List<CandleChartModel> PriceDetails 
        {
            get { return _priceDetails; }
            set
            {
                _priceDetails = value;
                OnPropertyChanged(nameof(PriceDetails));
            }
        }

        public DetailsViewModel(Asset asset)
        {
            Asset = asset;
            SetInitialData();
        }

        public async void SetInitialData()
        {
            await GetAssetMarketsImages();
            await GetChart();
        }

        public async Task GetAssetMarketsImages()
        {
            using (var client = new HttpClient())
            {
                List<AssetMarket> markets = new List<AssetMarket>();
                for (int i = 0; i < Asset.AssetMarkets.Count; i++)
                {
                    // Getting data about exhchanges for every asset and finalizing work with model for details page
                    string url = $"https://api.coingecko.com/api/v3/exchanges/{Asset.AssetMarkets[i].MarketImage}";
                    string json = await client.GetStringAsync(url);
                    var change = JsonConvert.DeserializeObject<Exchange>(json);
                    if (change is not null)
                    {
                        markets.Add(new AssetMarket
                        {
                            Base = Asset.AssetMarkets[i].Base,
                            Target = Asset.AssetMarkets[i].Target,
                            MarketName = Asset.AssetMarkets[i].MarketName,
                            MarketImage = change.Image,
                            Price = Asset.AssetMarkets[i].Price,
                            Trade_URL = Asset.AssetMarkets[i].Trade_URL
                        });
                    }
                }
                Asset.AssetMarkets = new List<AssetMarket>(markets);
            }
        }

        public async Task GetChart()
        {
            using(var client = new HttpClient())
            {
                string url = $"https://api.coingecko.com/api/v3/coins/{Asset.Id}/ohlc?vs_currency=usd&days=7";
                string json = await client.GetStringAsync(url);
                var chart = JsonConvert.DeserializeObject<List<List<double>>>(json);
                if(chart is not null)
                {
                    List<CandleChartModel> prices = new List<CandleChartModel>();
                    foreach (var chartData in chart)
                    {
                        var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                        date = date.AddMilliseconds(chartData[0]).ToLocalTime();
                        prices.Add(new CandleChartModel
                        {
                            Date = date,
                            High = chartData[2],
                            Low = chartData[3],
                            Open = chartData[1],
                            Close = chartData[4]
                        });
                    }
                    PriceDetails = prices;
                }
            }
        }

        public async Task GetAsset(string searchValue)
        {
            string url, json;
            using(var client = new HttpClient())
            {
                url = $"https://api.coingecko.com/api/v3/search?query={searchValue}";
                json = await client.GetStringAsync(url);
                var searchResult = JsonConvert.DeserializeObject<SearchModel>(json);
                if(searchResult is not null)
                {
                    if(searchResult.Coins.Count != 0)
                    {
                        string id = searchResult.Coins[0].Id;

                        url = $"https://api.coingecko.com/api/v3/coins/{id}?tickers=true&market_data=true&community_data=false&developer_data=false";
                        json = await client.GetStringAsync(url);
                        var coin = JsonConvert.DeserializeObject<Coin>(json);
                        if (coin is not null)
                        {
                            Asset = new Asset
                            {
                                Id = coin.Id,
                                MarketCap = coin.Market_Data.Market_Cap.USD,
                                PriceChange24hPercentage = coin.Market_Data.Price_Change_Percentage_24h_In_Currency.USD,
                                TotalVolume = coin.Market_Data.Total_Volume.USD,
                                Symbol = coin.Symbol.ToUpper(),
                                Name = coin.Name,
                                Image = coin.Image.Small,
                                Price = coin.Market_Data.Current_Price.USD
                            };
                            Asset.AssetMarkets = new List<AssetMarket>();
                            // If API Key is small for testing all features set 'j < 1'
                            for (int j = 0; j < 2; j++)
                            {
                                Asset.AssetMarkets.Add(new AssetMarket
                                {
                                    Base = coin.Tickers[j].Base,
                                    Target = coin.Tickers[j].Target,
                                    MarketName = coin.Tickers[j].Market.Name,
                                    MarketImage = coin.Tickers[j].Market.Identifier,
                                    Price = coin.Tickers[j].Last,
                                    Trade_URL = coin.Tickers[j].Trade_URL
                                });
                            }
                            await GetAssetMarketsImages();
                            await GetChart();
                        }
                    }
                    
                }
            }
        }
    }
}
