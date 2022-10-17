using EntityHomeworkThird.Model.Entities;
using EntityHomeworkThird.Model.Configs;

namespace EntityHomeworkThird.Model.Context
{
    public class CurrentDatabaseContext : DbContext
    {


        #region PROPERTIES


        public DbSet<Student> Students { get; set; } = null!;


        public DbSet<Card> Cards { get; set; } = null!;


        #endregion PROPERTIES




        #region CONTEXT OVERRIDES


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\DoronovIV;Database=DoronovEntityCoreThird;Trusted_Connection=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
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
