using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Spectre.Console;


namespace Homework.NET
{
    public partial class Application
    {
        /// <summary>
        /// An abstraction for imposing common logic for different tasks;
        /// <br />
        /// Абстракция для объединения всех заданий общей логикой;
        /// </summary>
        public abstract class Task
        {

            /// <summary>
            /// Имя задания;
            /// </summary>
            protected string _TaskName;


            /// <summary>
            /// Условие задания
            /// </summary>
            protected string _TaskSentense;


            /// <summary>
            /// Свойство имени задания;
            /// </summary>
            public string TaskName
            {
                get { return _TaskName; }
                set { _TaskName = value; }
            }


            /// <summary>
            /// Свойство условия задания;
            /// </summary>
            public string TaskSentense
            {
                get { return _TaskSentense; }
                set { _TaskSentense = value; }
            }


            /// <summary>
            /// Вывод на экран имени и условия задания;
            /// </summary>
            protected void PrintTasknameAndSentence()
            {
                AnsiConsole.Write(new Markup("\n\t[underline]" + TaskName + "[/]\n\n"));
                AnsiConsole.Write(new Markup("\t" + TaskSentense + "\n\n"));
            }
        }
    }

}
