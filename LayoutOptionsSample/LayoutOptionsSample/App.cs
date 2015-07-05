using System;
using System.Collections.Generic;
using System.Linq;
using LayoutOptionsSample.Pages;
using Xamarin.Forms;

namespace LayoutOptionsSample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // The root page of your application
            var mainPage = new TabbedPage
            {
                Title = "LayoutOptions Demo",
            };

            mainPage.Children.Add(new Pages.StackLayouts.HorizontalPage());
            mainPage.Children.Add(new Pages.StackLayouts.VerticalPage());

            MainPage = mainPage;
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