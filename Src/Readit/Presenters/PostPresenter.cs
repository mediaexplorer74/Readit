using System;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using Readit.Contracts;
using Readit.Models;

namespace Readit.Presenters
{
    public class PostPresenter : PostContract.IPresenter
    {
        private readonly PostContract.IView _view;

        public PostPresenter(PostContract.IView view)
        {
            _view = view;
            
        }

        public async void UpdatePosts(string subreddit = "")
        {
            HttpClient _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/114.0.0.0 Safari/537.36");

            string request = "https://www.reddit.com" + subreddit + ".json";
            string json = default;

            try
            {
                json = await _client.GetStringAsync(request);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[ex] Error parsing " + request + ": "+ ex.Message);
                return;
            }
            finally
            {
                _client.Dispose();
            }

            SubredditModel frontPage = JsonConvert.DeserializeObject<SubredditModel>(json);
            _view.AddPosts(frontPage);
        }
    }
}