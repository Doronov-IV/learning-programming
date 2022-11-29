using ReversedService.Model.Entities;
using ReversedService.Model.Configs;
using ReversedService.service_view;
using ReversedService.ViewModel.ServiceWindow;
using Microsoft.Extensions.Configuration;

namespace ReversedService.Model.Context
{
    public class MessengerDatabaseContext : DbContext
    {

        #region STATE



        public DbSet<User> Users { get; set; } = null!; 


        public DbSet<Message> messages { get; set; } = null!;


        public DbSet<Chat> Chats { get; set; } = null!;



        #endregion STATE




        #region OVERRIDES



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
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
        }



        #endregion OVERRIDES




        #region CONSTRUCTION


        //


        #endregion CONSTRUCTION
    }
}
