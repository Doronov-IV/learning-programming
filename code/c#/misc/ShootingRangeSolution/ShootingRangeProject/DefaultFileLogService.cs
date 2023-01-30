using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShootingRangeProject
{
    public class DefaultFileLogService : ILogService
    {
        public void Write(string message)
        {
            string path = "..\\..\\..\\log.txt";

            if (!File.Exists(path))
            {
                File.Create(path);
            }
            
            File.WriteAllText(path, $"[{DateTime.Now.ToString("dd.MM.yy HH:mm:ss:fff")}] {message}.\n");
        }
    }
}
