using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.BaseClasses
{
    /// <summary>
    /// A class for tracking meals;
    /// <br />
    /// Класс для учёта заказов;
    /// </summary>
    public class Order
    {


        #region CONSTANTS


        /// <summary>
        /// Preset number One;
        /// <br />
        /// Пресет номер один;
        /// </summary>
        public static readonly int PRESET_ONE = 3;


        /// <summary>
        /// Preset number Two;
        /// <br />
        /// Пресет номер два;
        /// </summary>
        public static readonly int PRESET_TWO = 4;


        /// <summary>
        /// Preset number Three;
        /// <br />
        /// Пресет номер три;
        /// </summary>
        public static readonly int PRESET_THREE = 5;


        /// <summary>
        /// Courses in preset number One;
        /// <br />
        /// Блюд в пресете номер один;
        /// </summary>
        public static readonly int PRESET_ONE_COURSES = 2;


        /// <summary>
        /// Courses in preset number Two;
        /// <br />
        /// Блюд в пресете номер два;
        /// </summary>
        public static readonly int PRESET_TWO_COURSES = 3;


        /// <summary>
        /// Courses in preset number Three;
        /// <br />
        /// Блюд в пресете номер три;
        /// </summary>
        public static readonly int PRESET_THREE_COURSES = 4;


        /// <summary>
        /// Amount of drinks in all presets;
        /// <br />
        /// Кол-во напитков в каждом пресете;
        /// </summary>
        public static readonly int ONE_FOR_DRINK = 1;


        /// <summary>
        /// The amount of meals;
        /// <br />
        /// Кол-во приёмов пищи;
        /// </summary>
        public static readonly int MEALS_AMOUNT = 5;


        #endregion CONSTANTS



        #region PROPERTIES


        /// <summary>
        /// The amount of days;
        /// <br />
        /// Кол-во дней;
        /// </summary>
        public int DaysQuantity { get; set; }


        /// <summary>
        /// The number of an order preset;
        /// <br />
        /// Номер заказа;
        /// </summary>
        public int PresetNumber { get; set; }


        /// <summary>
        /// Overall order price;
        /// <br />
        /// Общая стоимость заказа;
        /// </summary>
        private double _OverallPrice;


        /// <summary>
        /// @see _OverallPrice
        /// </summary>
        public double OverallPrice 
        {   
            get
            {
                return _OverallPrice;
            }

            private set
            {
                _OverallPrice = value;
            }
        }


        /// <summary>
        /// Ovarall order courses calories value;
        /// <br />
        /// Общая калорийность всех блюд в заказе;
        /// </summary>
        private double _OverallCalories;


        /// <summary>
        /// @see _OverallCalories
        /// </summary>
        public double OverallCalories
        {
            get
            {
                return _OverallCalories;
            }

            private set
            {
                _OverallCalories = value;
            }
        }
        

        /// <summary>
        /// A list of all courses. The outer dim represents days, the inner stands for courses themselves;
        /// <br />
        /// Список всех блюд. Первое измерение отвечает за дни, второе представляет собой сами блюда;
        /// </summary>
        public List<List<List<IFood>>> FoodList { get; set; }


        /// <summary>
        /// A value of the discount;
        /// <br />
        /// Значение скидки;
        /// </summary>
        private int _DiscountValueInPercent = 0;


        /// <summary>
        /// @see _DiscountValueInPercent
        /// </summary>
        public int DiscountValueInPercent
        {
            get
            {
                return _DiscountValueInPercent;
            }

            set
            {
                if (0 > value || value > 100) throw new ArgumentOutOfRangeException("The discount is measured in values from 0 to 100.");
                else _DiscountValueInPercent = value;
            } 
        }


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// Initialize the food list;
        /// <br />
        /// Проинициализировать список блюд;
        /// </summary>
        private void InitList()
        {
            this.FoodList = new List<List<List<IFood>>>();
        }


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Order()
        {
            InitList();
        }


        /// <summary>
        /// Constructor with properties;
        /// <br />
        /// Конструктор со значениями;
        /// </summary>
        /// <param name="nDaysQuantity">The amount of days;<br />Кол-во дней;</param>
        /// <param name="nPresetNumber">The number of the preset;<br />Номер пресета;</param>
        public Order(int nDaysQuantity, int nPresetNumber) : this()
        {
            DaysQuantity = nDaysQuantity;
            PresetNumber = nPresetNumber;
        }


        #endregion CONSTRUCTION



        #region AUXILIARY


        /// <summary>
        /// Calculate Discount and change the overall price;
        /// <br />
        /// Посчитать итоговую скидку и скорректировать под неё цену;
        /// </summary>
        public void CalculateDiscount()
        {
            // If it hasn't been done yet;
            if (DiscountValueInPercent == 0)
            {
                // Calculate overall values;
                OverallPrice = FoodList.SelectMany(n => n.SelectMany(el => el.Select(unit => unit.Price))).Sum();
                OverallCalories = FoodList.SelectMany(n => n.SelectMany(el => el.Select(unit => unit.Calories))).Sum();


                // Calculate discount;
                if (DaysQuantity < 3) DiscountValueInPercent = 0;
                else if (DaysQuantity < 5) DiscountValueInPercent = 3;
                else if (DaysQuantity < 10) DiscountValueInPercent = 5;
                else DiscountValueInPercent = 10;

                
                // Apply the discount;
                OverallPrice = OverallPrice - (OverallPrice * ((double)DiscountValueInPercent / 100));
            }
            else return;
        }


        #endregion AUXILIARY

    }
}
