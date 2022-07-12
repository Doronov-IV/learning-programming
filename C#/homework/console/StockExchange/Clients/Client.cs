using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using StockExchange.Goods;
using StockExchange.TopLevel;


namespace StockExchange.Clients
{
    /// <summary>
    /// To unite all traders;
    /// <br />
    /// Объединить всех клиентов;
    /// </summary>
    public abstract class Client
    {


        #region PROPERTIES - Values that form the State of this class


        #region PUBLIC


        /// <summary>
        /// A name of the brocker;
        /// <br />
        /// Имя брокера;
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }


        /// <summary>
        /// A list of Assets;
        /// <br />
        /// Список товаров;
        /// </summary>
        public List<Asset> Assets
        {
            get;
            protected set;
        }


        /// <summary>
        /// A reference to an exchange instance;
        /// <br />
        /// Ссылка на инстанс биржи;
        /// </summary>
        public Exchange ptrExchange
        {
            set;
            get;
        }


        #endregion PUBLIC



        #region DELEGATES
        

        /// <summary>
        /// Broker handler;
        /// <br />
        /// Хендлер брокера;
        /// </summary>
        /// <param name="asset">Tracked asset;<br />Отслеживаемый ассет;</param>
        public delegate void BrokerHandler(Asset asset);


        /// <summary>
        /// Notify exchange about client wanting to trade;
        /// <br />
        /// Оповестить биржу о том, что клиент хочет поторговать;
        /// </summary>
        private event BrokerHandler _NotifyAboutTrade;


        /// <summary>
        /// @see _NotifyAboutTrade
        /// </summary>
        public event BrokerHandler NotifyAboutTrade
        {
            add
            {
                _NotifyAboutTrade += value;
            }

            remove
            {
                _NotifyAboutTrade -= value;
            }
        }


        #endregion DELEGATES


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Remove asset;
        /// <br />
        /// Изъять товар;
        /// </summary>
        /// <param name="asset">The asset for removal;<br />Товар для изъятия;</param>
        /// <returns>false - asset is null or missing;<br />false - товар равен null или отсутствует;</returns>
        public virtual bool RemoveAsset(Asset asset)
        {
            bool res = false;

            // Check if asset is not null and is owned by this client;
            if (asset is not null && Assets.Contains(asset)) res = true;

            // Clear assets' owner reference;
            asset.Owner = null;

            // Actually get rid of it;
            Assets.Remove(asset);

            return res;
        }


        /// <summary>
        /// Add asset;
        /// <br />
        /// Добавить товар;
        /// </summary>
        /// <param name="asset">The asset for addition;<br />Товар для изъятия;</param>
        public virtual void AddAsset(Asset asset)
        {
            if (asset is not null)
            {
                asset.Owner = this;

                Assets.Add(asset);
            }
            else throw new ArgumentNullException("You shouldn't handle null into the argument list.");
        }


        /// <summary>
        /// React on changing asset price;
        /// <br />
        /// Отреагировать на изменение цены;
        /// </summary>
        /// <param name="asset">Asset to watch;<br />Отслеживаемый ассет;</param>
        /// <param name="mChangedValue">A difference in the value;<br />Изменённый показатель цены;</param>
        public abstract void OnPriceChange(Asset asset, decimal mChangedValue);


        #endregion API


    }
}
