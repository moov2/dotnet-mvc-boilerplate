using System;
using System.Linq;
using NUnit.Framework;
using DotnetMvcBoilerplate.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using DotnetMvcBoilerplate.Core.Security;

namespace DotnetMvcBoilerplate.Tests.Unit.Models
{
    public class UserTests
    {
        /// <summary>
        /// Tests that by default the Id isn't empty or null.
        /// </summary>
        [Test]
        public void Default_IdIsNotNullOrEmpty()
        {
            Assert.That(new User().Id != ObjectId.Empty && new User().Id != null, Is.True);
        }

        /// <summary>
        /// Tests that by default the Roles list is empty.
        /// </summary>
        [Test]
        public void Default_RolesIsEmpty()
        {
            Assert.That(new User().Roles, Is.Empty);
        }

        /// <summary>
        /// Tests that IsAdmin returns true when the 
        /// Administrator role is in the Roles collection.
        /// </summary>
        [Test]
        public void IsAdmin_AdminRoleInRoles_IsTrue()
        {
            Assert.That(new User { Roles = new List<string> { Role.Admin } }.IsAdmin, Is.True);
        }

        /// <summary>
        /// Tests that IsAdmin returns false when the 
        /// Administrator role is not within the Roles collection.
        /// </summary>
        [Test]
        public void IsAdmin_AdminRoleIsNotInRoles_IsFalse()
        {
            Assert.That(new User().IsAdmin, Is.False);
        }

        /// <summary>
        /// Tests that MakeAdmin adds the admin role to the collection
        /// of roles.
        /// </summary>
        [Test]
        public void MakeAdmin_NotAnAdmin_AddsAdminRoleToRoles()
        {
            var user = new User();
            user.MakeAdmin();
            Assert.That(user.Roles.Contains(Role.Admin), Is.True);
        }

        /// <summary>
        /// Tests that MakeAdmin when the User is already an admin doesn't duplicate
        /// the Administrator role in the Roles collection.
        /// </summary>
        [Test]
        public void MakeAdmin_AlreadyAdmin_DoesntDuplicateAdminRoleInRoles()
        {
            var user = new User { Roles = new List<string> { Role.Admin } };
            user.MakeAdmin();
            Assert.That(user.Roles.Where(x => x == Role.Admin).Count(), Is.EqualTo(1));
        }

        /// <summary>
        /// Tests that setting the Password sets the PasswordKey
        /// to the expected value.
        /// </summary>
        [Test]
        public void SetPassword_ShouldSetPasswordKey()
        {
            var password = new Encryption().Encrypt("password");
            var user = new User();
            user.SetPassword(password);

            Assert.That(user.PasswordKey, Is.EqualTo(password.Key));
        }

        /// <summary>
        /// Tests that setting the Password sets the PasswordSalt
        /// to the expected value.
        /// </summary>
        [Test]
        public void SetPassword_ShouldSetPasswordSalt()
        {
            var password = new Encryption().Encrypt("password");
            var user = new User();
            user.SetPassword(password);

            Assert.That(user.PasswordSalt, Is.EqualTo(password.Salt));
        }
    }
}
