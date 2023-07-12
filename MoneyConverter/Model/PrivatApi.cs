using System.Collections.Generic;
using System.Net;

namespace MoneyConverter.Model
{
    public class PrivatApi : IApiManager
    {
        private const string Url = "https://api.privatbank.ua/p24api/exchange_rates?date=";
        
        
        public string GetJson()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Url + System.DateTime.Now.ToString("dd.MM.yyyy"));
                return json;
            }
        }

        public List<Currency> GetCurrencies(string json)
        {
            List<Currency> currencies = new List<Currency>();
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRateData>(json);
            foreach (var currency in data.exchangeRate)
            {
                currencies.Add(new Currency
                {
                    Name = currency.currency.ToUpper(),
                    ValueSell = currency.saleRate ?? currency.saleRateNB,
                    ValueBuy = currency.purchaseRate ?? currency.purchaseRateNB
                });
            }
            return currencies;
        }
        
        private struct CurrencyFromApi
        {
            public string baseCurrency { get; set; }
            public string currency { get; set; }
            public double saleRateNB { get; set; }
            public double purchaseRateNB { get; set; }
            public double? saleRate { get; set; }
            public double? purchaseRate { get; set; }
        }
        
        private class ExchangeRateData
        {
            public string date { get; set; }
            public string bank { get; set; }
            public int baseCurrency { get; set; }
            public string baseCurrencyLit { get; set; }
            public List<CurrencyFromApi> exchangeRate { get; set; }
        }
    }
}