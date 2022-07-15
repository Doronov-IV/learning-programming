using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrickAndTreat.Enumeration
{
    /// <summary>
    /// All the levels of difficulty;
    /// <br />
    /// Все уровни сложности;
    /// </summary>
    public enum GameDifficulty
    {

        Undefined = 0,      // 'null', значение по умолчанию;
        Easy = 1,           // Лёгкий;
        Medium = 2,         // Средний;
        Hard = 3,           // Сложный;
        Insane = 4          // "Мастер";

    }
}
