using Prism.Navigation;
using RhodeIT.Views;
using Syncfusion.XForms.TabView;
using System.ComponentModel;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class MainMenuTabsViewModel : INotifyPropertyChanged
    {
        private int startTabIndex { get; set; }
        public Grid main { get; private set; }
        private TabItemCollection tabs { set; get; }
        private int count { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private SfTabView tabView;
        public INavigationService Navigation { get; }
        public string Title = "Rhode IT";
        public TabItemCollection Tabs
        {
            get => tabs;
            set
            {
                if (value != tabs)
                {
                    tabs = value;
                    Count = tabs.Count;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Tabs)));
                }
            }

        }
        public int StartIndex
        {
            get => startTabIndex;
            set
            {
                if (value != StartIndex)
                {
                    startTabIndex = value;
                    MainTabView.SelectedIndex = startTabIndex;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(StartIndex)));

                }
            }
        }
        public int Count
        {
            get => count;
            set
            {
                if (value != Count)
                {
                    count = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                }
            }
        }
        public SfTabView MainTabView
        {
            get => tabView;
            set
            {
                if (value != tabView)
                {
                    tabView = value;
                    OnPropertyChanged(new PropertyChangedEventArgs(nameof(MainTabView)));
                }
            }
        }
        /// <summary>
        /// Defualt constructor
        /// </summary>
        public MainMenuTabsViewModel()
        {
            SetUp();
        }

        /// <summary>
        /// Sets up the main menu tabs view with its contents
        /// </summary>
        public void SetUp()
        {
            main = new Grid { BackgroundColor = Color.White };
            Tabs = new TabItemCollection { new SfTabItem { Title = "Profile", Content = new UserProfile().Content}, new SfTabItem { Title = "Docking Stations", Content = new MapsTab().Content, ImageSource = "https://banner2.kisspng.com/20180327/ziq/kisspng-computer-icons-user-profile-avatar-profile-5ab9c9868b8c84.1893767815221251905716.jpg" }, new SfTabItem { Title = "History", Content = new HistoryTab().Content, ImageSource = "https://banner2.kisspng.com/20180327/ziq/kisspng-computer-icons-user-profile-avatar-profile-5ab9c9868b8c84.1893767815221251905716.jpg" } };
            Count = tabs.Count;
            tabView = new SfTabView() { SelectedIndex = startTabIndex, DisplayMode = TabDisplayMode.ImageWithText, EnableSwiping = true, BackgroundColor = Color.WhiteSmoke, Items = tabs, VisibleHeaderCount = count };
            tabView.SelectionIndicatorSettings.Position = SelectionIndicatorPosition.Top;
            tabView.SelectionIndicatorSettings.Color = Color.SkyBlue;
            tabView.TabHeaderPosition = TabHeaderPosition.Bottom;
            main.Children.Add(tabView);
        }


        /// <summary>
        /// Invoked when a property is assigned a new value
        /// </summary>
        /// <param name="e"> property thats changed</param>
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
    }
}
