using Nethereum.Web3;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Helpers;
using System.ComponentModel;
using System.Numerics;
using System.Windows;

namespace RhodeITManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTE3MjNAMzEzNzJlMzEyZTMwUUU4Q3NGUitCN0ZVQ09yTmxReDV3VlBRZ0xWNUdOdEJNYmoxWjhNNTh2bz0=;OTE3MjRAMzEzNzJlMzEyZTMwbjJUSnlaTkFLUGdKYXdhYjBNeHBsQUNhcThGVFNvZndoL1owQ0tTTUJIbz0=;OTE3MjVAMzEzNzJlMzEyZTMwWVpzcGM2RFZKUXN2VUh6eHRRNzVvbjIrV1Uzck1jS0wva2ZFcEpnaUFUQT0=;OTE3MjZAMzEzNzJlMzEyZTMwRlNSU2R2NjlHNkZ4T0I2b3ZDSnJ3V2YzMlpmL1Z5VWpWbVNiOFZJNGp4MD0=;OTE3MjdAMzEzNzJlMzEyZTMwT2dlRzFrRFJxTnY2bnhBSGVjQVpocEhjWFVhbzVDQmxLU0VLVGEwbXptRT0=;OTE3MjhAMzEzNzJlMzEyZTMwWE40LzRkN1ZqNnFNUms5QlRkSDVLN2ZsblNvNy92YnlrWnZRVXdXNU16ST0=;OTE3MjlAMzEzNzJlMzEyZTMwV1ppeDRIdUkwWkdNa1ZtMFhjaVlSVDZzT1JIMFk5K3VZN2hIRkdxVjB5VT0=;OTE3MzBAMzEzNzJlMzEyZTMwVFVqNjJRSUFqcUNTVVJVOWdCU1Z2Sy9kVDBxUU9yQTJTNGdjdjY0N3Uzdz0=;OTE3MzFAMzEzNzJlMzEyZTMwSDZpQnpWZUhKbW5xdkxjMEFqZTFYbTE3SHIwVStVUmNUWW9PMHVURWtWZz0=;OTE3MzJAMzEzNzJlMzEyZTMwT2dlRzFrRFJxTnY2bnhBSGVjQVpocEhjWFVhbzVDQmxLU0VLVGEwbXptRT0=");
            SfDataGrid dataGrid = new SfDataGrid();
            RhodeITDB db = new RhodeITDB();
            var helper = new ResourceHelper();
            GEOJSONParser temp = new GEOJSONParser(helper.Process(), helper.GetParsedVenuesWithSubjects());
            dataGrid.ItemsSource = db.GetAllVenueLocations();
            dataGrid.FilterRowPosition = FilterRowPosition.FixedTop;
            dataGrid.AllowEditing = true;
            dataGrid.AddNewRowPosition = AddNewRowPosition.Top;
            var addNewRowController = dataGrid.GetAddNewRowController();
            int addNewRowIndex = addNewRowController.GetAddNewRowIndex();
            dataGrid.AddNewRowPosition = AddNewRowPosition.Top;
            dataGrid.NewItemPlaceholderPosition = NewItemPlaceholderPosition.AtBeginning;
            dataGrid.SearchHelper.AllowFiltering = true;
            grid.Children.Add(dataGrid);
           
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var web3 = new Web3("HTTP://146.231.123.137:11001");
            web3.TransactionManager.DefaultGas = new BigInteger(8000000);
            web3.TransactionManager.DefaultGasPrice = new BigInteger(20000000000);
            RhodeITService rhodeITService = new RhodeITService(web3, "0x2ca1a0ab4db77f821117227aaac153023908c4c7");

        }
    }
}
