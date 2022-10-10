using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainNetworkingProject.Misc
{
    public interface IPendingOutput
    {

        public delegate void OutputDelegate(string sOutputMessage);

        public event OutputDelegate SendOutput;

        public void PendOutput(string Message);

    }
}
