using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.Abstractions;
using JsonRpcSharp.Client;
using RhodeIT.Classes;
using RhodeIT.Databases;
using RhodeIT.Models;
using RhodeIT.Views;
using Syncfusion.XForms.Buttons;
using Syncfusion.XForms.TextInputLayout;
using System;
using System.ComponentModel;
using System.Reflection;
using Xamarin.Forms;

namespace RhodeIT.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private Entry studentNo, password;
        private SfTextInputLayout inputLayoutStudentNo, inputLayoutPassword;
        private StackLayout maincontent, PagelabelStackLayOut;
        private Label TitleLabel;
        private RhodeITDB db;
        private int fontSize = 30;//@dev defualt fontsize   
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly string[] Icons = new string[] { "Icon.png" };
        private  RhodeITSmartContract SmartContract;
        private CircleImage LoginPageIcon;
        public int FontSize
        {
            get => fontSize;
            private set
            {
                if (value != fontSize)
                {
                    fontSize = value;
                    OnPropertyChanged(nameof(FontSize));

                }
            }
        }
        public StackLayout Maincontent
        {
            get => maincontent;
            private set
            {
                if (value != maincontent)
                {
                    maincontent = value;
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
            Maincontent = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            PagelabelStackLayOut = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            TitleLabel = new Label
            {
                TextColor = Color.Black
            };
            inputLayoutStudentNo = new SfTextInputLayout
            {
                Hint = "Student no",
                FocusedColor = Color.Black,
                UnfocusedColor = Color.Black,
                ContainerType = ContainerType.Outlined,
                OutlineCornerRadius = 8
            };
            studentNo = new Entry();
            inputLayoutStudentNo.InputView = studentNo;
            inputLayoutStudentNo.ErrorText = "Field cant be left blank";
            inputLayoutStudentNo.FocusedColor = Color.Black;
            inputLayoutPassword = new SfTextInputLayout
            {
                Hint = "Password",
                ContainerType = ContainerType.Outlined,
                OutlineCornerRadius = 8
            };
            password = new Entry();
            inputLayoutPassword.FocusedColor = Color.Black;
            inputLayoutPassword.InputView = password;
            inputLayoutPassword.ErrorText = "Field cant be left blank";
            inputLayoutPassword.EnablePasswordVisibilityToggle = true;
            inputLayoutPassword.ShowCharCount = true;
            inputLayoutPassword.HelperText = "The password is similar to that used in all university systems i.e. ross";
            inputLayoutPassword.UnfocusedColor = Color.Black;
            inputLayoutPassword.ShowHelperText = true;
            TitleLabel.FontSize = 30;
            SfButton login = new SfButton
            {
                BackgroundColor = Color.DeepSkyBlue,
                Text = "Login",
                TextColor = Color.Black
            };
            //Assembly assembly = typeof(LoginPage).GetTypeInfo().Assembly;
            //string file = Icons[0];
            //string[] names = assembly.GetManifestResourceNames();
            LoginPageIcon = new CircleImage
            {
                HeightRequest = 100,
                WidthRequest = 100,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                Source = ImageSource.FromUri(new Uri("https://lh3.googleusercontent.com/m_1Pc3Sfge_muByZBS1KVgwvDYSQldlfmKkOV4QBqRwoyaf2zPQxRiXLjckmCctk-o1X=s85")),
                Opacity=100
            };
            PagelabelStackLayOut.Children.Add(LoginPageIcon);
            Maincontent.Children.Add(PagelabelStackLayOut);
            Maincontent.Children.Add(inputLayoutStudentNo);
            Maincontent.Children.Add(inputLayoutPassword);
            Maincontent.Children.Add(login);
            login.Clicked += Login_ClickedAsync;
            return maincontent;
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

            IUserDialogs dialog = UserDialogs.Instance;
            dialog.ShowLoading("Logging in please wait", MaskType.Gradient);

            try
            {

                // await test.LoginAsync(studentNo.Text, password.Text);
                Tuple<bool,LoginDetails> results = await rhodesDataBase.VerifyStudentAysnc(new LoginDetails { Password = password.Text, User_ID = studentNo.Text.ToLower() });
                ///@dev 

                /*
                 * @dev the below if statement checks if the student is valid if they are the second if statement checks if they have been registered on the platforms smart
                 * contract if they are usually the student object has a field called 'loginhash' if this field is not null it means the student has logged into the platform before
                 * if its null we register the student on the platform
                 */
                if (results.Item1)
                {
                    SmartContract.RegisterStudent(results.Item2);
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
            catch (LoginException E)
            {
                dialog.HideLoading();
                await dialog.AlertAsync(E.Message, "Login Error");

            }
            catch (RpcClientUnknownException)
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
        private void OnPropertyChanged(string propertyName)
        {
            //PropertyChangedEventHandler eventHandler = this.PropertyChanged;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
