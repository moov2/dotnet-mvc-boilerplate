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
