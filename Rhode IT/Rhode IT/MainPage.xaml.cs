using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.XForms.TextInputLayout;
using Syncfusion.XForms.Buttons;

namespace Rhode_IT
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            StackLayout centerStack = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor=Color.Transparent
            };


            var inputLayoutStudentNo = new SfTextInputLayout();
            inputLayoutStudentNo.Hint = "Student no";
            inputLayoutStudentNo.ContainerType = ContainerType.Outlined;
            inputLayoutStudentNo.OutlineCornerRadius = 8;
            Entry studentNo = new Entry();
            inputLayoutStudentNo.InputView = studentNo;
            inputLayoutStudentNo.ErrorText = "Field cant be left blank";
            inputLayoutStudentNo.FocusedColor = Color.Black;
            var inputLayoutPassword = new SfTextInputLayout();
            inputLayoutPassword.Hint = "Password";
            inputLayoutPassword.ContainerType = ContainerType.Outlined;
            inputLayoutPassword.OutlineCornerRadius = 8;
            Entry password = new Entry();
            inputLayoutPassword.FocusedColor = Color.Black;
            inputLayoutPassword.InputView = password;
            inputLayoutPassword.ErrorText = "Field cant be left blank";
            inputLayoutPassword.EnablePasswordVisibilityToggle = true;
            TitleLabel.FontSize = 30;
            SfButton login = new SfButton();
            login.BackgroundColor = Color.DeepSkyBlue;
            login.Text = "Login";
            login.TextColor = Color.Black;
            main.Children.Add(centerStack);
            centerStack.Children.Add(inputLayoutStudentNo);
            centerStack.Children.Add(inputLayoutPassword);
            centerStack.Children.Add(login);
            Content = main;
        }
    }
}
