using EntityHomeworkFirst.Model;

namespace EntityHomeworkFirst.ViewModel
{
    public class ApplicationContext : DbContext
    {


        public DbSet<Order> Orders { get; set; }



        #region CONTEXT


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DoronovIV;Database=DoronovEntityNetCore;Trusted_Connection=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
        }


        #endregion CONTEXT




        #region CONSTRUCTION


        public ApplicationContext()
        {
            Database.EnsureCreated();
        }


        #endregion CONSTRUCTION

    }
}
