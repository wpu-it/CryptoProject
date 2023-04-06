using CryptoTool.Models;
using CryptoTool.ViewModels;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
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

namespace CryptoTool.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindowPageStart.xaml
    /// </summary>
    public partial class MainWindowPageStart : Page
    {
        private MainViewModel viewModel;
        private string _from;

        public delegate void NavigationHandler(string destination, Asset asset);
        public event NavigationHandler NavigationRequested;

        public MainWindowPageStart(string from)
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            DataContext = viewModel;
            _from = from;
        }
        private async void ComboBoxExchanges_Selected(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var item = comboBox.SelectedItem as ComboBoxItem;
            string value = item.Name;
            await viewModel.GetData(value);
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as DataGrid;
            var item = grid.SelectedItem as Asset;
            NavigationRequested?.Invoke("DetailPage", item);
        }
    }
}
