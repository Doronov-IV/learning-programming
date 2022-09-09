using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Delivery.BaseClasses;

namespace Delivery.Builder
{
    /// <summary>
    /// A "Director" for the "Builder";
    /// <br />
    /// "Директор" для "строителя";
    /// </summary>
    public partial class OrderDirector
    {


        #region PROPERTIES


        /// <summary>
        /// The amount of days;
        /// <br />
        /// Кол-во дней;
        /// </summary>
        private int _DaysNumber;


        /// <summary>
        /// @see _DaysNumber
        /// </summary>
        public int DaysNumber
        {
            get { return _DaysNumber; }
            set 
            {
                if (_DaysNumber <= 0) _DaysNumber = value;
                else throw new ArgumentException("You cannot have amount of days less then or equal to zero.");
            }
        }


        /// <summary>
        /// A number of a preset;
        /// <br />
        /// Номер пресета;
        /// </summary>
        private int _PresetNumber;


        /// <summary>
        /// @see _PresetNumber
        /// </summary>
        public int PresetNumber 
        {
            get { return _PresetNumber; }
            set
            {
                if (0 > value && value > 3) 
                    throw new ArgumentException();
                else _PresetNumber = value;
            }
        }


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public OrderDirector()
        {
            PresetNumber = 1;
        }


        /// <summary>
        /// Preset number constructor;
        /// <br />
        /// Конструктор с номером набора;
        /// </summary>
        /// <param name="nPresetNumber">Preset number;<br />Номер набора;</param>
        public OrderDirector(int nPresetNumber)
        {
            PresetNumber = nPresetNumber;
        }


        #endregion CONSTRUCTION



        #region LOGIC


        /// <summary>
        /// Build an order;
        /// <br />
        /// Построить заказ;
        /// </summary>
        /// <returns>Order;<br />Заказ;</returns>
        public Order Build()
        {
            OrderBuilder builder = new OrderBuilder(this);

            return builder.Build();
        }


        #endregion LOGIC


    }
}
