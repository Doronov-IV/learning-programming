using MainEntityProject.Model.Entities;
using MainEntityProject.Model.Configs;
using Microsoft.Extensions.Configuration;
using System.IO;

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



        /// <summary>
        /// A set of books corrensponding to the 'Books' table in the db.
        /// <br />
        /// Сет книг, соответствующий таблице "Книги" в бд.
        /// </summary>
        public DbSet<Book> Books { get; set; } = null!;


        /// <summary>
        /// A set of books corrensponding to the 'Authors' table in the db.
        /// <br />
        /// Сет книг, соответствующий таблице "Авторы" в бд.
        /// </summary>
        public DbSet<Author> Authors { get; set; } = null!;



        #endregion STATE





        #region CONTEXT OVERRIDES


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile(".config/appsettings.json");

            var config = builder.Build();
            string _connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(_connectionString);
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


        /// <summary>
        /// Constructor with connection options configurations (in this case).
        /// <br />
        /// Конструктор с конфигурацией опций подключения (в данном случае).
        /// </summary>
        /// <param name="options">
        /// Connection options instance.
        /// <br />
        /// Экземпляр опций подключения.
        /// </param>
        public CurrentDatabaseContext(DbContextOptions<CurrentDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }



        #endregion CONSTRUCTION



    }
}
