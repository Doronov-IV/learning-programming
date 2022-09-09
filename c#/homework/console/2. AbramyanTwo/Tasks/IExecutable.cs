using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.NET
{

    public partial class Application
    {
        /// <summary>
        /// Абстракция для выбора задания;
        /// </summary>
        public interface IExecutable
        {

            /// <summary>
            /// Выполнить;
            /// </summary>
            public void Execute();
        }
    }
}
