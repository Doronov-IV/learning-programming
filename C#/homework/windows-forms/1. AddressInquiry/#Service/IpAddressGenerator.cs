using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;
using Tools.FileReader;

namespace AddressInquiry.Service
{
    /// <summary>
    /// A generator for ip addresses;
    /// <br />
    /// Генератор случайных адресов;
    /// </summary>
    public static class IpAddressProvider
    {

        /// <summary>
        /// Get random address from the list;
        /// <br />
        /// Получить случайный адрес из списка;
        /// </summary>
        /// <returns>Address string;<br />адрес в строке;</returns>
        public static string Generate()
        {
            Random random = new Random();
            var lRes = GetAddressList();
            return lRes[random.Next() % lRes.Count];
        }


        /// <summary>
        /// Get list of valid ip addresses;
        /// <br />
        /// Получить список действительных ip адресов;
        /// </summary>
        /// <returns>List of adresses;<br />Лист адресов;</returns>
        public static List<string> GetAddressList()
        {
            FileReader reader = new FileReader();
            var AddressList = reader.ReadList("#Data/Addresses.txt");
            return AddressList;
        }

    }
}
