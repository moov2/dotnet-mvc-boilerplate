using System;
using DotnetMvcBoilerplate.Core.Provider;
using Simple.Data;

namespace DotnetMvcBoilerplate.Tests.Unit.Utils
{
    public class MemoryDatabaseProvider : IDatabaseProvider
    {
        private InMemoryAdapter _memoryAdapter;

        public MemoryDatabaseProvider()
        {
            _memoryAdapter = new InMemoryAdapter();
        }
        
        /// <summary>
        /// Sets the key column on the memory adapter.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="columnName">Column in the table.</param>
        public void SetKeyColumn(string tableName, string columnName)
        {
            _memoryAdapter.SetKeyColumn(tableName, columnName);
        }

        /// <summary>
        /// Gets an open connection to an in memory database.
        /// </summary>
        /// <returns>Open connection to in memory database.</returns>
        public dynamic GetDb()
        {
            Database.UseMockAdapter(_memoryAdapter);
            return Database.Open();
        }
    }
}
