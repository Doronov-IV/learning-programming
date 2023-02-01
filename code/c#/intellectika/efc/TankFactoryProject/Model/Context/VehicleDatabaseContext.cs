using TankFactory.Model.Configs;

namespace TankFactory.Model.Context
{
    public class VehicleDatabaseContext : DbContext
    {


        #region State


        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Engine> Engines { get; set; }
        public DbSet<Gun> Guns { get; set; }



        #endregion State





        #region Overrides



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("Config/appsettings.json");

            var config = builder.Build();
            string _connectionString = config.GetConnectionString("TankFactoryConnection");

            optionsBuilder.UseSqlServer(_connectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GunConfiguration());
            modelBuilder.ApplyConfiguration(new PriceConfiguration());
            modelBuilder.ApplyConfiguration(new EngineConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new MainBattleTankConfiguration());
        }



        #endregion Overrides





        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public VehicleDatabaseContext()
        {
            var debugInfo = Database.EnsureDeleted();
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
        public VehicleDatabaseContext(DbContextOptions<VehicleDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }


        #endregion Construction


    }
}
