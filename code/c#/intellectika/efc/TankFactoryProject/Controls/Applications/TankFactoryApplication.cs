using MainEntityProject.Controls.Common;
using MainEntityProject.Generation;
using MainEntityProject.Generation.TankFactories;
using MainEntityProject.Model.Context;

namespace MainEntityProject.Controls.Applications
{
    public class TankFactoryApplication : IApplication
    {
        private IServiceCollection serviceCollection;

        public async Task Start()
        {
            ITankFactory factory = new GermanTankFactory();
            await AddTank(factory.CreateNativeTank());
        }


        private async Task AddTank(MainBattleTank vehicle)
        {
            using var context = new VehicleDatabaseContext();

            var duplicate = await context.Tanks.FirstOrDefaultAsync(t => t.ModelName.Equals(vehicle.ModelName));

            if (duplicate is null)
            {
                await context.Prices.AddAsync(vehicle.GunReference.PriceReference);
                await context.Prices.AddAsync(vehicle.EngineReference.PriceReference);
                await context.Prices.AddAsync(vehicle.PriceReference);

                await context.Budgets.AddAsync(vehicle.GunReference.ManufacturerReference.BudgetReference);
                await context.Budgets.AddAsync(vehicle.EngineReference.ManufacturerReference.BudgetReference);
                await context.Budgets.AddAsync(vehicle.ManufacturerReference.BudgetReference);

                await context.Manufacturers.AddAsync(vehicle.GunReference.ManufacturerReference);
                await context.Manufacturers.AddAsync(vehicle.EngineReference.ManufacturerReference);
                await context.Manufacturers.AddAsync(vehicle.ManufacturerReference);

                await context.Guns.AddAsync(vehicle.GunReference);

                await context.Engines.AddAsync(vehicle.EngineReference);

                await context.Tanks.AddAsync(vehicle);
            }

            context.SaveChanges();
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public TankFactoryApplication()
        {
            serviceCollection = new ServiceCollection();
            
            
        }

    }
}
