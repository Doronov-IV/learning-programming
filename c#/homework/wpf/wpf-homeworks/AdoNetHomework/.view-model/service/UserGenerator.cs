

namespace AdoNetHomework
{
    /// <summary>
    /// User generator;
    /// <br />
    /// Генератор пользователей;
    /// </summary>
    public class UserGenerator
    {
        private List<string> _UserNameList;

        private Random random = new Random();

        public List<string> UserNameList { get { return _UserNameList; }  set { _UserNameList = value; } }


        public User GetUser()
        {
            return new User(UserNameList[random.Next(0, UserNameList.Count)], GetRandomPhoneNumber());
        }


        public static List<string> GetUserNameList(string sFilePath)
        {
            FileReader reader = new FileReader();
            List<string> sResultList = reader.ReadList(sFilePath);
            return sResultList;
        }

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



        public UserGenerator()
        {           
            _UserNameList = new List<string>();
            _UserNameList = GetUserNameList(".data\\User\\UserNames.txt");        
        }

    }
}
