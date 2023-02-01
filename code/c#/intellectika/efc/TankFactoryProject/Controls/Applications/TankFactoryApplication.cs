using MainEntityProject.Controls.Common;
using MainEntityProject.Model.Context;

namespace MainEntityProject.Controls.Applications
{
    public class TankFactoryApplication : IApplication
    {

        public async Task Start()
        {
            await using var context = new VehicleDatabaseContext();
        }

    }
}
