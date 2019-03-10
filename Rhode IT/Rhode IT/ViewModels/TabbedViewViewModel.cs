using Rhode_IT.Views;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Rhode_IT.ViewModels
{
   public class TabbedViewViewModel:INotifyPropertyChanged
    {
        private int startTabIndex { get; set; }
        public Grid main { get; private set; }
        private TabItemCollection tabs { set; get; }
        private int count { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        private SfTabView tabView;

        public TabItemCollection Tabs
        {
            get
            {
                return tabs;
            }
            set
            {
                if(value != Tabs)
                {
                    tabs = value;
                    Count = tabs.Count;
                    OnPropertyChanged(nameof(Tabs));
                }
            }

        }
        public int StartIndex
        {
            get
            {
                return startTabIndex;
            }
            set
            {
                if(value != StartIndex)
                {
                    startTabIndex = value;
                    mainTabView.SelectedIndex = startTabIndex;
                    OnPropertyChanged(nameof(StartIndex));
                    
                }
            }
        }
        public int Count
        {
            get
            {
                return count;
            }
            set
            {
                if(value != Count)
                {
                    count = value;
                    OnPropertyChanged(nameof(Count));
                }
            }
        }
        public SfTabView mainTabView
        {
            get
            {
                return tabView;
            }
            set
            {
                if(value != mainTabView)
                {
                    tabView = value;
                    OnPropertyChanged(nameof(mainTabView));
                }
            }
        }
        /// <summary>
        /// Defualt constructor
        /// </summary>
        public TabbedViewViewModel()
        {
            startTabIndex = 0;
            setUp();
        }
       /// <summary>
        /// sets the index of the main tab 
        /// </summary>
        /// <param name="index">represents the index of the tab to be set as main</param>
        public TabbedViewViewModel(int index)
        {
            //@dev ensure that index is not less than 0 i.e. invalid
            if(index <0)
            {
                return;
            }
            StartIndex = index;
            setUp(tabs);
        }

        private void setUp(TabItemCollection tabs)
        {
            main = new Grid { BackgroundColor = Color.White };
            Count = tabs.Count;
            tabView = new SfTabView() { SelectedIndex = startTabIndex, DisplayMode = TabDisplayMode.ImageWithText, EnableSwiping = true, BackgroundColor = Color.WhiteSmoke, Items = tabs, VisibleHeaderCount = count };
            tabView.SelectionIndicatorSettings.Position = SelectionIndicatorPosition.Top;
            tabView.SelectionIndicatorSettings.Color = Color.SkyBlue;
            tabView.TabHeaderPosition = TabHeaderPosition.Bottom;
            main.Children.Add(tabView);
        }

        public void setUp()
        {
            main = new Grid { BackgroundColor = Color.White };
            Tabs = new TabItemCollection { new SfTabItem { Title = "Account", Content = new MainMenuTab().Content }, new SfTabItem { Title = "Connect to Station", Content = new ConnectToPlatformTab().Content }, new SfTabItem { Title = "History", Content = new MapsTab().Content } };
            Count = tabs.Count;
            tabView = new SfTabView() { SelectedIndex=startTabIndex,DisplayMode = TabDisplayMode.ImageWithText, EnableSwiping = true, BackgroundColor = Color.WhiteSmoke, Items = tabs , VisibleHeaderCount = count };
            tabView.SelectionIndicatorSettings.Position = SelectionIndicatorPosition.Top;
            tabView.SelectionIndicatorSettings.Color = Color.SkyBlue;
            tabView.TabHeaderPosition = TabHeaderPosition.Bottom;
            main.Children.Add(tabView);
        }
        /// <summary>
        /// Adds new tab to the tab collection
        /// </summary>
        /// <param name="newtab">Tab to be added</param>
        public void addTab(SfTabItem newtab)
        {
            Tabs.Add(newtab);
            Count = Tabs.Count;
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
