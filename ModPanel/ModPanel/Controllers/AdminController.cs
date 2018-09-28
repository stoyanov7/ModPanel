namespace ModPanel.Controllers
{
    using System.Linq;
    using Attributes;
    using Models.BindingModels;
    using Models.Enums;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class AdminController : BaseController
    {
        private const string EditError = "<p>Check your form for errors.</p><p>Title must begin with uppercase letter and has length between 3 and 100 symbols (inclusive).</p><p>Content must be more than 10 symbols (inclusive).</p>";

        private readonly IUserService userService;
        private readonly IPostService postService;
        private readonly ILogService logService;

        public AdminController(IUserService userService, IPostService postService, ILogService logService)
        {
            this.userService = userService;
            this.postService = postService;
            this.logService = logService;
        }

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Users()
        {
            var rows = this.userService
                .All()
                .Select(u => u.UsersToHtml());

            this.Model.Data["users"] = string.Join(string.Empty, rows);
            this.Log(LogType.OpenMenu, nameof(this.Users));

            return this.View();
        }

        [AuthorizeLogin]
        public IActionResult Approve(int id)
        {
            var userEmail = this.userService.Approve(id);

            if (userEmail != null)
            {
                this.Log(LogType.UserApproval, userEmail);
            }

            return this.RedirectToAction("/admin/users");
        }

        [AuthorizeLogin]
        public IActionResult Posts()
        {
            var rows = this.postService
                .AllPost()
                .Select(p => p.PostsToHtml());

            this.Model.Data["posts"] = string.Join(string.Empty, rows);
            this.Log(LogType.OpenMenu, nameof(this.Posts));

            return this.View();
        }

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Edit(int id)
        {
            var post = this.postService.GetById(id);

            if (post == null)
            {
                return this.RedirectToHome();
            }

            this.Model.Data["title"] = post.Title;
            this.Model.Data["content"] = post.Content;

            return this.View();
        }

        [HttpPost]
        [AuthorizeLogin]
        public IActionResult Edit(int id, PostBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError(EditError);
                return this.View();
            }

            this.postService.Update(id, model.Title, model.Content);
            this.Log(LogType.EditPost, model.Title);

            return this.RedirectToAction("/admin/posts");
        }

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Delete(int id)
        {
            var post = this.postService.GetById(id);

            if (post == null)
            {
                return this.RedirectToHome();
            }

            this.Model.Data["title"] = post.Title;
            this.Model.Data["content"] = post.Content;

            return this.View();
        }

        [HttpPost]
        [AuthorizeLogin]
        public IActionResult Delete(int id, PostBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError(EditError);
                return this.View();
            }

            var post = this.postService.Delete(id);

            if (post != null)
            {
                this.Log(LogType.DeletePost, post);
            }

            return this.RedirectToAction("/admin/posts");
        }

        [AuthorizeLogin]
        public IActionResult Log()
        {
            this.Log(LogType.OpenMenu, nameof(Log));

            var rows = this.logService
                .AllLogs()
                .Select(l => l.LogsToHtml());

            this.Model.Data["logs"] = string.Join(string.Empty, rows);

            return this.View();
        }
    }
}