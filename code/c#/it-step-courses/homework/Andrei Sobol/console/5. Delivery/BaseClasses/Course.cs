using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BaseClasses
{
    /// <summary>
    /// Class "Course" (i.e. "Meal");
    /// <br />
    /// Класс "Блюдо";
    /// </summary>
    public class Course : IFood
    {


        #region PROPERTIES


        /// <summary>
        /// @see IFood.cs
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// @see IFood.cs
        /// </summary>
        public double Calories { get; set; }


        /// <summary>
        /// @see IFood.cs
        /// </summary>
        public double Price { get; set; }


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Course()
        {

        }


        /// <summary>
        /// Constructor with properties;
        /// <br />
        /// Конструктор с параметрами;
        /// </summary>
        /// <param name="sName">Name of the course;<br />Имя блюда;</param>
        /// <param name="dCalories">The energetic value of the course;<br />Пищевая ценность блюда;</param>
        /// <param name="dPrice">The price of the course;<br /> Стоимость блюда;</param>
        public Course(string sName, double dCalories, double dPrice)
        {
            Name = sName;
            Calories = dCalories;
            Price = dPrice;
        }


        #endregion CONSTRUCTION


    }
}
