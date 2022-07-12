using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using StockExchange.Clients;


namespace StockExchange.Goods
{
    public class Currency : Asset
    {

        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Currency()
        {
            Name = "Unknown";
            Price = 0.0m;
            Owner = null;
        }


        /// <summary>
        /// Name constructor;
        /// <br />
        /// Конструктор с именем;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        public Currency(string sName) : this()
        {
            Name = sName;
            Price = 0.0m;
        }


        /// <summary>
        /// Properties constructor;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        /// <param name="mPrice">Price;<br />Стоимость;</param>
        public Currency(string sName, decimal mPrice) : this()
        {
            Name = sName;
            Price = mPrice;
        }


        /// <summary>
        /// Full properties constructor;
        /// <br />
        /// Конструктор со всеми параметрами;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        /// <param name="mPrice">Price;<br />Стоимость;</param>
        /// <param name="Owner">Owner;<br />Владелец;</param>
        public Currency(string sName, decimal mPrice, Client Owner) : this(sName, mPrice)
        {
            this.Owner = Owner;
        }


        #endregion CONSTRUCTION

    }
}
