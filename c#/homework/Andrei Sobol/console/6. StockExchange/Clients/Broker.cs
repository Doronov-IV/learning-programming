using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


using StockExchange.Goods;
using StockExchange.TopLevel;


namespace StockExchange.Clients
{
    /// <summary>
    /// An individual client;
    /// <br />
    /// Клиент-физическое лицо;
    /// </summary>
    public class Broker : Client
    {


        #region PROPERTIES - Values that form the State of this class


        #region PRIVATE


        /// <summary>
        /// Check for consequent asset price change streak;
        /// <br />
        /// Проверка на последовательную серию изменений стоимости ассета;
        /// </summary>
        private byte ChangeStreak = 0;


        #endregion PRIVATE


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// React on changing asset price;
        /// <br />
        /// Отреагировать на изменение цены;
        /// </summary>
        /// <param name="asset">Asset to watch;<br />Отслеживаемый ассет;</param>
        /// <param name="mChangedValue">A difference in the value;<br />Изменённый показатель цены;</param>
        public override void OnPriceChange(Asset asset,  decimal mChangedValue)
        {
            // We'll need to note the new price either way;
            string sChangeValue = $"\t[blue] (Brocker) " + Name + ": I know that the price has changed! [/]" + 
                "[underline](" + mChangedValue.ToString("0.##") + ")[/]\n";

            // Check for the streak;
            MakeBuyingDecision(asset, mChangedValue, sChangeValue);
        }


        #endregion API



        #region LOGIC - Mostly private encapsulated auxiliary methods


        /// <summary>
        /// Make decision about whether to buy asset or not;
        /// <br />
        /// Принять решение о том, покупать ассет или нет;
        /// </summary>
        /// <param name="asset">Asset to watch;<br />Отслеживаемый ассет;</param>
        /// <param name="mChangedValue">A difference in the value;<br />Изменённый показатель цены;</param>
        /// <param name="sChangeValue">A essage of reacting to change;<br />Сообщение по поводу реакции на изменение;</param>
        private void MakeBuyingDecision(Asset asset, decimal mChangedValue, string sChangeValue)
        {
            Random random = new Random();

            if (ChangeStreak != 1 + random.Next() % 3)
            {
                if (mChangedValue > 0) ChangeStreak += 1;
                else ChangeStreak = 0;
                AnsiConsole.Write(new Markup(sChangeValue));
            }
            else
            {
                AnsiConsole.Write(new Markup(sChangeValue));
                AnsiConsole.Write(new Markup("\t[blue] (Brocker) " + Name + ": I've decided to buy the asset! [/]\n"));
                if (asset.Owner.GetType() == typeof(Bank)) ptrExchange.ChangeOwner(this, asset);
                ChangeStreak = 0;
            }
        }


        #endregion LOGIC



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Broker()
        {
            Name = "Unknown";
            Assets = new List<Asset>();
        }


        /// <summary>
        /// Name constructor;
        /// <br />
        /// Конструктор имени;
        /// </summary>
        /// <param name="sName">Name;<br />Имя;</param>
        public Broker(string sName) : this()
        {
            Name = sName;
        }


        #endregion CONSTRUCTION


    }
}
