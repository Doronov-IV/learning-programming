using MainEntityProject.Model.Context;

namespace MainEntityProject.LocalService
{
    public class TankReceptionChain
    {

        private MainBattleTank vehicle;
        private MainBattleTank duplicate;
        private VehicleDatabaseContext context;


        public async Task Start()
        {
            await StartAssemblyChain(vehicle, duplicate);
        }



        #region Logic


        private async Task StartAssemblyChain(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            await AddPrices(vehicle, duplicate);
        }



        private async Task AddPrices(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            if (vehicle.GunReference.PriceReference is not null)
            {
                Price gunPrice = context.Prices.Where(p => p.Value.Equals(vehicle.GunReference.PriceReference.Value) &&
                p.Currency.Equals(vehicle.GunReference.PriceReference.Currency)).ToList().FirstOrDefault();

                if (gunPrice is null)
                    await context.Prices.AddAsync(vehicle.GunReference.PriceReference);
                else
                {
                    duplicate.GunReference.PriceReference = gunPrice;
                    duplicate.GunReference.PriceId = gunPrice.Id;
                }
            }


            if (vehicle.EngineReference.PriceReference is not null)
            {
                Price enginePrice = context.Prices.Where(p => p.Value.Equals(vehicle.EngineReference.PriceReference.Value) &&
                p.Currency.Equals(vehicle.EngineReference.PriceReference.Currency)).ToList().FirstOrDefault();

                if (enginePrice is null)
                    await context.Prices.AddAsync(vehicle.EngineReference.PriceReference);
                else
                {
                    duplicate.EngineReference.PriceReference = enginePrice;
                    duplicate.EngineReference.PriceId = enginePrice.Id;
                }
            }


            if (vehicle.PriceReference is not null)
            {
                Price tankPrice = context.Prices.Where(p => p.Value.Equals(vehicle.PriceReference.Value) &&
                p.Currency.Equals(vehicle.PriceReference.Currency)).ToList().FirstOrDefault();

                if (tankPrice is null)
                    await context.Prices.AddAsync(vehicle.PriceReference);
                else
                {
                    duplicate.PriceReference = tankPrice;
                    duplicate.PriceId = tankPrice.Id;
                }
            }


            await AddBudgets(vehicle, duplicate);
        }



        private async Task AddBudgets(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            if (vehicle.GunReference.ManufacturerReference.BudgetReference is not null)
            {
                Budget gunManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.GunReference.ManufacturerReference.BudgetReference.Value) &&
                b.Currency.Equals(vehicle.GunReference.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                if (gunManufacturerBudget is null)
                    await context.Budgets.AddAsync(vehicle.GunReference.ManufacturerReference.BudgetReference);
                else
                {
                    duplicate.GunReference.ManufacturerReference.BudgetReference = gunManufacturerBudget;
                    duplicate.GunReference.ManufacturerReference.BudgetId = gunManufacturerBudget.Id;
                }
            }


            if (vehicle.EngineReference.ManufacturerReference.BudgetReference is not null)
            {
                Budget engineManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.EngineReference.ManufacturerReference.BudgetReference.Value) &&
                b.Currency.Equals(vehicle.EngineReference.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                if (engineManufacturerBudget is null)
                    await context.Budgets.AddAsync(vehicle.EngineReference.ManufacturerReference.BudgetReference);
                else
                {
                    duplicate.EngineReference.ManufacturerReference.BudgetReference = engineManufacturerBudget;
                    duplicate.EngineReference.ManufacturerReference.BudgetId = engineManufacturerBudget.Id;
                }
            }


            if (vehicle.ManufacturerReference.BudgetReference is not null)
            {
                Budget tankManufacturerBudget = context.Budgets.Where(b => b.Value.Equals(vehicle.ManufacturerReference.BudgetReference.Value) &&
                b.Currency.Equals(vehicle.ManufacturerReference.BudgetReference.Currency)).ToList().FirstOrDefault();

                if (tankManufacturerBudget is null)
                    await context.Budgets.AddAsync(vehicle.ManufacturerReference.BudgetReference);
                else
                {
                    duplicate.ManufacturerReference.BudgetReference = tankManufacturerBudget;
                    duplicate.ManufacturerReference.BudgetId = tankManufacturerBudget.Id;
                }
            }


            await AddManufacturers(vehicle, duplicate);
        }



        private async Task AddManufacturers(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            if (vehicle.GunReference.ManufacturerReference is not null)
            {
                Manufacturer gunManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.GunReference.ManufacturerReference.Name)).ToList().FirstOrDefault();
                if (gunManufacturer is null)
                    await context.Manufacturers.AddAsync(vehicle.GunReference.ManufacturerReference);
                else
                {
                    duplicate.GunReference.ManufacturerReference = gunManufacturer;
                    duplicate.GunReference.ManufacturerId = gunManufacturer.Id;
                }
            }


            if (vehicle.EngineReference.ManufacturerReference is not null)
            {
                Manufacturer engineManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.EngineReference.ManufacturerReference.Name)).ToList().FirstOrDefault();
                if (engineManufacturer is null)
                    await context.Manufacturers.AddAsync(vehicle.EngineReference.ManufacturerReference);
                else
                {
                    duplicate.EngineReference.ManufacturerReference = engineManufacturer;
                    duplicate.EngineReference.ManufacturerId = engineManufacturer.Id;
                }
            }


            if (vehicle.ManufacturerReference is not null)
            {
                Manufacturer tankManufacturer = context.Manufacturers.Where(b => b.Name.Equals(vehicle.ManufacturerReference.Name)).ToList().FirstOrDefault();
                if (tankManufacturer is null)
                    await context.Manufacturers.AddAsync(vehicle.ManufacturerReference);
                else
                {
                    duplicate.ManufacturerReference = tankManufacturer;
                    duplicate.ManufacturerId = tankManufacturer.Id;
                }
            }


            await AddGun(vehicle, duplicate);
        }



        private async Task AddGun(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            if (vehicle.GunReference is not null)
            {
                Gun gun = context.Guns.Where(g => g.ModelName.Equals(vehicle.GunReference.ModelName)).ToList().FirstOrDefault();
                if (gun is null)
                    await context.Guns.AddAsync(vehicle.GunReference);
                else
                {
                    duplicate.GunReference = gun;
                    duplicate.GunId = gun.Id;
                }
            }

            await AddEngine(vehicle, duplicate);
        }



        private async Task AddEngine(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            if (vehicle.EngineReference is not null)
            {
                Engine engine = context.Engines.Where(e => e.ModelName.Equals(vehicle.EngineReference.ModelName)).ToList().FirstOrDefault();
                if (engine is null)
                    await context.Engines.AddAsync(vehicle.EngineReference);
                else
                {
                    duplicate.EngineReference = engine;
                    duplicate.EngineId = engine.Id;
                }
            }

            await AddTank(vehicle, duplicate);
        }



        private async Task AddTank(MainBattleTank vehicle, MainBattleTank duplicate)
        {
            var existingTank = context.Tanks.ToList().Where(t => t.ModelName.Equals(duplicate.ModelName)).FirstOrDefault();
            if (existingTank is null)
            { 
                await context.Tanks.AddAsync(duplicate);
                context.SaveChanges();
            }
        }


        #endregion Logic




        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public TankReceptionChain(MainBattleTank vehicle, MainBattleTank duplicate, VehicleDatabaseContext context)
        {
            this.vehicle = vehicle;
            this.duplicate = duplicate;
            this.context = context;
        }

    }
}
