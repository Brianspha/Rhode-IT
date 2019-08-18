using Acr.UserDialogs;
using Prism.Navigation;
using RhodeIT.Classes;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Views;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using JsonRpcSharp;
using JsonRpcSharp.Client;

namespace RhodeIT.ViewModels
{
    public class LoginPageViewModel:INotifyPropertyChanged
    {
        View content;
        Entry studentNo, password;
        SfTextInputLayout inputLayoutStudentNo, inputLayoutPassword;
        private DataTemplate templateView;
        StackLayout main;
        Label TitleLabel;
        RhodeITDB db;
        private int FontSize = 30;//@dev defualt fontsize   
        public event PropertyChangedEventHandler PropertyChanged;
        RhodeITSmartContract SmartContract;
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
            db = new RhodeITDB();
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
            SmartContract = new RhodeITSmartContract();
            RhodesDataBase rhodesDataBase = new RhodesDataBase();
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
                // await test.LoginAsync(studentNo.Text, password.Text);
                bool success = await rhodesDataBase.VerifyStudentAysnc(new LoginDetails { password = password.Text, userID = studentNo.Text.ToLower() });
                ///@dev 
               
                if (success)
                {
                    await SmartContract.Login(studentNo.Text, password.Text);
                    Console.WriteLine("Login succesfull");
                    dialog.HideLoading();
                    Application.Current.MainPage = new NavigationPage(new MainMenuTab());
                }
                else
                {
                    Console.WriteLine("Login in failure");
                    throw new Exception();
                }
             }
            catch(LoginException E)
            {
                dialog.HideLoading();
                await dialog.AlertAsync(E.Message, "Login Error");

            }
            catch(RpcClientUnknownException E)
            {
                Console.WriteLine("Blockchain node is not runnig");
                //@dev notify admin about error
                dialog.HideLoading();
                await dialog.AlertAsync("Something went wrong on our server we are working on fixing the problem please try again in a few minutes", "Internal error");
            }
            catch (Exception E)
            {

                Console.WriteLine("Error: ", E.Message);
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
