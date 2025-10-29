using Readit.Contracts;
using Readit.Views;
using Xamarin.Forms;

namespace Readit.Navigators
{
    public class CommentNavigator : CommentContract.INavigator
    {
        private readonly CommentContract.IView _view;

        public CommentNavigator(CommentContract.IView view)
        {
            _view = view;
        }

        public async void ShowSearchScreen()
        {
            var searchView = new SearchView();
            await Application.Current.MainPage.Navigation.PushAsync(searchView);
        }

        public async void ShowCommentScreen(string commentPermalink)
        {
            CommentView commentView = new CommentView(commentPermalink);
            await Application.Current.MainPage.Navigation.PushAsync(commentView);
        }
    }
}