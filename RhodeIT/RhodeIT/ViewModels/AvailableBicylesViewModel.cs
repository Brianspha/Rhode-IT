using Acr.UserDialogs;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{

    public class AvailableBicylesViewModel : INotifyPropertyChanged
    {
        private SfListView listView;
        private RhodeITService SmartContract;
        public event PropertyChangedEventHandler PropertyChanged;

        private StackLayout listViewParent;
        public StackLayout ListViewParent
        {
            get => listViewParent;
            set
            {
                if (value != listViewParent)
                {
                    listViewParent = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(ListViewParent)));
                }
            }
        }
        public ICommand Close { get; set; }
        public string name { get; set; }

        private ObservableCollection<Bicycle> availBicycles;
        private Command ShowMenu;
        public ObservableCollection<Bicycle> AvailBicycles
        {
            set
            {
                if (value != availBicycles)
                {
                    availBicycles = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(AvailBicycles)));

                }
            }
        }
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        private ObservableCollection<Bicycle> bikes;
        public ObservableCollection<Bicycle> Bikes
        {
            get => bikes;
            set
            {
                if (value != bikes)
                {
                    bikes = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Bikes)));
                }
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="venue"> The name of the venue which the pull up is to populate its listview with available bicycles</param>
        /// <param name="show"> The method which will be used to show the PullUp menu</param>
        /// <param name="closeMenu">The method which will be used to close the PullUp menu </param>
        /// <param name="listView">The GridLayout which is a parent of the pullup menu</param>
        /// <param name="main"></param>
        public AvailableBicylesViewModel(string venue, Command show, Command closeMenu, StackLayout listView)
        {
            Name = venue;
            ListViewParent = listView;
            Close = closeMenu;
            SmartContract = new RhodeITService();
            ShowMenu = show;
            SetUp();
        }
        /// <summary>
        /// Sets up the view for the pull up menu when a particular docking is clicked on the map
        ///A Listview is used to poplate the pull up menu
        ///Each item within the Listview is templated in a Material Card View
        ///The Material CardView is wrapped in a Frame the material card view doesnt render elements well using a Frame fixes this issue
        /// </summary>
        private void SetUp()
        {
            UpdateBicycleListAsync().Wait();
            listView = new SfListView
            {
                ItemSize = 120,
                ItemsSource = bikes,
                LayoutManager = new LinearLayout(),
                ItemTemplate = AvailablesBicyclesDataTemplate()
            };
            listView.ItemTapped += ListView_ItemTapped;
            ListViewParent.Children.Add(listView);
        }
        /// <summary>
        /// Creates the users upcoming user rides data template to be used to populate the 
        /// Listview
        /// </summary>
        /// <returns>DataTemplate</returns>
        private static DataTemplate AvailablesBicyclesDataTemplate()
        {
            return new DataTemplate(() =>
            {
                Label bikeID = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label bikeIDLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "BikeID" };
                bikeID.SetBinding(Label.TextProperty, new Binding("ID"));
                StackLayout bikeIDParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 50,
                    Children = {
                            bikeIDLabel,bikeID
                        }
                };
                MaterialCard parentInforCard = new MaterialCard
                {
                    Elevation = 5,
                    BackgroundColor = Color.White,
                    Content = bikeIDParent
                };
                Frame cardFrame = new Frame
                {
                    HeightRequest = 100,
                    CornerRadius = 5,
                    BackgroundColor = Color.WhiteSmoke,
                    Padding = 3,
                    Content = parentInforCard
                };
                return cardFrame;
            });
        }

        private void ListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            Bicycle bicycle = e.ItemData as Bicycle;
            RhodeITDB tempDB = new RhodeITDB();
            LoginDetails details = tempDB.GetUserDetails();
            bicycle.renter = details.Ethereum_Address;
            IUserDialogs dialog = UserDialogs.Instance;
            dialog.ShowLoading("Renting Out bicycle...");
            ConfiguredTaskAwaitable<bool> success = SmartContract.RentBicycleRequestAndWaitForReceiptAsync(bicycle).ConfigureAwait(false);
            if (success.GetAwaiter().GetResult())
            {
                dialog.HideLoading();
                UpdateBicycleListAsync().Wait();
                dialog.Toast("Success!!");
                dialog.Alert("Successfully rented out bicycle", "Success", "OK");
            }
            else
            {
                dialog.HideLoading();
                dialog.Toast("Error!!");
                dialog.Alert("Something went wrong whilst processing bicycle please ensure you have enough ride credits", "Error", "OK");
            }
        }
        private async Task UpdateBicycleListAsync()
        {
            ObservableCollection<Bicycle> tempBikes = await SmartContract.GetAvailableBicyclesFromDockingStationAsync(Name).ConfigureAwait(false);
            Bikes = new ObservableCollection<Bicycle>();
            foreach (Bicycle bicycle in tempBikes)
            {
                if (bicycle.DockdeAt == Name)
                {
                    Bikes.Add(bicycle);
                }
            }
        }

        /// <summary>
        /// Shows the Pull up menu when the user pulls up the menu
        /// </summary>
        public void showMenu()
        {
            ShowMenu.Execute(null);
        }
        /// <summary>
        /// Closes the Pull Up menu when the user clicks on the close button
        /// </summary>
        public void closeMenu()
        {
            Close.Execute(null);
        }
        /// <summary>
        /// Invloked when any property is assigned a new value.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            PropertyChanged?.Invoke(this, e);
        }
    }
}
