using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingRangeProject
{
    public class ShortTimeService : ITimeService
    {

        public string GetTime() => DateTime.Now.ToString("HH:mm");

    }
}
