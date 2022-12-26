using EntityHomeworkFirst.Model;
using EntityHomeworkFirst.ViewModel;

namespace EntityHomeworkFirst.ViewModel
{
    public class ApplicationContext : DbContext
    {


        /// <summary>
        /// An Orders table contents;
        /// <br />
        /// Содержимое таблицы "Orders";
        /// </summary>
        public DbSet<Order> Orders { get; set; } = null!;



        #region CONTEXT OVERRIDES


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MainWindowViewModel.ConnectionString);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.Id);
        }


        #endregion CONTEXT OVERRIDES




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }



        #endregion CONSTRUCTION

    }
}
