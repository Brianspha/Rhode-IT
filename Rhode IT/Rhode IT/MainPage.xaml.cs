using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.TextInputLayout;
using Syncfusion.XForms.Buttons;
using Rhode_IT.Views;
using Rhode_IT.ViewModels;
using TK.CustomMap.Api.Google;
using Syncfusion.SfBusyIndicator.XForms;
using Syncfusion.XForms.PopupLayout;
using Rg.Plugins.Popup.Extensions;
using Acr.UserDialogs;
using Rhode_IT.Helpers;

namespace Rhode_IT
{
    public partial class MainPage : ContentPage
    {
        LoginViewModel loginViewModel;
        ConnectivityChecker connectivityChecker;
        public MainPage()
        {
            InitializeComponent();
            connectivityChecker = new ConnectivityChecker();
            loginViewModel = new LoginViewModel(main, TitleLabel,Content,Navigation);
            GmsDirection.Init("AIzaSyCjMY_194mgeHLsyhlPre7kZ-UVXHCCt0o");
            BindingContext = loginViewModel;
        }
        
    }
}
