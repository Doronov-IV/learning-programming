using AdoNetHomework.Model;

namespace AdoNetHomework.Service
{

    /// <summary>
    /// User generator;
    /// <br />
    /// Генератор пользователей;
    /// </summary>
    public class UserGenerator
    {


        #region PROPERTIES - forming the State of an Object


        /// <summary>
        /// A list of users' names;
        /// <br />
        /// Список имён пользователей;
        /// </summary>
        private List<string> UserNameList;


        /// <summary>
        /// A reference of 'System.Random' instance;
        /// <br />
        /// Ссылка на копию класса "System.Random";
        /// </summary>
        private Random random;


        #endregion PROPERTIES - forming the State of an Object




        #region API - public Contract Methods


        /// <summary>
        /// Get reference to a new random user for table 'Users';
        /// <br />
        /// Получить ссылку на нового случайного пользователя для таблицы "Users";
        /// </summary>
        /// <returns>
        /// A reference to a fresh user;
        /// <br />
        /// Ссылка на нового пользователя;
        /// </returns>
        public User GetRandomUser()
        {
            return new User(UserNameList[random.Next(0, UserNameList.Count)], GetRandomPhoneNumber());
        }


        /// <summary>
        /// Get random phone number via 'System.Random' for 'Users' table;
        /// <br />
        /// Получить случайный номер телефона через "System.Random" для таблицы "Users";
        /// </summary>
        /// <returns>
        /// Long integer number via 'ToString';
        /// <br />
        /// Большое целое число через "ToString";
        /// </returns>
        public string GetRandomPhoneNumber()
        {
            string sRes = "";

            string sPrefix = "+44";

            long minValue = 1000000000;

            long maxValue = 9999999999;

            Int64 lNumber = random.NextInt64(minValue, maxValue);

            sRes = sPrefix + lNumber.ToString();

            return sRes;
        }


        #endregion API - public Contract Methods





        #region LOGIC - private interior Methods


        /// <summary>
        /// Get list of users' names for generating new users for 'Users' table;
        /// <br />
        /// Получить список имён пользователь для их генерации в таблицу "Users";
        /// </summary>
        /// <param name="sFilePath">
        /// A relative path to a text file;
        /// <br />
        /// Относительный путь к текстовому файлу;
        /// </param>
        /// <returns>
        /// List of names;
        /// <br />
        /// Список имён;
        /// </returns>
        private static List<string> GetUserNameList(string sFilePath)
        {
            FileReader reader = new FileReader();
            List<string> sResultList = reader.ReadList(sFilePath);
            return sResultList;
        }


        #endregion LOGIC - private interior Methods




        #region CONSTRUCTION - Object Lifetime Control


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчаню;
        /// </summary>
        public UserGenerator()
        {
            random = new Random();
            UserNameList = new List<string>();
            UserNameList = GetUserNameList(".data\\User\\UserNames.txt");
        }


        #endregion CONSTRUCTION - Object Lifetime Control


    }
}
