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

        public CommentView(string commentPermalink)
        {
            _presenter = new CommentPresenter(this);
            _navigator = new CommentNavigator(this);

            //RnD
            SubscribeToMessages();

            InitializeComponent();

            SearchItem.Clicked += (sender, args) => _navigator.ShowSearchScreen();

            Comments = new ObservableCollection<PostsCommentModel>();
            //CommentListView.ItemsSource = Comments;
            
            CommentListView.ItemsSource = Comments;
            _presenter.UpdateComments(commentPermalink);

            //RequestUpdate();
        }

        private ObservableCollection<PostsCommentModel> Comments { get; }


        //ObservableCollection<List<PostsCommentModel>>();

        // AddComments
        public void AddComments(List<PostsModel> models)
        {
            foreach (PostsModel model in models)
            {
                //List<PostsCommentModel> commentList = new List<PostsCommentModel>();

                foreach (PostsChildrenModel childrenModel in model.Data.Children)
                {
                    //RnD : if (childrenModel.Kind == "t1")
                    if ((childrenModel.Kind == "t3") || (childrenModel.Kind == "t1"))
                    {
                        childrenModel.Kind = "k";
                        childrenModel.Data.Author = childrenModel.Kind;//"[Author]";
                        childrenModel.Data.Body = "b";
                        childrenModel.Data.Created = "c";
                        //childrenModel.Data.Replies = "Replies";

                        //commentList.Add(childrenModel.Data);
                        Comments.Add(childrenModel.Data);
                    }
                }

                /*
                if (commentList.Count > 0)
                {
                    foreach (var comment in commentList)
                    {
                        PostsCommentModel item = default;

                        item.Author = comment.Author;
                        item.Body = comment.Body;
                        //item.Kind = comment.Kind;
                        item.Created = comment.Created;
                        item.Replies = default;
                        Comments.Add(item);
                    }
                    
                    //Comments.Add(commentList);
                }
                */
            }//foreach..
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
    }
}