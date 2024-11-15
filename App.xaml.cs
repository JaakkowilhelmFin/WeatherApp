/*public App()
{
    InitializeComponent();

    MainPage = new NavigationPage(new LandingPage());
}*/


using Microsoft.Maui.Controls;

namespace WeatherApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LandingPage());
        }
    }
}

