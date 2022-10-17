using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizationDictionary.Service
{
    /// <summary>
    /// A wrapper over 'OrderBy' enum;
    /// <br />
    /// Обёртка над перечислением 'OrderBy';
    /// </summary>
    public partial class ViewProperties
    {


        #region PROPERTIES - Values that form the State of this class


        /// <summary>
        /// The order of words;
        /// <br />
        /// Порядок слов;
        /// </summary>
        public OrderBy Order { set; get; }


        #endregion PROPERTIES



        #region CONSTRUCTION - Fundamental behavior of an object


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static ViewProperties instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private ViewProperties()
        {
            Order = OrderBy.Unsorted;
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static ViewProperties GetInstance()
        {
            if (instance == null)
            {
                instance = new ViewProperties();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
