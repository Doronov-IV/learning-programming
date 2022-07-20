using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tools.Formatting;
using Tools.FileReader;

using StockExchange.Clients;
using StockExchange.Goods;
using StockExchange.Service;

using Spectre.Console;

#pragma warning disable

namespace StockExchange.TopLevel
{
    /// <summary>
    /// A wrapper for demonstration Exchange's life;
    /// <br />
    /// Обёртка для симуляции работы биржи;
    /// </summary>
    public class ExchangeSimulator
    {


        #region PROPERTIES - Values that form the State of this class



        #region PRIVATE - Internal components


        /// <summary>
        /// List of all possible brokers;
        /// <br />
        /// Лист всех возможных брокеров;
        /// </summary>
        private List<Broker> OverallBrokers { get; set; }


        /// <summary>
        /// List of all ACTIVE brokers;
        /// <br />
        /// An active broker is the one, who's currently playing;
        /// <br />
        /// Лист всех АКТИВНЫХ брокеров;
        /// <br />
        /// Активный брокер - это тот, что в данный момент участвует в игре;
        /// </summary>
        private List<Broker> ActiveBrokers { get; set; }


        /// <summary>
        /// List of all possible banks;
        /// <br />
        /// Лист всех возможных банков;
        /// </summary>
        private List<Bank> Banks { get; set; }


        /// <summary>
        /// List of all possible equities;
        /// <br />
        /// Лист всех возможных ценных бумаг;
        /// </summary>
        private List<Equity> Equities { get; set; }


        /// <summary>
        /// A pointer to The Exchange class that we are running;
        /// <br />
        /// Указатель на экземпляр класса "Exchange", который мы симулируем;
        /// </summary>
        private Exchange ptrExchange { get; set; }


        #endregion PRIVATE



        #region UTILITY - Such as: file readers, primitive and object randomizers etc.

        /// <summary>
        /// A pointer of a service that provides project-specific instances of random properties;
        /// <br />
        /// Ссылка на сервис, который предоставляет набор случайно сгенерированных сущностей, характерных данному проекту;
        /// </summary>
        private StockExchangeGenerator generator;

        /// <summary>
        /// Simple text file reader;
        /// <br />
        /// Простой ридер файлов;
        /// </summary>
        private FileReader Reader;

        /// <summary>
        /// An entity that provides random primitive data values;
        /// <br />
        /// Сущность, которая предоставляет случайные данные примитивных типов;
        /// </summary>
        private Random Random;

        #endregion UTILITY



        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Run a loop of demonstration of the exchange processes;
        /// <br />
        /// Запустить цикл демонстрации работы биржи;
        /// </summary>
        public void RunDemo()
        {
            // Fill the make the exchange object;
            GenerateExchange();

            // Select a random bank;
            Bank RandomBank = Banks[Random.Next() % Banks.Count];

            // Add an asset to it;
            RandomBank.AddAsset(generator.GetNewAssetPair().Equity);

            // Add this asset to the exchange;
            ptrExchange.AddAssetForTrade(RandomBank, RandomBank.Assets[0]);

            // Show 'before';
            ShowBeforeAfterChanges(ref RandomBank);

            // The main loop;
            StartTrade(ref RandomBank);

            // Show 'after';
            ShowBeforeAfterChanges(ref RandomBank);

            // Dispose the data;
            ptrExchange.Clients.Clear();
            ActiveBrokers.ForEach(el => el.Assets.Clear());
            ActiveBrokers.Clear();
        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Generate and fill new Exchange;
        /// <br />
        /// Создать и заполнить новую биржу;
        /// </summary>
        private void GenerateExchange()
        {
            if (OverallBrokers == null || Equities == null)
            {
                GenerateAssets();
                GenerateClients();
            }

            ptrExchange = Exchange.GetInstance();

            Random random = new Random();

            Broker client;

            for (int i = 0, iSize = random.Next(1, OverallBrokers.Count - 1); i < iSize; i++)
            {
                client = OverallBrokers[i];
                ActiveBrokers.Add(client);
                ptrExchange.AddClient(client);
            }
        }


        /// <summary>
        /// Print clients' assets;
        /// <br />
        /// Вывести активы клиентов;
        /// </summary>
        /// <param name="bank">Active bank;<br />Активный банк;</param>
        private void ShowBeforeAfterChanges(ref Bank bank)
        {
            // Print for brokers;
            foreach (var broker in ActiveBrokers)
            {
                AnsiConsole.Write(new Markup("\n\t[Blue]" + broker.Name + "[/], Assets: "));
                broker.Assets.ForEach(el => AnsiConsole.Write(el.Name + " "));
            }

            // Print for the bank;
            AnsiConsole.Write(new Markup("\n\t[Red]" + bank.Name + "[/], Assets: "));
            bank.Assets.ForEach(el => AnsiConsole.Write(el.Name + " "));
            AnsiConsole.WriteLine("");
        }


        /// <summary>
        /// Begin the main loop, where brokers watch the prices and buy assets;
        /// <br />
        /// Начать основной цикл, в котором брокеры следят за ценами и покупают товары;
        /// </summary>
        /// <param name="bank">Active bank;<br />Активный банк;</param>
        private void StartTrade(ref Bank bank)
        {
            // Start the trade;
            AnsiConsole.WriteLine("\n\n\t Trades were started!");

            // The main loop, where bank changes prices etc;
            while (ptrExchange.AssetsForTrade.Count != 0)
            {
                bank.ChangeEquityPrice((decimal)(1.5d - (Random.NextDouble() + 1d)));
                Thread.Sleep(500);
            }

            // Finish the trade;
            AnsiConsole.WriteLine("\n\t Trades were finished!\n");
        }


        #region MISC - All the rest methods for construction;


        /// <summary>
        /// Generate the "Brokers" and "Banks" Lists;
        /// <br />
        /// Сгенерировать списки "Brokers" и "Banks";
        /// </summary>
        private void GenerateClients()
        {
            //
            // <fill brokers>
            OverallBrokers = new List<Broker>();
            ActiveBrokers = new List<Broker>();
            var ListCounter = Reader.ReadList("Data/TXT/BrockerNamePool.txt");
            var unitBroker = generator.GetNewClientPair().Broker;
            OverallBrokers.Add(unitBroker);
            while (OverallBrokers.Count != ListCounter.Count)
            {
                unitBroker = generator.GetNewClientPair().Broker;

                if (!OverallBrokers.Contains(unitBroker)) OverallBrokers.Add(unitBroker);
            }
            // <fill brokers />
            //
            //
            //
            // <fill banks>
            Banks = new List<Bank>();
            ListCounter = Reader.ReadList("Data/TXT/BankNamePool.txt");
            var unitBank = generator.GetNewClientPair().Bank;
            Banks.Add(unitBank);
            while (Banks.Count != ListCounter.Count)
            {
                unitBank = generator.GetNewClientPair().Bank;

                if (!Banks.Contains(unitBank)) Banks.Add(unitBank);
            }
            // <fill banks />
            //
        }


        /// <summary>
        /// Generate the "Asset" List;
        /// <br />
        /// Сгенерировать список "Asset";
        /// </summary>
        private void GenerateAssets()
        {

            FileReader<List<Equity>> reader = new FileReader<List<Equity>>();

            Equities = new List<Equity>();

            var ListCounter = reader.Read("Data/JSON/Asset/Equities.json");

            var unit = generator.GetNewAssetPair().Equity;

            Equities.Add(unit);

            while (Equities.Count != ListCounter?.Count)
            {
                unit = generator.GetNewAssetPair().Equity;

                if (!Equities.Contains(unit)) Equities.Add(unit);
            }
        }


        #endregion MISC


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static ExchangeSimulator instance = null;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private ExchangeSimulator()
        {
            // Utility;
            generator = StockExchangeGenerator.GetInstance();
            Random = new Random();
            Reader = new FileReader();

            // Private;
            OverallBrokers = null;
            Equities = null;
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static ExchangeSimulator GetInstance()
        {
            if (instance == null)
            {
                instance = new ExchangeSimulator();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
