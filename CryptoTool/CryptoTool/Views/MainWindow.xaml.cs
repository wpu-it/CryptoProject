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
using System.Windows.Shapes;

namespace CryptoTool.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        private DetailsViewModel detailsViewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            viewModel.NavigationRequested += Navigate;
            detailsViewModel = new DetailsViewModel();
            frame.Navigate(new MainWindowPageStart("start", viewModel));
        }

        public void Navigate(string dest)
        {
            switch (dest)
            {
                case "MainWindowPageStart":
                    frame.Navigate(new MainWindowPageStart("start", viewModel));
                    break;
                case "DetailsPage":
                    frame.Navigate(new DetailsPage("start", detailsViewModel));
                    break;
            }
        }
    }
}
