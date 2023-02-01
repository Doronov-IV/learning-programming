using TankFactory.Controls.Common;
using TankFactory.Model.Context;

namespace TankFactory.Controls.Applications
{
    public class TankFactoryApplication : IApplication
    {

        public async Task Start()
        {
            await using var context = new VehicleDatabaseContext();
        }

    }
}
