using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingRangeProject
{
    public class Logger
    {

        private ILogService? service;


        public void Log(string message) => service?.Write(message);


        public Logger(ILogService service)
        {
            this.service = service;
        }

        public Logger()
        {
            this.service = null;
        }

    }
}
