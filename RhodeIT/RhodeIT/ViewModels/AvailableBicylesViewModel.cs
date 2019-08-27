using RhodeIT.Models;
using RhodeIT.Services.RhodeIT;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using XF.Material.Forms.UI;

namespace RhodeIT.ViewModels
{

    public class AvailableBicylesViewModel : INotifyPropertyChanged
    {
        private SfListView listView;
        private readonly StackLayout mapsTab;
        private RhodeITService db;
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
            db = new RhodeITService();
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
            ObservableCollection<Bicycle> bikes = db.GetAvailableBicyclesFromDockingStation(Name);
            listView = new SfListView
            {
                ItemSize = 120,
                ItemsSource = new RidesViewModel().Rides,
                LayoutManager = new LinearLayout(),
                ItemTemplate = CreateUpcomingRidesDataTemplate()
            };
            ListViewParent.Children.Add(listView);
        }
        /// <summary>
        /// Creates the users upcoming user rides data template to be used to populate the 
        /// Listview
        /// </summary>
        /// <returns>DataTemplate</returns>
        private static DataTemplate CreateUpcomingRidesDataTemplate()
        {
            return new DataTemplate(() =>
            {
                Label bikeID = new Label { BackgroundColor = Color.Transparent, FontSize = 15 };
                Label bikeIDLabel = new Label { FontAttributes = FontAttributes.Bold, BackgroundColor = Color.Transparent, FontSize = 15, Text = "BikeID" };
                bikeID.SetBinding(Label.TextProperty, new Binding("ID"));
                SfButton rentOut = new SfButton
                {
                    Text = "RentOut",
                    BackgroundColor = Color.White,
                    TextColor = Color.Black,
                    CornerRadius = 20,
                    BorderColor = Color.Black,
                    BorderWidth = 1
                };
                StackLayout bikeIDParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            bikeIDLabel,bikeID
                        }
                };
                StackLayout rentOutButtonParent = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 0,
                    HeightRequest = 30,
                    Children = {
                            rentOut
                        }
                };
                MaterialCard parentInforCard = new MaterialCard
                {
                    Elevation = 5,
                    BackgroundColor = Color.White
                };
                StackLayout parentInfo = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Padding = 0
                };
                parentInfo.Children.Add(bikeIDParent);
                parentInfo.Children.Add(rentOutButtonParent);
                parentInforCard.Content = parentInfo;
                Frame cardFrame = new Frame
                {
                    WidthRequest = 120,
                    HeightRequest = 100,
                    CornerRadius = 5,
                    BackgroundColor = Color.WhiteSmoke,
                    Padding = 3,
                    Content = parentInforCard
                };
                return cardFrame;
            });
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
