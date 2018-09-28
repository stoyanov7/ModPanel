namespace ModPanel.Controllers
{
    using System.Linq;
    using Services.Contracts;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        private readonly IPostService postService;
        private readonly ILogService logService;

        public HomeController(IPostService postService, ILogService logService)
        {
            this.postService = postService;
            this.logService = logService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            this.Model.Data["guestDisplay"] = "block";
            this.Model.Data["userDisplay"] = "none";
            this.Model.Data["admin"] = "none";

            if (this.User.IsAuthenticated)
            {
                this.Model.Data["guestDisplay"] = "none";
                this.Model.Data["userDisplay"] = "flex";

                string search = null;
                if (this.Request.UrlParameters.ContainsKey("search"))
                {
                    search = this.Request.UrlParameters["search"];
                }

                var postsData = this.postService.AllWithDetails(search);

                var postsCards = postsData
                    .Select(p => p.HomePostsToHtml());

                this.Model.Data["posts"] = postsCards.Any()
                    ? string.Join(string.Empty, postsCards)
                    : "<h2>No posts found!</h2>";

                if (this.IsAdmin)
                {
                    this.Model.Data["authenticated"] = "none";
                    this.Model.Data["admin"] = "flex";

                    var logsHtml = this.logService
                        .AllLogs()
                        .Select(l => l.LogsToHtml());

                    this.Model.Data["logs"] = string.Join(string.Empty, logsHtml);
                }
            }

            return this.View();
        }
    }
}