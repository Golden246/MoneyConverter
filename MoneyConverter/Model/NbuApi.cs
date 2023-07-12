using System.Collections.Generic;
using System.Net;

namespace MoneyConverter.Model
{
    public class NbuApi : IApiManager
    {
        const string Url = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
        
        public string GetJson()
        {
            using (var webClient = new WebClient())
            {
                var json = webClient.DownloadString(Url);
                return json;
            }
        }

        public List<Currency> GetCurrencies(string json)
        {
            List<Currency> currencies = new List<Currency>();
            var currencyList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CurrencyFromApi>>(json);
            foreach (var currency in currencyList)
            {
                currencies.Add(new Currency
                {
                    Name = currency.cc.ToUpper(),
                    ValueSell = currency.rate,
                    ValueBuy = currency.rate
                });
            }
            return currencies;
        }

        private struct CurrencyFromApi
        {
            public int r030 { get; set; }
            public string txt { get; set; }
            public double rate { get; set; }
            public string cc { get; set; }
            public string exchangedate { get; set; }
        }
    }
}