using CryptoTool.ViewModels;
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
    /// Логика взаимодействия для DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        private string _from;
        private DetailsViewModel _viewModel;
        public DetailsPage(string from, DetailsViewModel viewModel)
        {
            InitializeComponent();
            _from = from;
            _viewModel = viewModel;
            DataContext = viewModel;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
