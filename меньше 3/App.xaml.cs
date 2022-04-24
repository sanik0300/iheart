using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace меньше_3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            App.Current = this;
            MainPage = new MainPage();

            RequestedThemeChanged += App_RequestedThemeChanged;
        }

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            (MainPage as MainPage).PerharpsChangeTheme();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
