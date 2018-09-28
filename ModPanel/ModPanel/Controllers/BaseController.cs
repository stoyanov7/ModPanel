namespace ModPanel.Controllers
{
    using System;
    using System.Linq;
    using Data;
    using Models;
    using Models.Enums;
    using Services;
    using Services.Contracts;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Interfaces;

    public class BaseController : Controller
    {
        private readonly ILogService logService;

        protected BaseController()
        {
            this.logService = new LogService();

            this.Model.Data["anonymousDisplay"] = "flex";
            this.Model.Data["userDisplay"] = "none";
            this.Model.Data["adminDisplay"] = "none";
            this.Model.Data["show-error"] = "none";
            this.Model.Data["year"] = DateTime.Now.Year.ToString();
        }

        protected User DbUser { get; private set; }

        protected bool IsAdmin => this.User.IsAuthenticated && this.DbUser.IsAdmin;

        protected IActionResult RedirectToHome() => this.RedirectToAction("/home/index");

        protected IActionResult RedirectToLogin() => this.RedirectToAction("/user/login");

        protected void ShowError(string error)
        {
            this.Model.Data["show-error"] = "block";
            this.Model.Data["error"] = error;
        }

        protected void Log(LogType type, string additionalInformation)
            => this.logService
                .Create(this.DbUser.Email, type, additionalInformation);

        public override void OnAuthentication()
        {
            if (this.User.IsAuthenticated)
            {
                this.Model.Data["anonymousDisplay"] = "none";
                this.Model.Data["userDisplay"] = "flex";

                using (var context = new ModPanelContext())
                {
                    this.DbUser = context
                        .Users
                        .First(u => u.Email == this.User.Name);

                    if (this.DbUser.IsAdmin)
                    {
                        this.Model.Data["adminDisplay"] = "flex";
                    }
                }
            }
        }
    }
}