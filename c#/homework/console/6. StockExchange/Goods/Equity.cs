using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using StockExchange.Clients;


namespace StockExchange.Goods
{
    /// <summary>
    /// Some valuable paper;
    /// <br />
    /// Некая ценная бумага;
    /// </summary>
    public class Equity : Currency
    {

        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Equity() : base()
        {

        }


        /// <summary>
        /// Name constructor;
        /// <br />
        /// Конструктор с именем;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        public Equity(string sName) : base(sName)
        {
            
        }


        /// <summary>
        /// Properties constructor;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        /// <param name="mPrice">Price;<br />Стоимость;</param>
        public Equity(string sName, decimal mPrice) : base(sName, mPrice)
        {

        }


        /// <summary>
        /// Full properties constructor;
        /// <br />
        /// Конструктор со всеми параметрами;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        /// <param name="mPrice">Price;<br />Стоимость;</param>
        /// <param name="Owner">Owner;<br />Владелец;</param>
        public Equity(string sName, decimal mPrice, Client Owner) : base(sName, mPrice)
        {
            this.Owner = Owner;
        }


        #endregion CONSTRUCTION

    }
}
