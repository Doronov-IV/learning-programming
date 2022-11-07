using MainEntityProject.Model.Entities;
using MainEntityProject.Model.Configs;
using MainEntityProject.ViewModel;

namespace MainEntityProject.Model.Context
{
    /// <summary>
    /// Custom Entity database context;
    /// <br />
    /// Кастомный контекст датабазы из Entity;
    /// </summary>
    public class CurrentDatabaseContext : DbContext
    {



        #region STATE



        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;



        #endregion STATE





        #region CONTEXT OVERRIDES


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer($@"Server=.\doronoviv;Database = MainEFCproject;Trusted_Connection=true;Encrypt=false");
        }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        }


        #endregion CONTEXT OVERRIDES





        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public CurrentDatabaseContext()
        {
            Database.EnsureCreated();
        }



        #endregion CONSTRUCTION



    }
}
