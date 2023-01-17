using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// A list of predefined standart information for the type 'string'.
    /// <br />
    /// Список предопределённой стандартизированной информации для типа "string".
    /// </summary>
    public static class Asset
    {


        /// <summary>
        /// The application default date format.
        /// <br />
        /// Дефолтный формат даты в приложении.
        /// </summary>
        public static readonly string DateFormat = DateTime.Now.ToString("dd.MM.yyyy");


        /// <summary>
        /// The application default time format (down to seconds).
        /// <br />
        /// Дефолтный формат времени в приложении (с точностью до секунды).
        /// </summary>
        public static readonly string TimeSecondFormat = DateTime.Now.ToString("HH:mm:ss");


        /// <summary>
        /// The application default time format (down to milliseconds).
        /// <br />
        /// Дефолтный формат времени в приложении (с точностью до милисекунды).
        /// </summary>
        public static readonly string TimeMillisecondFormat = DateTime.Now.ToString("HH:mm:ss:fff");


        /// <summary>
        /// Represents a mock string for the cases when the information was not specified.
        /// <br />
        /// Представляет собой строку-затычку для случаев, когда информация не была задана явно.
        /// </summary>
        public static readonly string NonAccessable = "n/a";


    }
}
