using Battleships.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Data.Database
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("BattleshipsDB")
        {
            System.Data.Entity.Database.SetInitializer<MyDbContext>(new CreateDatabaseIfNotExists<MyDbContext>());
        }

        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbScore> Scores { get; set; }
        public DbSet<DbGame> Games { get; set; }
    }
}
