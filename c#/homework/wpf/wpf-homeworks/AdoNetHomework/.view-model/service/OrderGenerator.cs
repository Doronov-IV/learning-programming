using AdoNetHomework.Model;

namespace AdoNetHomework.Service
{
    /// <summary>
    /// Order generator;
    /// <br />
    /// Генератор заказов;
    /// </summary>
    public class OrderGenerator
    {


        #region PROPERTIES


        /// <summary>
        /// A reference of 'System.Random' instance;
        /// <br />
        /// Ссылка на копию класса "System.Random";
        /// </summary>
        private Random random;


        #endregion PROPERTIES




        #region API


        /// <summary>
        /// Get new random order;
        /// <br />
        /// Получить новый случайный заказ;
        /// </summary>
        /// <param name="nUserTableCount">
        /// Current users quantity;
        /// <br />
        /// Текущее кол-мо пользователей;
        /// </param>
        /// <returns></returns>
        public Order GetRandomOrder(int nUserTableCount)
        {
            return new Order(id: int.MaxValue, customerId: random.Next(0, nUserTableCount), summ: random.NextDouble(), DateTime.Now);
        }


        #endregion API




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public OrderGenerator()
        {
            random = new Random();
        }


        #endregion CONSTRUCTION



    }
}
