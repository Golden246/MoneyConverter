using MoneyConverter.Model;

namespace MoneyConverter.Controller
{
    public class ConvertController
    {
        public ConvertModel ConvertModel { get; set; }
        public MainWindow MainWindow { get; set; }

        public ConvertController(MainWindow mainWindow)
        {
            ConvertModel = new ConvertModel();
            MainWindow = mainWindow;
        }

        public void ChangeTop(int top)
        {
            ConvertModel.ChangeTop(top);
            MainWindow.PopularCurrencyBuy1.Content = ConvertModel.GetCurrencyBuy("USD", "UAH", true);
            MainWindow.PopularCurrencyBuy2.Content = ConvertModel.GetCurrencyBuy("EUR", "UAH", true);
            MainWindow.PopularCurrencyBuy3.Content = ConvertModel.GetCurrencyBuy("EUR", "USD",true);
            
            MainWindow.PopularCurrencySale1.Content = ConvertModel.GetCurrencyBuy("UAH", "USD", true);
            MainWindow.PopularCurrencySale2.Content = ConvertModel.GetCurrencyBuy("UAH", "EUR", true);
            MainWindow.PopularCurrencySale3.Content = ConvertModel.GetCurrencyBuy("USD", "EUR", true);
        }
        
        public double Convert()
        {
            return ConvertModel.Convert(ConvertModel.ConvertValue, ConvertModel.CurrencyFrom, ConvertModel.CurrencyTo, ConvertModel.GetConvertCurrencies());
        }
        
        public void ChangeCalculate()
        {
            ConvertModel.ChangeCalculate(MainWindow.CalculateBank.SelectedIndex);
            var newItems = ConvertModel.GetConvertCurrenciesCc();
            MainWindow.CurrencyFromConvert.ItemsSource = newItems;
            MainWindow.CurrencyToConvert.ItemsSource = newItems;
            MainWindow.CurrencyFromConvert.SelectedItem = null; // Clear selection
            MainWindow.CurrencyToConvert.SelectedItem = null; // Clear selection

            MainWindow.CurrencyFromConvert.SelectedIndex = 0;
            MainWindow.CurrencyToConvert.SelectedIndex = 0;
            MainWindow.CurrencyPrice.Text = ConvertModel.GetCurrencyBuy(MainWindow.CurrencyFromConvert.SelectedItem.ToString().ToUpper(),
                MainWindow.CurrencyToConvert.SelectedItem.ToString().ToUpper());
            MainWindow.CurrencyFromConvertValue.Text = "1";
        }
    }
}