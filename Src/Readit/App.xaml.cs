using Readit.Views;
using System.Net.Http;
using Xamarin.Forms;

namespace Readit
{
    

    public partial class App
    {
        public App()
        {                      

            // Xamarin.Forms.Core
            MainPage = new NavigationPage();
        }

        protected override void OnStart()
        {
            // Инициализируем главную страницу с популярными постами
            var postView = new PostView();
            MainPage.Navigation.PushAsync(postView);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}