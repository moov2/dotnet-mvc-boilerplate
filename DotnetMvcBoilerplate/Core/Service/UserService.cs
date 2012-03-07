using System;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Provider;

namespace DotnetMvcBoilerplate.Core.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Open connection to the database.
        /// </summary>
        private dynamic _db;

        public UserService(IDatabaseProvider databaseProvider)
        {
            _db = databaseProvider.GetDb();
        }

        /// <summary>
        /// Inserts the user into the database, thus
        /// creating a new user in the system.
        /// </summary>
        /// <param name="user">User that is being created.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public void Create(User user)
        {
            _db.Users.Insert(user);
        }

        /// <summary>
        /// Returns flag indicating whether there is
        /// at least one user in the database.
        /// </summary>
        /// <returns>True if there is at least one user,
        /// otherwise false is returned.</returns>
        public bool AtLeastOneExists()
        {
            return _db.Users.All().Count() > 0;
        }
    }

    public interface IUserService
    {
        void Create(User user);
    }
}