using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamlet.Service
{
    public static class TempToolbox
    {

        public static string TabWidth(string s, int w)
        {
            if (s.Length == 0 || s == " ")
            {
                string sRes = "\t\t";
                return sRes;
            }
            //w is the desired width
            int stringwidth = s.Length;
            int i;
            string resultstring = s;

            for (i = 0; i <= (w - stringwidth) / 6; i++)
            {
                resultstring = resultstring + "\t";
            }
            return resultstring;
        }

    }
}
