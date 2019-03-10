using PanCardView;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rhode_IT.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StudentView : ContentPage
	{
        private StackLayout main;

        public StudentView ()
		{
			InitializeComponent ();
         main = new StackLayout();
        main.HorizontalOptions = LayoutOptions.CenterAndExpand;
        main.VerticalOptions = LayoutOptions.CenterAndExpand;
            Label studentDets = new Label();
            studentDets.Text = "Test Student";
            studentDets.TextColor = Color.NavajoWhite;
            studentDets.HorizontalOptions = LayoutOptions.CenterAndExpand;
            studentDets.VerticalOptions = LayoutOptions.CenterAndExpand;
            Label studentBalance = new Label();
            studentBalance.Text = "Test Student";
            studentBalance.TextColor = Color.NavajoWhite;
            studentBalance.HorizontalOptions = LayoutOptions.CenterAndExpand;
            studentBalance.VerticalOptions = LayoutOptions.CenterAndExpand;
            var buy = new SfButton();
            buy.Clicked += Buy_Clicked;
            buy.Text = "Purchase Ride credits";
            buy.BackgroundColor = Color.SkyBlue;
            buy.TextColor = Color.NavajoWhite;
            main.Children.Add(studentDets);
            main.Children.Add(studentBalance);
            main.Children.Add(buy);


        }

        private void Buy_Clicked(object sender, EventArgs e)
        {

        }
        public View GetCardItem()
        {
            var label = new Label
            {
                TextColor = Color.White,
                FontSize = 50,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };
            label.SetBinding(Label.TextProperty, "Number");
            AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(label, new Rectangle(.5, 1, 1, .5));

            var cardsView = new PanCardView.CarouselView
            {
                ItemTemplate = new DataTemplate(() =>
                {
                    var subCard = new ContentView();
                    subCard.SetBinding(BackgroundColorProperty, "Color");
                    return subCard;
                })
            };
            cardsView.SetBinding(CardsView.ItemsSourceProperty, "Items");
            cardsView.SetBinding(CardsView.SelectedIndexProperty, "CurrentIndex");

            AbsoluteLayout.SetLayoutFlags(cardsView, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(cardsView, new Rectangle(.5, 0, 1, .5));

            return new AbsoluteLayout
            {
                BackgroundColor = Color.Black,
                Children =
                {
                    cardsView,
                    main
                }
            };
        }
    }
}