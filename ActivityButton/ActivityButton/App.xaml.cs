using System;
using Xamarin.Forms;

namespace ActivityButton
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            StartApp();
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        public void StartApp()
        {
            var bootstrapper = new AppBootstrapper();
            MainPage = bootstrapper.CreateMainPage();
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }
    }
}