using PanCardView;
using Rhode_IT.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
namespace Rhode_IT.ViewModels
{
   public class MainMenuViewModel : INotifyPropertyChanged
    {
        bool AddedAlready = false;
        StackLayout main;
        public MainMenuViewModel()
        {
            SetUpViews();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public StackLayout getViews()
        {
       
            return main;
        }

        public void SetUpViews()
        {
            main = new StackLayout();
            main.HorizontalOptions = LayoutOptions.CenterAndExpand;
            main.VerticalOptions = LayoutOptions.CenterAndExpand;
            var cardsView = new CardsView
            {
                ItemTemplate = new DataTemplate(() =>new StudentView().GetCardItem()) //your template
            };
            main.Children.Add(cardsView);
          
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
