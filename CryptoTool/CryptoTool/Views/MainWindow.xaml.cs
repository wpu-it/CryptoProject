using CryptoTool.Models;
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
        public MainWindow()
        {
            InitializeComponent();
            var startPage = new MainWindowPageStart("start");
            startPage.NavigationRequested += Navigate;
            frame.Navigate(startPage);
        }

        public void Navigate(string dest, Asset asset)
        {
            switch (dest)
            {
                case "DetailPage":
                    frame.Navigate(new DetailsPage("start", asset));
                    break;
            }
        }
    }
}
