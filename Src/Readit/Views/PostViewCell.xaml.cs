using System;
using System.Diagnostics;
using System.Linq;
using Readit.Models;
using Xamarin.Forms;

namespace Readit.Views
{
    public partial class PostViewCell
    {
        public PostViewCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (!(BindingContext is SubredditPostModel item)) return;

            TapGestureRecognizer gesture = new TapGestureRecognizer();
            
            gesture.Tapped += (sender, eventArgs) => 
            { 
                MessagingCenter.Send(this, "PostClicked", item.Permalink); 
            };

            Content.GestureRecognizers.Add(gesture);

            SetTextViews(item);
            if (!item.Self) SetThumbnail(item);
        }

        private void SetTextViews(SubredditPostModel item)
        {
            Title.Text = item.Title;
            Subreddit.Text = item.Subreddit;
            Author.Text = item.Author;

            // DEBUG only
            URL.Text = item.Url;
        }

        private void SetThumbnail(SubredditPostModel item)
        {
            Thumbnail.IsVisible = true;

            ImageSource imageSource = default;
            switch (item.Thumbnail)
            {
                case "":
                    imageSource = ImageSource.FromResource(
                        "Readit.Resources.Images.icon_link.png");
                    break;
                case "default":
                    imageSource = ImageSource.FromResource(
                        "Readit.Resources.Images.icon_link.png");
                    break;
                case "nsfw":
                    imageSource = ImageSource.FromResource(
                        "Readit.Resources.Images.icon_nsfw.png");
                    break;
                default:
                    try
                    {
                        imageSource = ImageSource.FromUri(new Uri(item.Thumbnail));
                    }
                    catch (Exception ex) 
                    {
                        Debug.WriteLine("[ex] Set SetThumbnail error: " + ex.Message);
                    }

                    break;
            }

            Thumbnail.Source = imageSource;

            string url = item.Url != null
                ? item.Url.Replace("&", "&")
                : item.Preview?.Images?.FirstOrDefault()?.Source?.Url?.Replace("&", "&") ?? item.Url;

            TapGestureRecognizer gesture = new TapGestureRecognizer();
            gesture.Tapped += (sender, eventArgs) => { Device.OpenUri(new Uri(url)); };

            Thumbnail.GestureRecognizers.Add(gesture);
        }
    }
}