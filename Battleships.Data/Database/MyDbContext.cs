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
            System.Data.Entity.Database.SetInitializer(new ContextInitializer());
        }
    }
}
