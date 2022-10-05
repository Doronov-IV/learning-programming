using EntityHomeworkSecond.Model.Configs;
using EntityHomeworkSecond.Model.Entities;

namespace EntityHomeworkSecond.Model.Context
{
    public class LocalDbContext : DbContext
    {


        public DbSet<Student> Students { get; set; } = null!;


        public DbSet<Student> Cards { get; set; } = null!;



        #region CONTEXT


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\DoronovIV;Database=DoronovEntityCore;Trusted_Connection=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
        }


        #endregion CONTEXT




        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public LocalDbContext()
        {
            Database.EnsureCreated();
        }


    }
}
