namespace ModPanel.Services
{
    using Contracts;
    using Data;
    using Models;
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
    }
}