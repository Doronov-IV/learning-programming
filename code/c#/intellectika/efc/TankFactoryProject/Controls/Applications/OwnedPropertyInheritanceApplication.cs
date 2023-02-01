using MainEntityProject.Model.Context;
using MainEntityProject.Controls.Common;

namespace MainEntityProject.Controls.Applications
{
    public class OwnedPropertyInheritanceApplication : IApplication
    {
        public async Task Start()
        {
            await using var context = new OwnedPropertyInheritanceContext(); 
        }
    }
}
