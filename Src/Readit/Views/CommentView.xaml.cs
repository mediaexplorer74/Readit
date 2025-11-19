using System.Collections.Generic;
using System.Collections.ObjectModel;
using Readit.Contracts;
using Readit.Models;
using Readit.Navigators;
using Readit.Presenters;
using Xamarin.Forms;

namespace Readit.Views
{
    public partial class CommentView : CommentContract.IView
    {
        private readonly CommentContract.INavigator _navigator;
        private readonly CommentContract.IPresenter _presenter;
        private PostsCommentModel _post; // holds t3 post for header

        public CommentView(string commentPermalink)
        {
            _presenter = new CommentPresenter(this);
            _navigator = new CommentNavigator(this);

            //RnD
            SubscribeToMessages();

            InitializeComponent();

            SearchItem.Clicked += (sender, args) => _navigator.ShowSearchScreen();

            Comments = new ObservableCollection<PostsCommentModel>();
            
            CommentListView.ItemsSource = Comments;
            _presenter.UpdateComments(commentPermalink);

            //RequestUpdate();
        }

        private ObservableCollection<PostsCommentModel> Comments { get; }


        //ObservableCollection<List<PostsCommentModel>>();

        // AddComments
        public void AddComments(List<PostsModel> models)
        {
            // Reddit returns an array: [0] = post (t3), [1] = comments (t1)
            if (models == null || models.Count == 0) return;

            // Extract post (t3) as header if available
            var first = models[0];
            if (first?.Data?.Children != null)
            {
                foreach (var child in first.Data.Children)
                {
                    if (child.Kind == "t3")
                    {
                        _post = child.Data;
                        SetHeader(_post);
                        break;
                    }
                }
            }

            // Extract comments (t1) from the rest of arrays
            int commentCount = 0;
            //const int maxComments = 10; // Ограничиваем количество комментариев до 10
            
            for (int i = 1; i < models.Count; i++)
            {
                var model = models[i];
                if (model?.Data?.Children == null) continue;

                foreach (var child in model.Data.Children)
                {
                    if (child.Kind == "t1")// && commentCount < maxComments)
                    {
                        Comments.Add(child.Data);
                        commentCount++;
                    }
                }
                
                //if (commentCount >= maxComments) break; // Прекращаем, если достигли лимита
            }
        }//AddComments

        private void SubscribeToMessages()
        {
            MessagingCenter.Subscribe<CommentViewCell, string>
            (
                this, "CommentClicked", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        // Delete last char of string 
                        arg = arg.TrimEnd('/');

                        _navigator.ShowCommentScreen(arg);
                    }
                });

            MessagingCenter.Subscribe<SearchView, string>(this,
                "UpdateComment", (sender, arg) =>
                {
                    if (arg != null)
                    {
                        RequestUpdate(arg, true);
                    }
                });
        }

        //RnD
        private void RequestUpdate(string comment = "comment", bool clearList = false)
        {
            SetTitle(comment);

            if (clearList)
            {
                Comments.Clear();
            }

            _presenter.UpdateComments(comment);
        }


        private void SetTitle(string title)
        {
            if (title == "")
            {
                title = "Comments Page";
            }
            Title = title;
        }

        private void SetHeader(PostsCommentModel post)
        {
            if (post == null) return;

            PostTitle.Text = post.Title;
            PostAuthor.Text = $"by {post.Author}";
            PostBody.Text = string.IsNullOrWhiteSpace(post.Selftext) ? "No text content" : post.Selftext;
        }
    }
}