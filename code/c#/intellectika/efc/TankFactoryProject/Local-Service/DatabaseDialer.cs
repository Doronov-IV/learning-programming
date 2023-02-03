using MainEntityProject.Controls.Common;
using MainEntityProject.Model.Context;

namespace MainEntityProject.LocalService
{
    public class DatabaseDialer
    {

        private IApplication? dependencyApplication = null;


        private VehicleDatabaseContext? context = null;


        #region API



        public async Task AddTank(MainBattleTank vehicle)
        {
            if (context is null) SetupContext();

            if (vehicle is not null)
            {
                MainBattleTank duplicate = (MainBattleTank)vehicle.Clone();

                TankReceptionChain chain = new (vehicle, duplicate, context);

                await chain.Start();
            }

            else
                Console.WriteLine("Null object was passed to the database.");
        }



        #endregion API



        #region Logic



        private void SetupContext()
        {
            if (context is null && dependencyApplication is not null)
                context = dependencyApplication.GetProvider().GetRequiredService<VehicleDatabaseContext>();
        }



        #endregion Logic





        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public DatabaseDialer(IApplication dependency)
        {
            dependencyApplication = dependency;
        }

    }
}
