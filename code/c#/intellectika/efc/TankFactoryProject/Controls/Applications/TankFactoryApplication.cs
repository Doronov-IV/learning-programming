using MainEntityProject.Model.Context;
using MainEntityProject.Controls.Common;
using MainEntityProject.Generation;
using MainEntityProject.Generation.TankFactories;
using MainEntityProject.LocalService;
using MainEntityProject.Data.JSON;

namespace MainEntityProject.Controls.Applications
{
    public class TankFactoryApplication : IApplication
    {

        private IServiceCollection serviceCollection;


        public async Task Start()
        {

            List<ITankFactory> list = new()
            {
                new FrenchTankFactory(),
                new GermanTankFactory(),
                new SwedishTankFactory(),
                new JapaneseTankFactory()
            };

            DatabaseDialer visitor = new(this);

            var context = GetProvider().GetRequiredService<VehicleDatabaseContext>();

            Parallel.ForEach(context.Tanks, (tank) =>
            {
                TankJsonSerializer.Serialize(tank);
            });
        }


        public IServiceProvider GetProvider()
        {
            return serviceCollection.BuildServiceProvider();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TankFactoryApplication()
        {
            serviceCollection = new ServiceCollection();
            
            serviceCollection.AddDbContext<VehicleDatabaseContext>();
        }

    }
}
