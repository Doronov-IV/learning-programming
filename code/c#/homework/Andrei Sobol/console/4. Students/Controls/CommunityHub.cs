using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


namespace Homework.NET
{
    /// <summary>
    /// A space for printing groups info into console;
    /// <br />
    /// Класс для вывода данных о группах в консоль;
    /// </summary>
    public class CommunityHub
    {


        #region FIELDS


        /// <summary>
        /// An instance of a group;
        /// <br />
        /// Экземпляр группы;
        /// </summary>
        private Group demo_group;


        #endregion FIELDS



        #region PRINTING


        /// <summary>
        /// Print all grades of all students;
        /// <br />
        /// Вывести все оценки всех учеников;
        /// </summary>
        public void PrintAllStudentsGrades()
        {
            // A counter for extra tab if quantity of grades is too low;
            int nTabCounter;

            PrintStudentsList();

            // Start printing subjects and grades;
            AnsiConsole.Write(new Markup("\n"));
            foreach (Subject subj in demo_group.Subjects)
            {
                AnsiConsole.Write(new Markup("\n\t"));
                AnsiConsole.Write(new Markup("[underline]" + subj.Name + "[/]"));

                // Subject name length validation;
                if (subj.Name.Length >= 10) AnsiConsole.Write(new Markup("\t"));            // 'Computer Science';
                else if (subj.Name.Length == 9) AnsiConsole.Write(new Markup("\t\t"));      // 'Chemistry';
                else if (subj.Name.Length >= 5) AnsiConsole.Write(new Markup("\t\t\t"));    // Others;
                else AnsiConsole.Write(new Markup("\t\t\t"));

                foreach (Student stud in demo_group.Students)
                {
                    nTabCounter = 0;
                    foreach (Grade grd in stud.GradesList)
                    {
                        if (grd.Subject == subj) 
                        {
                            AnsiConsole.Write(new Markup(grd.Value + " "));
                            nTabCounter += 1;
                        }
                    }
                    if (nTabCounter <= 2) AnsiConsole.Write(new Markup("\t"));
                    AnsiConsole.Write(new Markup("\t"));
                }
            }

        }


        /// <summary>
        /// Print all students' average grades;
        /// <br />
        /// Вывести средние оценки студентов;
        /// </summary>
        public void PrintEachStudentAvg()
        {
            PrintStudentsList();

            // Start printing subjects and grades;
            AnsiConsole.Write(new Markup("\n\n\t\t"));
            foreach (Student stud in demo_group.Students)
            {
                AnsiConsole.Write(new Markup("\t\t"));
                AnsiConsole.Write(new Markup("" + stud.GetAverageGrade().Value));
            }
        }


        /// <summary>
        /// Print average grades of students for all subjects;
        /// <br />
        /// Вывести средние оценки студентов по всем предметам;
        /// </summary>
        public void PrintEachStudentAvgForSubjects()
        {
            int nSubjectsCounter = 0;

            PrintStudentsList();

            // Start printing subjects and grades;
            AnsiConsole.Write(new Markup("\n"));
            foreach (Subject subj in demo_group.Subjects)
            {
                AnsiConsole.Write(new Markup("\n\t"));
                AnsiConsole.Write(new Markup("[underline]" + subj.Name + "[/]"));

                // Subject name length validation;
                if (subj.Name.Length >= 10) AnsiConsole.Write(new Markup("\t"));            // 'Computer Science';
                else if (subj.Name.Length == 9) AnsiConsole.Write(new Markup("\t\t"));      // 'Chemistry';
                else if (subj.Name.Length >= 5) AnsiConsole.Write(new Markup("\t\t\t"));    // Others;
                else AnsiConsole.Write(new Markup("\t\t\t"));

                foreach (Student stud in demo_group.Students)
                {
                    AnsiConsole.Write(new Markup("" + stud.GetAverageGradesForAllSubjects()[nSubjectsCounter]));
                    AnsiConsole.Write(new Markup("\t\t"));
                }

                nSubjectsCounter += 1;
            }
        }


        /// <summary>
        /// Print group avg grade;
        /// <br />
        /// Вывести средний балл группы;
        /// </summary>
        public void PrintGroupsAvg()
        {
            AnsiConsole.Write(new Markup("\t\t[underline]" + "Group AVG = " + demo_group.GetAverageGroupGrade().Value + "[/]\n"));
        }


        /// <summary>
        /// Print all min and max grades for all subjects;
        /// <br />
        /// Вывести минимальный и максимальный балл по всем предметам;
        /// </summary>
        public void PrintMinMax()
        {
            AnsiConsole.Write(new Markup("\n"));

            int nSubjectsCounter = 0;

            foreach (Subject subj in demo_group.Subjects)
            {
                AnsiConsole.Write(new Markup("\n\t"));
                AnsiConsole.Write(new Markup("[underline]" + subj.Name + "[/]"));

                // Subject name length validation;
                if (subj.Name.Length >= 10) AnsiConsole.Write(new Markup("\t"));            // 'Computer Science';
                else if (subj.Name.Length == 9) AnsiConsole.Write(new Markup("\t\t"));      // 'Chemistry';
                else if (subj.Name.Length >= 5) AnsiConsole.Write(new Markup("\t\t\t"));    // Others;
                else AnsiConsole.Write(new Markup("\t\t\t"));

                AnsiConsole.Write(new Markup("[green]Max: " + demo_group.GetMinMaxGradesForAllSubjects().Highest[nSubjectsCounter].FirstName +
                        " " + demo_group.GetMinMaxGradesForAllSubjects().Highest[nSubjectsCounter].LastName
                        + " = " + demo_group.GetMinMaxGradesForAllSubjects().Highest[nSubjectsCounter].GetAverageGradesForAllSubjects()[nSubjectsCounter] + "[/]"));
                AnsiConsole.Write(new Markup("\t\t"));
                AnsiConsole.Write(new Markup("[red]Min: " + demo_group.GetMinMaxGradesForAllSubjects().Lowest[nSubjectsCounter].FirstName +
                    " " + demo_group.GetMinMaxGradesForAllSubjects().Lowest[nSubjectsCounter].LastName
                    + " = " + demo_group.GetMinMaxGradesForAllSubjects().Lowest[nSubjectsCounter].GetAverageGradesForAllSubjects()[nSubjectsCounter] + "[/]"));
                AnsiConsole.Write(new Markup("\n"));

                nSubjectsCounter += 1;
            }
        }


        #endregion PRINTING



        #region AUXILIARY


        /// <summary>
        /// Print the names of the students for table;
        /// <br />
        /// Вывести имена студентов для таблицы;
        /// </summary>
        private void PrintStudentsList()
        {
            // Start printing students' names;
            AnsiConsole.Write(new Markup("\t\t\t\t"));
            for (int i = 0, nGroupSize = demo_group.Students.Count; i < nGroupSize; ++i)
            {
                AnsiConsole.Write(new Markup(demo_group.Students[i].FirstName + " " + demo_group.Students[i].LastName + "\t"));
            }
        }


        #endregion AUXILIARY



        #region CONSTRUCTION

        /// <summary>
        /// The instance of a class;
        /// <br />
        /// Экземпляр класса;
        /// </summary>
        private static CommunityHub instance;


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        private CommunityHub()
        {
            var helper = GroupGenerator.GetInstance();
            demo_group = helper.GenerateNewGroup();
        }


        /// <summary>
        /// Get instance of a class;
        /// <br />
        /// Получить экземпляра класса;
        /// </summary>
        /// <returns>
        /// A pointer to the instance;
        /// <br />
        /// Указатель на экземпляр;
        /// </returns>
        public static CommunityHub GetInstance()
        {
            if (instance == null)
            {
                instance = new CommunityHub();
            }
            return instance;
        }


        #endregion CONSTRUCTION


    }
}
