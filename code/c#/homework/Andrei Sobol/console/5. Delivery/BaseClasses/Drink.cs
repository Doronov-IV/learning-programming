using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BaseClasses
{
    /// <summary>
    /// Class "Drink" which is different from "Course" onlt by name;
    /// <br />
    /// Класс "Напиток", который отличается от класса "Блюдо" только названием;
    /// </summary>
    public class Drink : Course
    {

        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Drink()
        {

        }


        /// <summary>
        /// Constructor with properties;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="sName">Name of the drink;<br />Имя напитка;</param>
        /// <param name="dCalories">The energetic value of the drink;<br />Пищевая ценность напитка;</param>
        /// <param name="dPrice">The price of the drink;<br /> Стоимость напитка;</param>
        public Drink(string sName, double dCalories, double dPrice) : base(sName, dCalories, dPrice)
        {

        }


        #endregion CONSTRUCTION

    }
}
