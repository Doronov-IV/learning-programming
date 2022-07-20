using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StockExchange.Clients;
using StockExchange.Goods;
using StockExchange.TopLevel;

using Tools.FileReader;

#pragma warning disable

namespace StockExchange.Service
{
    /// <summary>
    /// An event and entity generator for the exchange;
    /// <br />
    /// Генератор событий и сущностей для биржи;
    /// </summary>
    public class StockExchangeGenerator
    {


        #region PROPERTIES - Values that form the State of this class



        #region PRIVATE

        /// <summary>
        /// A list of all possible currencies;
        /// <br />
        /// Список всех возможных валют;
        /// </summary>
        private List<Currency> _CurrencyList = null;


        /// <summary>
        /// A list of all possible equities;
        /// <br />
        /// Список всех возможных ценных бумаг;
        /// </summary>
        private List<Equity> _EquityList = null;


        /// <summary>
        /// A list of all possible brockers;
        /// <br />
        /// Список всех возможных брокеров;
        /// </summary>
        private List<Broker> _BrockerList = null;


        /// <summary>
        /// A list of all possible banks;
        /// <br />
        /// Список всех возможных банков;
        /// </summary>
        private List<Bank> _BankList = null;

        #endregion PRIVATE



        #region PUBLIC

        /// <summary>
        /// @see _BanksList
        /// </summary>
        public List<Bank> BankList
        {
            get
            {
                return _BankList;
            }

            set
            {
                this._BankList = value;
            }
        }


        /// <summary>
        /// @see _BrockersList
        /// </summary>
        public List<Broker> BrokerList
        {
            get
            {
                return _BrockerList;
            }

            set
            {
                this._BrockerList = value;
            }
        }


        /// <summary>
        /// @see _EquityList
        /// </summary>
        public List<Equity> EquityList
        {
            get
            {
                return _EquityList;
            }

            set
            {
                this._EquityList = value;
            }
        }


        /// <summary>
        /// @see _CurrencyList
        /// </summary>
        public List<Currency> CurrencyList
        {
            get
            {
                return _CurrencyList;
            }

            set
            {
                this._CurrencyList = value;
            }
        }

        #endregion PUBLIC



        #region UTILITY

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private FileReader TextReader
        {
            set;
            get;
        }

        /// <summary>
        /// ;
        /// <br />
        /// ;
        /// </summary>
        private Random Random
        {
            set;
            get;
        }

        #endregion UTILITY



        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Get random new brocker;
        /// <br />
        /// Получить случайного брокера;
        /// </summary>
        /// <returns>New brocker;<br />Новый брокер;</returns>
        public (Broker Broker, Bank Bank) GetNewClientPair()
        {
            if (null == BrokerList || null == BankList) ReadFiles();

            return (BrokerList[new Random().Next() % BrokerList.Count], BankList[new Random().Next() % BankList.Count]);
        }


        /// <summary>
        /// Get random assets tuple;
        /// <br />
        /// Получить кортеж случайных ассетов;
        /// </summary>
        /// <returns>New tuple;<br />Новый кортеж;</returns>
        public (Equity Equity, Currency Currency) GetNewAssetPair()
        {
            if (null == EquityList || null == CurrencyList) ReadFiles();

            return (EquityList[new Random().Next() % EquityList.Count], CurrencyList[new Random().Next() % CurrencyList.Count]);
        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Read all json files needed;
        /// <br />
        /// Прочитать все необходимые json-файлы;
        /// </summary>
        private void ReadFiles()
        {
            ReadBrockers();
            ReadBanks();
            ReadEquities();
            ReadCurrency();
        }


        /// <summary>
        /// Read 'Brocker' file;
        /// <br />
        /// Прочитать файл 'Brocker';
        /// </summary>
        private void ReadBrockers()
        {
            BrokerList = new List<Broker>();

            List<string> BrockerNamesList = new List<string>();

            FileReader reader = new FileReader();

            BrockerNamesList = reader.ReadList("Data/TXT/BrockerNamePool.txt");

            foreach (var unit in BrockerNamesList)
            {
                Broker brocker = new Broker(unit);

                BrokerList.Add(brocker);
            }
        }


        /// <summary>
        /// Read 'Bank' file;
        /// <br />
        /// Прочитать файл 'Bank';
        /// </summary>
        private void ReadBanks()
        {
            BankList = new List<Bank>();

            List<string> BanksNamesList = new List<string>();

            FileReader reader = new FileReader();

            BanksNamesList = reader.ReadList("Data/TXT/BankNamePool.txt");

            foreach (var unit in BanksNamesList)
            {
                Bank bank = new Bank(unit);

                BankList.Add(bank);
            }
        }


        /// <summary>
        /// Read 'Equity' file;
        /// <br />
        /// Прочитать файл 'Equity';
        /// </summary>
        private void ReadEquities()
        {
            EquityList = new List<Equity>();

            FileReader<List<Equity>> reader = new FileReader<List<Equity>>();

            EquityList = reader.Read("Data/JSON/Asset/Equities.json");
        }


        /// <summary>
        /// Read 'Currency' file;
        /// <br />
        /// Прочитать файл 'Currency';
        /// </summary>
        private void ReadCurrency()
        {
            CurrencyList = new List<Currency>();

            FileReader<List<Currency>> reader = new FileReader<List<Currency>>();

            CurrencyList = reader.Read("Data/JSON/Asset/Currency.json");
        }


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static StockExchangeGenerator instance = null;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private StockExchangeGenerator()
        {
            TextReader = new FileReader();
            Random = new Random();
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static StockExchangeGenerator GetInstance()
        {
            if (instance == null)
            {
                instance = new StockExchangeGenerator();
            }
            return instance;
        }


        #endregion CONSTRUCTION



    }
}
