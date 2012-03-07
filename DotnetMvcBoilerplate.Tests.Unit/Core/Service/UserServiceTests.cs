using System;
using AutoMoq;
using DotnetMvcBoilerplate.Core.Provider;
using NUnit.Framework;
using DotnetMvcBoilerplate.Tests.Unit.Utils;
using DotnetMvcBoilerplate.Models;
using DotnetMvcBoilerplate.Core.Service;

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
            _autoMoqer.SetInstance<IDatabaseProvider>(_memoryDatabaseProvider);
        }

        [TearDown]
        public void TearDown()
        {
            _memoryDatabaseProvider.GetDb().Users.RemoveAll();
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
    }
}
