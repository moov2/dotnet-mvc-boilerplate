using System;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Provider;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Core.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Open connection to the database.
        /// </summary>
        private dynamic _db;

        private IEncryption _encryption;

        public UserService(IDatabaseProvider databaseProvider, IEncryption encryption)
        {
            _db = databaseProvider.GetDb();
            _encryption = encryption;
        }

        /// <summary>
        /// Returns flag indicating whether there is
        /// at least one user in the database.
        /// </summary>
        /// <returns>True if there is at least one user,
        /// otherwise false is returned.</returns>
        public bool AtLeastOneExists()
        {
            return _db.Users.All().ToList().Count > 0;
        }

        /// <summary>
        /// Retrieves a user by their username and then compares the password
        /// to the encrypted password, if successful then the user is returned,
        /// otherwise null.
        /// </summary>
        /// <param name="username">Username credential.</param>
        /// <param name="password">Password credential.</param>
        /// <returns>User that matches the credentials, otherwise null.</returns>
        public User ByUsernameAndPassword(string username, string password)
        {
            var user = (User)_db.Users.FindByUsername(username);

            if (user == null || !_encryption.DecryptCompare(password, user.Password))
                return null;

            return user;
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
    }

    public interface IUserService
    {
        bool AtLeastOneExists();
        User ByUsernameAndPassword(string username, string password);
        void Create(User user);
    }
}