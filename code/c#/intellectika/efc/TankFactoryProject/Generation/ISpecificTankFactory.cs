 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainEntityProject.Generation
{
    public interface ISpecificTankFactory
    {
        public MainBattleTank GetImportedTank();

        public MainBattleTank GetExportedTank();

        public MainBattleTank GetNativeTank();

    }
}
