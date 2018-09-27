namespace ModPanel
{
    using System;
    using Data;
    using Services;
    using Services.Contracts;
    using SimpleInjector;
    using SimpleInjector.Lifestyles;
    using SimpleMvc.Framework.Controllers;
    using SimpleMvc.Framework.Routers;

    public class DependencyControllerRouter : ControllerRouter
    {
        private readonly Container container;

        public DependencyControllerRouter()
        {
            this.container = new Container();
            this.container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
        }

        public Container Container => this.container;

        public static DependencyControllerRouter Get()
        {
            var router = new DependencyControllerRouter();

            var container = router.Container;

            container.Register<IUserService, UserService>();
            container.Register<IPostService, PostService>();
            container.Register<ModPanelContext>(Lifestyle.Scoped);

            container.Verify();

            return router;
        }

        protected override Controller CreateController(Type controllerType)
        {
            AsyncScopedLifestyle.BeginScope(this.Container);
            return (Controller)this.Container.GetInstance(controllerType);
        }
    }
}