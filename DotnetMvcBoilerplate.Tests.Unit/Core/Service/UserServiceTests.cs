using System;
using AutoMoq;
using DotnetMvcBoilerplate.Core.Provider;
using NUnit.Framework;
using DotnetMvcBoilerplate.Tests.Unit.Utils;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Service;
using System.Collections.Generic;
using DotnetMvcBoilerplate.Core.Security;
using Moq;
using MongoDB.Bson;

namespace DotnetMvcBoilerplate.Tests.Unit.Core.Service
{
    public class UserServiceTests
    {
        private AutoMoqer _autoMoqer;
        private MemoryDatabaseProvider _memoryDatabaseProvider;

        [SetUp]
        public void Setup()
        {
            _autoMoqer = new AutoMoqer();
            _memoryDatabaseProvider = new MemoryDatabaseProvider();
            _memoryDatabaseProvider.SetKeyColumn("Users", "Id");
            _autoMoqer.SetInstance<IDatabaseProvider>(_memoryDatabaseProvider);
        }

        [TearDown]
        public void TearDown()
        {
            _memoryDatabaseProvider.GetDb().Users.RemoveAll();
        }

        /// <summary>
        /// Tests that AtLeastOneExists returns false when there
        /// are no users in the database.
        /// </summary>
        [Test]
        public void AtLeastOneExists_NoUsersInDatabase_ReturnsFalse()
        {
            Assert.That(_autoMoqer.Resolve<UserService>().AtLeastOneExists(), Is.False);
        }

        /// <summary>
        /// Tests that AtLeastOneExists returns true when there
        /// are users in the database.
        /// </summary>
        [Test]
        public void AtLeastOneExists_UsersInDatabase_ReturnsTrue()
        {
            InsertFakeUsers(Fakes.Users());
            Assert.That(_autoMoqer.Resolve<UserService>().AtLeastOneExists(), Is.True);
        }

        /// <summary>
        /// Tests that ById returns the User from that database that
        /// matches the Id entered into the method.
        /// </summary>
        [Test]
        public void ById_IdThatIsAssociatedToUser_ReturnsUser()
        {
            var users = Fakes.Users();
            InsertFakeUsers(users);

            Assert.That(_autoMoqer.Resolve<UserService>().ById(users[0].Id).Username, Is.EqualTo(users[0].Username));
        }

        /// <summary>
        /// Tests that ById returns null when no Users with the Id
        /// provided are found.
        /// </summary>
        [Test]
        public void ById_IdThatIsntAssociatedToUser_ReturnsNull()
        {
            InsertFakeUsers(Fakes.Users());
            Assert.That(_autoMoqer.Resolve<UserService>().ById(ObjectId.GenerateNewId()), Is.Null);
        }

        /// <summary>
        /// Tests that ById with a string parameter returns the User from 
        /// the database that matches the Id entered into the method.
        /// </summary>
        [Test]
        public void ById_IdAsStringThatIsAssociatedToUser_ReturnsUser()
        {
            var users = Fakes.Users();
            InsertFakeUsers(users);

            Assert.That(_autoMoqer.Resolve<UserService>().ById(users[0].Id.ToString()).Username, Is.EqualTo(users[0].Username));
        }

        /// <summary>
        /// Tests that ByUsernameAndPassword returns null when no users match
        /// the username.
        /// </summary>
        [Test]
        public void ByUsernameAndPassword_NoUserWithUsernameExists_ReturnsNull()
        {
            var usernameThatDoesntExist = "UsernameDoesntExist";
            InsertFakeUsers(Fakes.Users());

            Assert.That(_autoMoqer.Resolve<UserService>().ByUsernameAndPassword(usernameThatDoesntExist, "password"), Is.Null);
        }

        /// <summary>
        /// Tests that ByUsernameAndPassword returns null when the username is
        /// associated to a user, but the password doesn't match.
        /// </summary>
        [Test]
        public void ByUsernameAndPassword_UserWithUsernameButIncorrectPassword_ReturnsNull()
        {
            const string incorrectPassword = "password";
            var users = Fakes.Users();
            InsertFakeUsers(users);

            _autoMoqer.GetMock<IEncryption>().Setup(x => x.DecryptCompare(incorrectPassword, It.IsAny<Password>())).Returns(false);

            Assert.That(_autoMoqer.Resolve<UserService>().ByUsernameAndPassword(users[0].Username, incorrectPassword), Is.Null);
        }

        /// <summary>
        /// Tests that ByUsernameAndPassword returns the User when the username is associated
        /// to a user, and the password is correct.
        /// </summary>
        [Test]
        public void ByUsernameAndPassword_UserWithUsernameAndPasswordCorrect_ReturnsUsername()
        {
            const string correctPassword = "password";
            var users = Fakes.Users();
            InsertFakeUsers(users);

            _autoMoqer.GetMock<IEncryption>().Setup(x => x.DecryptCompare(correctPassword, It.IsAny<Password>())).Returns(true);

            Assert.That(_autoMoqer.Resolve<UserService>().ByUsernameAndPassword(users[0].Username, correctPassword).Id, Is.EqualTo(users[0].Id));
        }

        /// <summary>
        /// Tests that Create inserts the user provided into
        /// the database.
        /// </summary>
        [Test]
        public void Create_InsertsUserIntoDatabase()
        {
            const string expectedUsername = "New User";
            var userBeingCreated = new User { Username = expectedUsername };
            _autoMoqer.Resolve<UserService>().Create(userBeingCreated);

            Assert.That((User)_memoryDatabaseProvider.GetDb().Users.FindByUsername(expectedUsername), !Is.Null);
        }

        /// <summary>
        /// Inserts a collection of fake users.
        /// </summary>
        /// <param name="users">List of users.</param>
        private void InsertFakeUsers(IList<User> users)
        {
            var db = _memoryDatabaseProvider.GetDb();

            foreach (var user in users)
                db.Users.Insert(user);
        }
    }
}
