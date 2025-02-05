﻿using System;
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
        }

        private void SetThumbnail(SubredditPostModel item)
        {
            Thumbnail.IsVisible = true;

            ImageSource imageSource;
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
                    imageSource = ImageSource.FromUri(new Uri(item.Thumbnail));
                    break;
            }

            Thumbnail.Source = imageSource;

            string url = item.Url != null
                ? item.Url.Replace("&amp;", "&")
                : item.Preview.Images.First().Source.Url.Replace("&amp;", "&");

            TapGestureRecognizer gesture = new TapGestureRecognizer();
            gesture.Tapped += (sender, eventArgs) => { Device.OpenUri(new Uri(url)); };

            Thumbnail.GestureRecognizers.Add(gesture);
        }
    }
}