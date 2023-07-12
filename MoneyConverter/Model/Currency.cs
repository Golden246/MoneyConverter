namespace MoneyConverter.Model
{
    public class Currency
    {
        public Currency() {}
        public Currency(string cc, double rateBuy, double rateSell)
        {
            Name = cc;
            ValueBuy = rateBuy;
            ValueSell = rateSell;
        }

        public string Name { get; set; }
        public double ValueSell { get; set; }
        public double ValueBuy { get; set; }
    }
}