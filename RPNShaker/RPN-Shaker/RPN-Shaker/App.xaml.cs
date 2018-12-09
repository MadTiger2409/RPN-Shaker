using System;
using RPN_Shaker.Pages;
using RPN_Shaker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RPN_Shaker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ShakeListenerNavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
