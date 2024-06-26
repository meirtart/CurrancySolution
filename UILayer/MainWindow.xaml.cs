using BusinessLayer.Services;
using DatalLayer.Repository;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UILayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
        private readonly TradingService _tradingService;
        
        private Timer _refreshTimer;
        public MainWindow()
        {
            InitializeComponent();
            _tradingService = new TradingService(); // initilize service

            StartRefreshing();
        }

        /// <summary>
        /// start timer that runs every 2 seconds and executes RefreshData
        /// </summary>
        private void StartRefreshing()  
        {
            _refreshTimer = new Timer(RefreshData, null, 0, 2000);
        }

        /// <summary>
        /// Timer runs on its own thread, update the GUI thread using Dispatcher.Invoke
        /// the update re-read the currencypair list from TradingService into the grid
        /// </summary>
        /// <param name="state"></param>
        private void RefreshData(object state)
        {
            Dispatcher.Invoke(() =>
            {

                CurrencyDataGrid.ItemsSource = _tradingService.GetCurrencyPairs();
            });
        }

        // UILayer        BusinessLayer           DataLayer           SQLServer
        // MainWindow -> _tradingService -> repository -> DbContext ->   DB

    }
}