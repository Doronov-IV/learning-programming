using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


using StockExchange.Clients;
using StockExchange.Goods;


namespace StockExchange.TopLevel
{
    /// <summary>
    /// Exchange class (S);
    /// <br />
    /// Класс "Биржа" (С);
    /// </summary>
    public class Exchange
    {


        #region PROPERTIES - Values that form the State of this class


        #region PUBLIC


        /// <summary>
        /// A list of all active clients;
        /// <br />
        /// Список активных клиентов;
        /// </summary>
        public List<Client> Clients { get; set; }


        /// <summary>
        /// A list of all assets to trade;
        /// <br />
        /// Список активных ассетов на продажу;
        /// </summary>
        public List<Asset> AssetsForTrade { get; set; }


        #endregion PUBLIC



        #region DELEGATES


        /// <summary>
        /// A handler to notify the brokers of owner change;
        /// <br />
        /// Хендлер нотификации брокера об изменении владельца;
        /// </summary>
        /// <param name="asset">The changed asset reference;<br />Ссылка на измененный ассет;</param>
        public delegate void OwnerChangedEventHandler(Client NewClient, Asset asset);


        /// <summary>
        /// The "owner has been changed" event;
        /// <br />
        /// Событие "изменился владелец";
        /// </summary>
        private event OwnerChangedEventHandler _OwnerChanged;


        /// <summary>
        /// @see _OwnerChanged
        /// </summary>
        public event OwnerChangedEventHandler OwnerChanged
        {
            add
            {
                _OwnerChanged += value;
            }

            remove
            {
                _OwnerChanged -= value;
            }
        }


        #endregion DELEGATES


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Add an asset for trade;
        /// <br />
        /// Добавить ассет для продажи;
        /// </summary>
        /// <param name="asset">A good to trade;<br />Товар на продажу;</param>
        public void AddAssetForTrade(Client client, Asset asset)
        {
            if (null != asset)
            {
                AssetsForTrade.Add(asset);

                OwnerChanged += OnAssetBought;

                foreach (var unit in Clients)
                {
                    asset.PriceChanged += unit.OnPriceChange;
                }
            }
        }


        /// <summary>
        /// Change the price of the asset;
        /// <br />
        /// Изменить цену ассета;
        /// </summary>
        /// <param name="mChangedValue">Difference value;<br />Разность старого и нового значения;</param>
        public void ChangeOwner(Client NewClient, Asset asset)
        {
            asset.Owner.Assets.Remove(asset);

            asset.Owner = NewClient;

            NewClient.Assets.Add(asset);

            _OwnerChanged?.Invoke(NewClient, asset);
        }


        /// <summary>
        /// Add a client;
        /// <br />
        /// Добавить клиента;
        /// </summary>
        /// <param name="brocker">New client;<br />Новый клиент;</param>
        public void AddClient(Broker brocker)
        {
            if (null != brocker)
            {
                Clients.Add(brocker);
                brocker.ptrExchange = this;
            }
        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Do when client buys an asset;
        /// <br />
        /// Выполнить, когда клиент купит ассет;
        /// </summary>
        /// <param name="asset">The bought asset;<br />Проданный ассет;</param>
        private void OnAssetBought(Client NewClient, Asset asset)
        {
            if (AssetsForTrade.Contains(asset))
            {
                // Unsubscribe clients of that equity;
                foreach (var client in Clients)
                {
                    asset.PriceChanged -= client.OnPriceChange;
                }

                // Remove equity from goods list of Exchange class;
                AssetsForTrade.Remove(asset);

                AnsiConsole.Write(new Markup($"\t[yellow]>>> Exchange: an asset {asset.Name} has ben sold to {NewClient.Name}.[/]\n"));

                ChangeOwner(NewClient, asset);
            }
        }


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static Exchange instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private Exchange()
        {
            Clients = new List<Client>();
            AssetsForTrade = new List<Asset>();
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static Exchange GetInstance()
        {
            if (instance == null)
            {
                instance = new Exchange();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
