using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


using StockExchange.Goods;


namespace StockExchange.Clients
{
    /// <summary>
    /// Класс "Банк";
    /// <br />
    /// "Bank" class;
    /// </summary>
    public class Bank : Client
    {


        #region PROPERTIES - Values that form the State of this class


        //
        // @see Brocker.cs
        //


        #endregion PROPERTIES



        #region API - Contract


        /// <summary>
        /// Change price value of an equity;
        /// <br />
        /// Изменить стоимость ценной бумаги;
        /// </summary>
        /// <param name="mChangedValue">A value that will be added to the current one;<br />Значение, которое будет присуммированно к текущему;</param>
        public void ChangeEquityPrice(decimal mChangedValue)
        {
            if (Assets.Count > 0)
            {
                if (Assets.Count != 0)
                {
                    /*
                         :\

                        Тут была какая-то дикая ошибка:

                        — "Collection was modified; enumeration operation may not execute". Я так понимаю, это особенность цикла foreach.

                        Вот здесь: https://stackoverflow.com/questions/604831/collection-was-modified-enumeration-operation-may-not-execute
                    
                        Предложили привести коллекцию к листу/массиву, и оно заработало.
                     */

                    AnsiConsole.Write(new Markup("\n\t[red] (Bank) " + this.Name + ": I'm gonna change an asset price![/]\n"));

                    foreach (var unit in Assets.ToArray())
                    {
                        unit.ChangePrice(mChangedValue);
                    }
                }
            }
        }


        /// <summary>
        /// If someone changed their price, change ours;
        /// <br />
        /// Если кто-то поменял цену, поменять у себя;
        /// </summary>
        /// <param name="asset">A good with price changed;<br />Товар с изменённой ценой;</param>
        /// <param name="mChangedValue">Difference in price value;<br />Изменение в значении стоимости;</param>
        public override void OnPriceChange(Asset asset, decimal mChangedValue)
        {
            Random random = new Random();

            if (asset.Owner != this) ChangeEquityPrice(random.Next() % 2m);
        }


        #endregion API



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Bank()
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
        public Bank(string sName) : this()
        {
            Name = sName;
        }


        #endregion CONSTRUCTION


    }
}
