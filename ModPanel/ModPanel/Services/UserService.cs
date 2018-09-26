namespace ModPanel.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;
    using SimpleMvc.Common;

    public class UserService : IUserService
    {
        private readonly ModPanelContext context;

        public UserService() => this.context = new ModPanelContext();

        public bool Create(string email, string password, PositionType position)
        {
            if (this.context.Users.Any(u => u.Email == email))
            {
                return false;
            }

            var isAdmin = !this.context.Users.Any();
            var passwordHash = PasswordUtilities.GetPasswordHash(password);

            var user = new User
            {
                Email = email,
                PasswordHash = passwordHash,
                IsAdmin = isAdmin,
                Position = position,
                IsApproved = isAdmin
            };

            this.context.Add(user);
            this.context.SaveChanges();

            return true;
        }
    }
}