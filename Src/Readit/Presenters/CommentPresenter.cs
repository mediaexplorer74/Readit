using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Readit.Contracts;
using Readit.Models;

namespace Readit.Presenters
{
    public class CommentPresenter : CommentContract.IPresenter
    {
        private readonly CommentContract.IView _view;

        public CommentPresenter(CommentContract.IView view)
        {
            _view = view;
        }

        public async void UpdateComments(string commentPermalink)
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");

            string request = "https://www.reddit.com" + commentPermalink + ".json";
            string json = default;

            try
            {
                json = await _client.GetStringAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] Error parsing " + request + ": " + ex.Message);
                return;
            }
            finally
            {
                _client.Dispose();
            }

            List<PostsModel> comments = JsonConvert.DeserializeObject<List<PostsModel>>(json);
            _view.AddComments(comments);
        }
    }
}