using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MoneyConverter.Model
{
    public class ConvertModel
    {
        public List<Currency> CurrenciesNbu { get; set; }
        public List<Currency> CurrenciesPrivat { get; set; }

        private readonly NbuApi _nbuApi = new NbuApi();
        private readonly PrivatApi _privatApi = new PrivatApi();

        private int _actualCalculate;
        private int _actualTop;
        
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public double ConvertValue { get; set; }

        public ConvertModel()
        {
            FillCurrencies();
        }

        private void FillCurrencies()
        {
            CurrenciesNbu = _nbuApi.GetCurrencies(_nbuApi.GetJson());
            CurrenciesPrivat = _privatApi.GetCurrencies(_privatApi.GetJson());

            CurrenciesNbu.Add(new Currency("UAH", 1, 1));
            CurrenciesPrivat.Add(new Currency("UAH", 1, 1));
        }

        public double Convert(double value, string currencyFrom, string currencyTo, List<Currency> currencies)
        {
            double rateFrom, rateTo, result;
            if (currencyFrom == "UAH")
            {
                rateFrom = 1;
                rateTo = currencies.Find(x => x.Name == currencyTo).ValueSell;
            }
            else if (currencyTo == "UAH")
            {
                rateFrom = currencies.Find(x => x.Name == currencyFrom).ValueBuy;
                rateTo = 1;
            }
            else
            {
                rateFrom = currencies.Find(x => x.Name == currencyFrom).ValueBuy;
                rateTo = currencies.Find(x => x.Name == currencyTo).ValueSell;
            }
            result = value * rateFrom / rateTo;
            return result;
        }

        public void ChangeTop(int top)
        {
            _actualTop = top;
        }
        
        public void ChangeCalculate(int calculate)
        {
            _actualCalculate = calculate;
        }

        public string GetCurrencyBuy(string from, string to, bool isForTop = false)
        {
            double value;
            var switcher = isForTop ? _actualTop : _actualCalculate;
            switch (switcher)
            {
                case 0:
                    value = Convert(1, from, to, CurrenciesNbu);
                    return (value < 1
                        ? 1 / value
                        : value).ToString("F2");
                case 1:
                    value = Convert(1, from, to, CurrenciesPrivat);
                    return (value < 1
                        ? 1 / value
                        : value).ToString("F2");
                default:
                    return "0";
            }
        }

        public List<Currency> GetConvertCurrencies()
        {
            switch (_actualCalculate)
            {
                case 0:
                    return CurrenciesNbu;
                case 1:
                    return CurrenciesPrivat;
                default:
                    return null;
            }
        }
        
        public List<string> GetConvertCurrenciesCc()
        {
            var result = new List<string>
            {
                "UAH",
                "USD",
                "EUR"
            };
            switch (_actualCalculate)
            {
                case 0:
                    result.AddRange(CurrenciesNbu.ConvertAll(x => x.Name).Where(x => x != "UAH").Where(x => x != "USD").Where(x => x != "EUR").ToList());
                    return result;
                case 1:
                    result.AddRange(CurrenciesPrivat.ConvertAll(x => x.Name).Where(x => x != "UAH").Where(x => x != "USD").Where(x => x != "EUR").ToList());
                    return result;
                default:
                    return null;
            }
        }
    }
}