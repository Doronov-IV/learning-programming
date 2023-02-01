using TankFactory.Model.Context;
using TankFactory.Controls.Common;

namespace TankFactory.Controls.Applications
{
    public class OwnedPropertyInheritanceApplication : IApplication
    {
        public async Task Start()
        {
            await using var context = new OwnedPropertyInheritanceContext(); 
        }
    }
}
