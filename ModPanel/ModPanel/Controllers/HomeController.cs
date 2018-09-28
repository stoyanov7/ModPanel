namespace ModPanel.Controllers
{
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            this.Model.Data["guestDisplay"] = "block";

            return this.View();
        }
    }
}