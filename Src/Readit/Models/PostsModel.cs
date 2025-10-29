using System.Collections.Generic;
using Newtonsoft.Json;

namespace Readit.Models
{
    public class PostsModel
    {
        [JsonProperty(PropertyName = "data")]
        public PostsListingModel Data { get; set; }
    }

    public class PostsListingModel
    {
        [JsonProperty(PropertyName = "children")]
        public List<PostsChildrenModel> Children { get; set; }
    }

    public class PostsChildrenModel
    {
        [JsonProperty(PropertyName = "kind")]
        public string Kind { get; set; }

        [JsonProperty(PropertyName = "data")]
        public PostsCommentModel Data { get; set; }
    }

    public class PostsCommentModel
    {
        [JsonProperty(PropertyName = "body")]
        public string Body { get; set; }

        [JsonProperty(PropertyName = "author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "created_utc")]
        public string Created { get; set; }

        [JsonProperty(PropertyName = "replies")]
        public PostsModel Replies { get; set; }

        // The following fields are present for the root post object (kind == "t3").
        // They will be null for regular comments (kind == "t1").
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "selftext")]
        public string Selftext { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}