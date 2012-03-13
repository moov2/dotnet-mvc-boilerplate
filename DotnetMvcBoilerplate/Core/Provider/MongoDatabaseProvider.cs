using System;
using Simple.Data;
using Simple.Data.MongoDB;
using System.Configuration;

namespace DotnetMvcBoilerplate.Core.Provider
{
    public class MongoDatabaseProvider : IDatabaseProvider
    {
        /// <summary>
        /// Gets a dynamic object that has an open connection
        /// to a MongoDatabase.
        /// </summary>
        /// <returns>Open connection to Mongo.</returns>
        public dynamic GetDb()
        {
            return Database.Opener.OpenMongo(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
        }
    }
}