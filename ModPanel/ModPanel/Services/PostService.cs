namespace ModPanel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.BindingModels;
    using Models.ViewModels;
    using Utilities;

    public class PostService : IPostService
    {
        private readonly ModPanelContext context;

        public PostService() => this.context = new ModPanelContext();

        /// <summary>
        /// Create new post and save it to database.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="userId"></param>
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
        {
            using (this.context)
            {
                return this.context
                        .Posts
                        .Select(p => new PostListingViewModel
                        {
                            Id = p.Id,
                            Title = p.Title
                        })
                        .ToList();
            }
        }

        public PostBindingModel GetById(int id)
        {
            using (this.context)
            {
                return this.context
                        .Posts
                        .Where(p => p.Id == id)
                        .Select(p => new PostBindingModel
                        {
                            Title = p.Title,
                            Content = p.Content
                        })
                        .FirstOrDefault();
            }
        }

        public void Update(int id, string title, string content)
        {
            using (this.context)
            {
                var post = this.context
                    .Posts
                    .Find(id);

                if (post == null)
                {
                    return;
                }

                post.Title = title.Capitalize();
                post.Content = content;

                this.context.SaveChanges();
            }
        }

        public string Delete(int id)
        {
            using (this.context)
            {
                var post = this.context.Posts.Find(id);

                if (post == null)
                {
                    return null;
                }

                this.context.Posts.Remove(post);
                this.context.SaveChanges();

                return post.Title;
            }
        }
    }
}