using EntityHomeworkFirst.Model;

namespace EntityHomeworkFirst.ViewModel
{
    public class ApplicationContext : DbContext
    {


        public DbSet<Order> Orders { get; set; }



        #region CONTEXT


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\DoronovIV;Database=DoronovEntityCore;Trusted_Connection=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
        }


        #endregion CONTEXT




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }



        #endregion CONSTRUCTION

    }
}
