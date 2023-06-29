using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.DbContexts
{
    public class UsersDbContextFactory : IUsersDbContextFactory
    {
        private readonly string _connectionString;

        public UsersDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public UsersDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            return new UsersDbContext(options);
        }
    }
}
