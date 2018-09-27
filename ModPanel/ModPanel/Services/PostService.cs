namespace ModPanel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.ViewModels;
    using Utilities;

    public class PostService : IPostService
    {
        private readonly ModPanelContext context;

        public PostService() => this.context = new ModPanelContext();

        public void Create(string title, string content, int userId)
        {
            using (this.context)
            {
                var post = new Post
                {
                    Title = title.Capitalize(),
                    Content = content,
                    UserId = userId
                };

                this.context.Posts.Add(post);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<PostListingViewModel> AllPost()
            => this.context
                .Posts
                .Select(p => new PostListingViewModel
                {
                    Id = p.Id,
                    Title = p.Title
                })
                .ToList();
    }
}