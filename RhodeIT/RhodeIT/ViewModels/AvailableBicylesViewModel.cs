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
        private string Venue;
        SfListView listView;
        LocalDataBase db;
        Action CloseMenu;
        public event PropertyChangedEventHandler PropertyChanged;
        public StackLayout ListViewParent;
        public ICommand Close { get; set; }
        public string name { get; set; }
        ObservableCollection<Bicycle> availBicycles;
        public ObservableCollection<Bicycle> AvailBicyles
        {
            set
            {
               if(value != availBicycles)
                {
                    availBicycles = value;
                    OnPropertyChanged(nameof(AvailBicyles));

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
        public AvailableBicylesViewModel(string venue, Action closeMenu, StackLayout listView)
        {
            Venue = venue;
            Close = new Command(CloseMenu);
            ListViewParent = listView;
            CloseMenu = closeMenu;
           
            SetUp();
        }
        private void SetUp()
        {
            ObservableCollection<Bicycle> bikes = db.getAvailableBicycles(Venue);
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

        }
        public void closeMenu()
        {
            CloseMenu();
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
