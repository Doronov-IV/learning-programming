using System;


namespace Homework.NET
{
    /// <summary>
    /// 'Subject' class;
    /// <br />
    /// Класс 'Предмет';
    /// </summary>
    public class Subject
    {

        #region FIELDS


        /// <summary>
        /// The name of the subject;
        /// <br />
        /// Имя предмета;
        /// </summary>
        private string _name = "Unknown";


        #endregion FIELDS



        #region PROPERTIES


        /// <summary>
        /// Property for '_name' field;
        /// <br />
        /// Свойства поля '_name';
        /// </summary>
        public string Name
        { 
            set { _name = value; }
            get { return _name; } 
        }


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// 'Name' constructor;
        /// <br />
        /// Конструктор с именем;
        /// </summary>
        /// <param name="sName">Имя предмета</param>
        public Subject(string sName)
        {
            Name = sName;
        }


        #endregion CONSTRUCTION



        #region OVERRIDE


        /// <summary>
        /// 'ToString()' method overrdie;
        /// <br />
        /// Перегрузка метода 'ToString()';
        /// </summary>
        /// <returns>Имя предмета</returns>
        public override string ToString()
        {
            return Name;
        }


        #endregion OVERRIDE


    }
}