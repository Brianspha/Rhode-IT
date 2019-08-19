using Acr.UserDialogs;
using Prism.Navigation;
using Rhode_IT.Databases;
using Rhode_IT.Models;
using Rhode_IT.Views;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Rhode_IT.ViewModels
{
    public class LoginPageViewModel:INotifyPropertyChanged
    {
        View content;
        Entry studentNo, password;
        SfTextInputLayout inputLayoutStudentNo, inputLayoutPassword;
        private DataTemplate templateView;
        StackLayout main;
        Label TitleLabel;
        MainDataBase db;
        private int FontSize = 30;//@dev defualt fontsize   
        public event PropertyChangedEventHandler PropertyChanged;
        public int fontSize
        {
            get
            {
                return FontSize;
            }
            private set
            {
                if(value != FontSize)
                {
                    FontSize = value;
                    OnPropertyChanged(nameof(FontSize));

                }
            }
        }
        public StackLayout Maincontent
        {
            get
            {
                return setUpView();
            }
            private set
            {
                if (value != Maincontent)
                { 
                OnPropertyChanged(nameof(Maincontent));
                }
             }
        }

        public LoginPageViewModel()
        {
            setUpView();
        }
  
        public StackLayout setUpView()
        {
            db = new MainDataBase();
            main = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.Transparent
            };
            TitleLabel = new Label();
            inputLayoutStudentNo = new SfTextInputLayout();
            inputLayoutStudentNo.Hint = "Student no";
            inputLayoutStudentNo.ContainerType = ContainerType.Outlined;
            inputLayoutStudentNo.OutlineCornerRadius = 8;
            studentNo = new Entry();
            inputLayoutStudentNo.InputView = studentNo;
            inputLayoutStudentNo.ErrorText = "Field cant be left blank";
            inputLayoutStudentNo.FocusedColor = Color.Black;
            inputLayoutPassword = new SfTextInputLayout();
            inputLayoutPassword.Hint = "Password";
            inputLayoutPassword.ContainerType = ContainerType.Outlined;
            inputLayoutPassword.OutlineCornerRadius = 8;
            password = new Entry();
            inputLayoutPassword.FocusedColor = Color.Black;
            inputLayoutPassword.InputView = password;
            inputLayoutPassword.ErrorText = "Field cant be left blank";
            inputLayoutPassword.EnablePasswordVisibilityToggle = true;
            TitleLabel.FontSize = 30;
            SfButton login = new SfButton();
            login.BackgroundColor = Color.DeepSkyBlue;
            login.Text = "Login";
            login.TextColor = Color.Black;
            main.Children.Add(inputLayoutStudentNo);
            main.Children.Add(inputLayoutPassword);
            main.Children.Add(login);
            login.Clicked += Login_ClickedAsync;
            return main;
        }

        private async void Login_ClickedAsync(object sender, EventArgs e)
        {
            TestClass test = new TestClass();
            if (string.IsNullOrEmpty(studentNo.Text))
            {
                inputLayoutStudentNo.HasError = true;
                return;
            }
            if (string.IsNullOrEmpty(password.Text))
            {
                inputLayoutPassword.HasError = true;
                return;
            }
            var dialog = UserDialogs.Instance;
            dialog.ShowLoading("Logging in please wait",MaskType.Gradient);
          
            try
            {
                await test.deployAsync(studentNo.Text, password.Text);
                dialog.HideLoading();
                db.saveLogins(new LoginDetails { userID = studentNo.Text, password = password.Text });
                Application.Current.MainPage = new NavigationPage(new MainMenuTab());
                
             }
            catch (Exception E)
            {
                await dialog.AlertAsync("Something went wrong please ensure the correct details were used to login", "OOPS");
                dialog.HideLoading();
            }
        }

        /// <summary>
        /// Ons the property changed.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
        void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
