using System.Data.Entity;

namespace AlphaTest.Models
{
    public class MyContext :DbContext
    {
        public MyContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Query> Queries { get; set; }
    }
}