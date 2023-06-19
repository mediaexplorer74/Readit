using Readit.Models;

namespace Readit.Views
{
    public partial class CommentViewCell
    {
        public CommentViewCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (!(BindingContext is PostsCommentModel item))
                return;

            SetTextViews(item);
        }

        private void SetTextViews(PostsCommentModel item)
        {
            Title.Text = "Test";
            Body.Text = item.Body;
            Author.Text = item.Author;
            Replies.Text= ( item.Replies == null ? "" : item.Replies.ToString() );
            TimeSincePosted.Text = item.Created;
        }
    }
}