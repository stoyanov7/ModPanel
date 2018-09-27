namespace ModPanel.Controllers
{
    using Attributes;
    using Models.BindingModels;
    using Models.Enums;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class PostController : BaseController
    {
        private const string CreateError = "<p>Check your form for errors.</p><p>Title must begin with uppercase letter and has length between 3 and 100 symbols (inclusive).</p><p>Content must be more than 10 symbols (inclusive).</p>";

        private readonly IPostService postService;

        public PostController(IPostService postService) => this.postService = postService;

        [HttpGet]
        [AuthorizeLogin]
        public IActionResult Create() => this.View();

        [HttpPost]
        [AuthorizeLogin]
        public IActionResult Create(PostBindingModel model)
        {
            if (!this.IsValidModel(model))
            {
                this.ShowError(CreateError);
                return this.View();
            }

            this.postService.Create(model.Title, model.Content, this.DbUser.Id);

            if (this.IsAdmin)
            {
                this.Log(LogType.CreatePost, model.Title);
            }

            return this.RedirectToHome();
        }
    }
}