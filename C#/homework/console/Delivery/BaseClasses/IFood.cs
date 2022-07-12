using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BaseClasses
{
    /// <summary>
    /// Interface "Food";
    /// <br />
    /// Интерфейс "Еда";
    /// </summary>
    public interface IFood
    {

        /// <summary>
        /// The name of the course;
        /// <br />
        /// Имя блюда;
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// The energetic value of the course;
        /// <br />
        /// Пищевая ценность блюда;
        /// </summary>
        public double Calories { get; set; }


        /// <summary>
        /// The price of the course;
        /// <br />
        /// Стоимость блюда;
        /// </summary>
        public double Price { get; set; }


    }
}
