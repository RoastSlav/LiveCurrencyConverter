using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCurrencyConverter.Library.Models;
using LiveCurrencyConverter.Library.Api;

namespace LiveCurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CurrencyEndpoint _currencyEndpoint;

        public MainWindow(CurrencyEndpoint currencyEndpoint)
        {
            InitializeComponent();
            _currencyEndpoint = currencyEndpoint;
            bindListBoxes();
        }


        private async void bindListBoxes()
        {
            var currencies = await _currencyEndpoint.GetAllCurrenciesAsync();
            currencies.Sort((x, y) => x.CurrencyName.CompareTo(y.CurrencyName));

            CurrenciesFrom.ItemsSource = currencies;
            CurrenciesTo.ItemsSource = currencies;
        }


        private async void ConvertCurrency_Click(object sender, RoutedEventArgs e)
        {
            Error.Text = "";
            if (CurrenciesTo.SelectedItem == null || CurrenciesFrom.SelectedItem == null)
            {
                Error.Text = "Both currencies must be selected!";
                return;
            }

            try
            {
                var exchangeRate = await _currencyEndpoint.GetExchangeRateAsync((CurrencyModel)CurrenciesFrom.SelectedItem,
                    (CurrencyModel)CurrenciesTo.SelectedItem);

                Converted.Text = (double.Parse(Quantity.Text) * exchangeRate).ToString();
            }
            catch
            {
                Error.Text = "Input not in a correct format!";
            }
            
        }
    }
}
