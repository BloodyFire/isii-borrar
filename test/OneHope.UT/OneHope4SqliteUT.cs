﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneHope.API.Models;

namespace OneHope.UT
{
    public class OneHope4SqliteUT
    {
        protected readonly DbConnection _connection;
        protected readonly ApplicationDBContext _context;
        protected readonly DbContextOptions<ApplicationDBContext> _contextOptions;

        protected ApplicationDBContext CreateContext() => new(_contextOptions);
        ////This code is the same one as the above line. 
        //ApplicationDBContext CreateContext() { 
        //    new ApplicationDBContext(_contextOptions); 
        //}

        void Dispose() => _connection.Dispose();
        public OneHope4SqliteUT()
        {
            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseSqlite(_connection).Options;

            // Create the schema and seed some data
            _context = new ApplicationDBContext(_contextOptions);
            if (_context.Database.EnsureCreated())
            {
                using var viewCommand = _context.Database.GetDbConnection().CreateCommand();
                viewCommand.CommandText = @"
                CREATE VIEW AllResources AS
                SELECT Name
                FROM Movies;";
                viewCommand.ExecuteNonQuery();
            }
        }
    }
}
