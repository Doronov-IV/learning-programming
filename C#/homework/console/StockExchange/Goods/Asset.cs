using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using StockExchange.Clients;


namespace StockExchange.Goods
{
    /// <summary>
    /// Basic goods properties;
    /// <br />
    /// Базовые характеристики товара;
    /// </summary>
    public abstract class Asset
    {


        #region PROPERTIES - Values that form the State of this class


        #region PUBLIC


        /// <summary>
        /// Asset name;
        /// <br />
        /// Имя товара;
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Asset price in USD;
        /// <br />
        /// Стоимость товара в долларах США;
        /// </summary>
        public decimal Price { get; set; }


        /// <summary>
        /// A reference for the owner;
        /// <br />
        /// Ссылка на владельца;
        /// </summary>
        public Client Owner { get; set; }


        #endregion PUBLIC



        #region DELEGATES


        /// <summary>
        /// A handler to notify the brokers of price change;
        /// <br />
        /// Хендлер нотификации брокера об изменении стоимости;
        /// </summary>
        /// <param name="asset">The changed asset reference;<br />Ссылка на измененный ассет;</param>
        /// <param name="mChangedValue">The changed value;<br />Значение изменения;</param>
        public delegate void PriceChangedEventHandler(Asset asset, decimal mChangedValue);


        /// <summary>
        /// The "price has been changed" event;
        /// <br />
        /// Событие "изменилась стоимость";
        /// </summary>
        private event PriceChangedEventHandler _PriceChanged;


        /// <summary>
        /// @see _PriceChanged
        /// </summary>
        public event PriceChangedEventHandler PriceChanged
        {
            add 
            {
                _PriceChanged += value;
            }

            remove 
            {
                _PriceChanged -= value; 
            }
        }


        #endregion DELEGATES


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Change the price of the asset;
        /// <br />
        /// Изменить цену ассета;
        /// </summary>
        /// <param name="mChangedValue">Difference value;<br />Разность старого и нового значения;</param>
        public void ChangePrice(decimal mChangedValue)
        {
            Price += mChangedValue;

            _PriceChanged?.Invoke(this, mChangedValue);
        }



        #endregion API


    }
}
