using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Delivery.BaseClasses;

using Delivery.Service;


namespace Delivery.Builder
{

    /// <summary>
    /// @see OrderDirecotr.cs
    /// </summary>
    public partial class OrderDirector
    {
        /// <summary>
        /// An order builder;
        /// <br />
        /// Строитель заказа;
        /// </summary>
        private class OrderBuilder
        {


            #region PROPERTIES


            /// <summary>
            /// A pointer to the Order;
            /// <br />
            /// Указатель на заказ;
            /// </summary>
            public Order Order { get; set; }


            /// <summary>
            /// A reference to the director to get the preset number;
            /// <br />
            /// Референс на директора, чтобы получить номер пресета;
            /// </summary>
            public OrderDirector DirectorReference { get; set; }


            #endregion PROPERTIES



            #region CONSTRUCTION


            /// <summary>
            /// A constructor with a reference to the director;
            /// <br />
            /// Конструктор с ссылкой на директора;
            /// </summary>
            /// <param name="ptrDirector">A reference to the director;<br />Ссылка на директора;</param>
            public OrderBuilder(OrderDirector ptrDirector)
            {
                this.DirectorReference = ptrDirector;
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
                // Create an order;
                if (this.Order == null) Order = new Order();

                // Pass directors info to the order;
                Order.DaysQuantity = DirectorReference.DaysNumber;
                Order.PresetNumber = DirectorReference.PresetNumber;

                FoodGenerator foodGenerator = FoodGenerator.GetInstance();

                // Convert preset number to amount of courses to generate;
                int nPresetToCourseNumberConverter = 0;
                switch (DirectorReference.PresetNumber)
                {
                    case 1:
                        nPresetToCourseNumberConverter = Order.PRESET_ONE_COURSES;
                        break;
                    case 2:
                        nPresetToCourseNumberConverter = Order.PRESET_TWO_COURSES;
                        break;
                    case 3:
                        nPresetToCourseNumberConverter = Order.PRESET_THREE_COURSES;
                        break;
                    default:
                        throw new Exception("Your haven't specified preset number.");
                }

                List<IFood> CurrentFoodList;
                List<List<IFood>> CurrentMealsList;

                // Add some courses for all days;
                for (int i = 0, iSize = DirectorReference.DaysNumber; i < iSize; ++i)
                {
                    CurrentMealsList = new List<List<IFood>>();
                    for (int v = 0, vSize = 5; v < vSize; ++v)
                    {
                        CurrentFoodList = new List<IFood>();
                        for (int j = 0, jSize = nPresetToCourseNumberConverter; j < jSize; ++j)
                        {
                            CurrentFoodList.Add(foodGenerator.GetFood().Course);
                        }
                        CurrentFoodList.Add(foodGenerator.GetFood().Drink);
                        CurrentMealsList.Add(CurrentFoodList);
                    }
                    Order.FoodList.Add(CurrentMealsList);
                }

                // Extremely important instruction;
                Order.CalculateDiscount();

                return this.Order;
            }


            #endregion LOGIC



        }
    }
}
