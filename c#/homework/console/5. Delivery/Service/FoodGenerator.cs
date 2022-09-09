using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Delivery.BaseClasses;

using Tools.FileReader;


namespace Delivery.Service
{
    public class FoodGenerator
    {


        #region PROPERTIES


        /// <summary>
        /// A list of all courses;
        /// <br />
        /// Список всех возможных блюд;
        /// </summary>
        List<Course> CoursesMenuList { get; set; }


        /// <summary>
        /// A list of all drinks;
        /// <br />
        /// Список всех возможных напитков;
        /// </summary>
        List<Drink> DrinksMenuList { get; set; }


        #endregion PROPERTIES



        #region CONSTRUCTION


        /// <summary>
        /// An instance of the class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static FoodGenerator instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private FoodGenerator()
        {
            // <read courses>

            CoursesMenuList = new List<Course>();

            // <read courses />


            // <read drinks>

            DrinksMenuList = new List<Drink>();

            // <read drinks />
        }


        /// <summary>
        /// Get the instance;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>A pointer to the instance;<br />Указатель на экземпляр;</returns>
        public static FoodGenerator GetInstance()
        {
            if (instance == null)
            {
                instance = new FoodGenerator();
            }
            return instance;
        }


        #endregion CONSTRUCTION



        #region LOGIC


        /// <summary>
        /// Get a pair of random course and drink;
        /// <br />
        /// Получить пару из случайного блюда и напитка;
        /// </summary>
        /// <returns>A tuple of random course and drink;<br />Кортеж(?) из случайного блюда и напитка;</returns>
        public (Course Course, Drink Drink) GetFood()
        {
            // Read menues in case we haven't done it yet;
            if (CoursesMenuList.Count == 0 || DrinksMenuList.Count == 0) ReadMenuFiles();

            // Pick some values from lists;
            return (CoursesMenuList[new Random().Next() % CoursesMenuList.Count], DrinksMenuList[new Random().Next() % DrinksMenuList.Count]);
        }


        /// <summary>
        /// Read menu files;
        /// <br />
        /// Прочитать файлы меню;
        /// </summary>
        private void ReadMenuFiles()
        {
            // <read courses>

            FileReader<List<Course>> readerCourse = new FileReader<List<Course>>();

            CoursesMenuList = readerCourse.Read("JSON\\Data\\Courses\\CourseMenu.json");

            // <read courses />


            // <read drinks>

            FileReader<List<Drink>> readerDrinks = new FileReader<List<Drink>>();

            DrinksMenuList = readerDrinks.Read("JSON\\Data\\Drinks\\DrinksMenu.json");

            // <read drinks />
        }


        #endregion LOGIC


    }
}
