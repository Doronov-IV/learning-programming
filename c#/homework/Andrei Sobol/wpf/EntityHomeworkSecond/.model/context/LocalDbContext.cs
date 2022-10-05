using EntityHomeworkSecond.Model.Configs;
using EntityHomeworkSecond.Model.Entities;

namespace EntityHomeworkSecond.Model.Context
{
    public class LocalDbContext : DbContext
    {


        public DbSet<Student> Students { get; set; } = null!;


        public DbSet<Card> Cards { get; set; } = null!;



        #region CONTEXT


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\DoronovIV;Database=DoronovEntityCore;Trusted_Connection=True;");
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new StudentConfiguration());
            //modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Card>().HasKey(c => c.Id);
            modelBuilder.Entity<Card>().HasOne(c => c.Student).WithOne(s => s.Card).HasForeignKey<Card>(c => c.SerialNumber);
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
