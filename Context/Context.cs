using BufunfaTech.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BufunfaTech.API.Context
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options) => Database.EnsureCreated();

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }
}
