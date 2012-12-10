using System;
using DotnetMvcBoilerplate.Models;
using System.Collections.Generic;

namespace DotnetMvcBoilerplate.Tests.Unit.Utils
{
    public class Fakes
    {
        /// <summary>
        /// Returns a collection of users.
        /// </summary>
        /// <returns>List of users.</returns>
        public static IList<User> Users()
        {
            var users = new List<User>();
            users.Add(new User { Username = "Dan Thomas" });
            users.Add(new User { Username = "Peter Keating" });
            users.Add(new User { Username = "Lawrence Dine" });
            return users;
        }
    }
}
