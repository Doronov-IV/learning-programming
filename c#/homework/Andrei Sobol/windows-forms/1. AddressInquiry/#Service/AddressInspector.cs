using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AddressInquiry.Service
{
    /// <summary>
    /// Address check;
    /// <br />
    /// Проверка адресов;
    /// </summary>
    public static class AddressInspector
    {

        /// <summary>
        /// Inspect the address;
        /// <br />
        /// Проверить адрес;
        /// </summary>
        /// <param name="sAddress">Address value;<br />Значение адреса;</param>
        /// <returns></returns>
        public static bool Inspect(string sAddress)
        { 
            string sAddressCopy = sAddress;

            // If there is a port number, cut it out;
            if (sAddressCopy.Contains(":"))
            {
                var temp = sAddress.Split(":");

                sAddressCopy = temp.FirstOrDefault();
            }

            // <check ip>
            string sBunchOfDigits = @"(0|[0-9]|[0-9][0-9]|1[0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])";

            string sAdressPattern = @"^(" + @sBunchOfDigits + @"\." + @"){3}" + @sBunchOfDigits + @"$";

            Match AddressMatch = Regex.Match(sAddressCopy, sAdressPattern);
            // <check ip />

            sAddressCopy = sAddress;

            // It there's a port number, once again, check it;
            if (sAddressCopy.Contains(":"))
            {
                var temp = sAddress.Split(":");

                sAddressCopy = temp.LastOrDefault();

                string sPortPattern = @"^[0-5][0-9][0-9][0-9][0-9]|6[0-4][0-9][0-9][0-9]|65[4][0-9][0-9]|655[0-2][0-9]|6553[0-5]|\d{1,4}$";

                Match PortMatch = Regex.Match(sAddressCopy, sPortPattern);

                return AddressMatch.Success && PortMatch.Success;
            }

            return AddressMatch.Success;
        }

    }
}
