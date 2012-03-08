using System;
using MongoDB.Bson;
using System.Collections.Generic;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Models
{
    public class User
    {
        public ObjectId Id { get; set; }
        public Password Password { get; set; }
        public virtual string Username { get; set; }
        public IList<string> Roles { get; set; }

        /// <summary>
        /// Returns flag that notifying if said user is an admin.
        /// </summary>
        public bool IsAdmin
        {
            get { return Roles.Contains(Role.Admin); }
        }

        public User()
        {
            Id = ObjectId.GenerateNewId();
            Roles = new List<string>();
        }

        /// <summary>
        /// Adds a role to the Roles collection, ensuring that their
        /// aren't any duplicates.
        /// </summary>
        /// <param name="role">Role to be added to the collection.</param>
        private void AddRole(string role)
        {
            if (!Roles.Contains(role))
                Roles.Add(role);
        }

        /// <summary>
        /// Adds the Administrator role to the Roles collection.
        /// </summary>
        public void MakeAdmin()
        {
            AddRole(Role.Admin);
        }
    }

    public class Role
    {
        public const string Admin = "Administrator";
    }
}