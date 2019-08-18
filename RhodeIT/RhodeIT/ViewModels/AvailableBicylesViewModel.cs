using RhodeIT.Databases;
using RhodeIT.Models;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class AvailableBicylesViewModel:INotifyPropertyChanged
    {
        SfListView listView;
        StackLayout mapsTab;
        RhodeITDB db;
        public event PropertyChangedEventHandler PropertyChanged;
        public Grid ListViewParent;
        public ICommand Close { get; set; }
        public string name { get; set; }
        ObservableCollection<Bicycle> availBicycles;
        Command ShowMenu;
        public ObservableCollection<Bicycle> AvailBicycles
        {
            set
            {
               if(value != availBicycles)
                {
                    availBicycles = value;
                    OnPropertyChanged(nameof(AvailBicycles));

                }
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if(value != name)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        public AvailableBicylesViewModel(string venue, Command show,Command closeMenu, Grid listView, StackLayout main)
        {
  
            mapsTab = main;
            Name = venue;
            ListViewParent = listView;
            Close = closeMenu;
            db = new RhodeITDB();
            ShowMenu = show;
            SetUp();
        }
        private void SetUp()
        {
            ObservableCollection<Bicycle> bikes = db.getAvailableBicycles(Name);
            listView = new SfListView();
            listView.ItemSize = 100;
            listView.ItemsSource = bikes;
            listView.ItemTemplate= new DataTemplate(() => {
                Grid temp = new Grid();
                Label BicyleName = new Label();
                BicyleName.SetBinding(Label.TextProperty, new Binding("BikeName"));
                Label model = new Label();
                model.SetBinding(Label.TextProperty, new Binding("Model"));
                Label status = new Label();
                status.SetBinding(Label.TextProperty, new Binding("Status"));
                temp.Children.Add(BicyleName);
                temp.Children.Add(model);
                temp.Children.Add(status);
                return temp;
            });
            ListViewParent.Children.Add(listView);
        }
        public void showMenu()
        {
            ShowMenu.Execute(null);
        }
        public void closeMenu()
        {
            Close.Execute(null);
        }
        /// <summary>
        /// On the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
