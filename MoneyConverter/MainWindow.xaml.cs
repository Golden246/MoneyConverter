using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using MoneyConverter.Controller;

namespace MoneyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private ConvertController _convertController;
        
        public MainWindow()
        {
            InitializeComponent();
            _convertController = new ConvertController(this);
            Startup();
        }
        
        private void Startup()
        {
            TopBank.SelectedIndex = 0;
            CalculateBank.SelectedIndex = 0;
        }

        private void Top_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _convertController.ChangeTop(TopBank.SelectedIndex);
        }

        private void CurrencyFromConvert_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CurrencyFromConvert.SelectedItem == null)
                return;
            var from = CurrencyFromConvert.SelectedItem.ToString().ToUpper();
            _convertController.ConvertModel.CurrencyFrom = from;
            if(CurrencyToConvert.SelectedIndex == -1)
                CurrencyToConvert.SelectedIndex = 0;
            CurrencyPrice.Text = _convertController.ConvertModel.GetCurrencyBuy(from, CurrencyToConvert.SelectedItem.ToString().ToUpper());
            
            CurrencyToConvertValue.Text = _convertController.Convert().ToString("F2", CultureInfo.InvariantCulture);
        }

        private void CurrencyToConvert_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CurrencyToConvert.SelectedItem == null)
                return;
            var to = CurrencyToConvert.SelectedItem.ToString().ToUpper();
            _convertController.ConvertModel.CurrencyTo = to;
            if(CurrencyFromConvert.SelectedIndex == -1)
                CurrencyFromConvert.SelectedIndex = 0;
            CurrencyPrice.Text = _convertController.ConvertModel.GetCurrencyBuy(CurrencyFromConvert.SelectedItem.ToString().ToUpper(), to);
            
            CurrencyToConvertValue.Text = _convertController.Convert().ToString("F2", CultureInfo.InvariantCulture);
        }


        private void CurrencyFromConvertValue_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (CurrencyFromConvertValue.Text == "")
            {
                CurrencyToConvertValue.Text = "";
                return;
            }
            if(!double.TryParse(CurrencyFromConvertValue.Text, out var result))
            {
                CurrencyToConvertValue.Text = "";
                return;
            }
            _convertController.ConvertModel.ConvertValue = result;
            CurrencyToConvertValue.Text = _convertController.Convert().ToString("F2", CultureInfo.InvariantCulture);
        }

        private void Calculate_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _convertController.ChangeCalculate();
        }

        private void ChangePositions_OnClick(object sender, RoutedEventArgs e)
        {
            var currencyFrom = CurrencyFromConvert.SelectedItem;
            var currencyTo = CurrencyToConvert.SelectedItem;
            CurrencyFromConvert.SelectedItem = currencyTo;
            CurrencyToConvert.SelectedItem = currencyFrom;
            
            CurrencyFromConvertValue_OnTextChanged(sender, null);
        }
    }
}