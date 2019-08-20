﻿using Prism.Navigation;
using RhodeIT.Views;
using Syncfusion.XForms.TabView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class MainMenuTabViewModel : INotifyPropertyChanged
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
            get
            {
                return tabs;
            }
            set
            {
                if (value != Tabs)
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
                if (value != StartIndex)
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
                if (value != Count)
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
                if (value != mainTabView)
                {
                    tabView = value;
                    OnPropertyChanged(nameof(mainTabView));
                }
            }
        }
        /// <summary>
        /// Defualt constructor
        /// </summary>
        public MainMenuTabViewModel() 
        {
            setUp();
        }

        public void setUp()
        {
            main = new Grid { BackgroundColor = Color.White };
            Tabs = new TabItemCollection { new SfTabItem { Title = "Profile", Content = new UserProfile().Content }, new SfTabItem { Title = "Docking Stations", Content = new MapsTab().Content }, new SfTabItem { Title = "History", Content = new HistoryTab().Content } };
            Count = tabs.Count;
            tabView = new SfTabView() { SelectedIndex = startTabIndex, DisplayMode = TabDisplayMode.ImageWithText, EnableSwiping = true, BackgroundColor = Color.WhiteSmoke, Items = tabs, VisibleHeaderCount = count };
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
