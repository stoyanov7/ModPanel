namespace ModPanel.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Models;
    using Models.Enums;
    using Models.ViewModels;
    using Utilities;

    public class UserService : IUserService
    {
        private readonly ModPanelContext context;

        public UserService() => this.context = new ModPanelContext();

        /// <summary>
        /// Register new user in database. The first registered user become an admin.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="position"></param>
        /// <returns>
        /// True if the user is registered successfully.
        /// False if the email is in the database.
        /// </returns>
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

        /// <summary>
        /// Check if user is approved.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>True if the user is approved, otherwise false.</returns>
        public bool UserIsApproved(string email)
            => this.context
                .Users
                .Any(u => u.Email == email && u.IsApproved);

        /// <summary>
        /// Check if email and password hash exist in the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserExists(string email, string password)
        {
            var passwordHash = PasswordUtilities.GetPasswordHash(password);

            return this.context
                .Users
                .Any(u => u.Email == email && u.PasswordHash == passwordHash);
        }

        public IEnumerable<AdminUsersViewModel> All()
            => this.context
                .Users
                .Select(u => new AdminUsersViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    IsApproved = u.IsApproved,
                    Position = u.Position,
                    Posts = u.Posts.Count()
                })
                .ToList();

        public string Approve(int id)
        {
            var user = this.context
                .Users
                .Find(id);

            if (user != null)
            {
                user.IsApproved = true;
                this.context.SaveChanges();
            }

            return user?.Email;
        }
    }
}