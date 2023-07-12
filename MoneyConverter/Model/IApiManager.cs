using System.Collections.Generic;

namespace MoneyConverter.Model
{
    public interface IApiManager
    {
        string GetJson();
        List<Currency> GetCurrencies(string json);
    }
}