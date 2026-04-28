using Microsoft.Extensions.DependencyInjection;
using VertHorisNaidis;

namespace MauiApp1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var startPage = new StartPage();
            var navPage = new NavigationPage(startPage)
            {
                BarBackgroundColor = Colors.Bisque,
                BarTextColor = Colors.White
            };

            return new Window(navPage);
        }
    }
}