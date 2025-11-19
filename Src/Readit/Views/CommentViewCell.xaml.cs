using Readit.Models;
using System;

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
            Author.Text = item.Author;
            Body.Text = item.Body;
            
            // Преобразуем время создания в читаемый формат
            if (double.TryParse(item.Created, out double createdTime))
            {
                var dateTime = DateTimeOffset.FromUnixTimeSeconds((long)createdTime);
                var timeSpan = DateTimeOffset.Now - dateTime;
                
                if (timeSpan.Days > 0)
                    TimeSincePosted.Text = $"{timeSpan.Days}d ago";
                else if (timeSpan.Hours > 0)
                    TimeSincePosted.Text = $"{timeSpan.Hours}h ago";
                else if (timeSpan.Minutes > 0)
                    TimeSincePosted.Text = $"{timeSpan.Minutes}m ago";
                else
                    TimeSincePosted.Text = "Just now";
            }
            else
            {
                TimeSincePosted.Text = "";
            }
        }
    }
}