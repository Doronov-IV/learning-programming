using System;


namespace Homework.NET
{

    /// <summary>
    /// An entity that carries all the grade data;
    /// <br />
    /// Сущность, которая хранит все данные об оценке;
    /// </summary>
    public class Grade
    { 


        #region FIELDS


        /// <summary>
        /// The value of a grade (in %);
        /// <br />
        /// Значение оценки (в %);
        /// </summary>
        private int? _nValue = default(int?);


        /// <summary>
        /// Date of creation;
        /// <br />
        /// Дата создания;
        /// </summary>
        private DateTime? _date = default(DateTime?);


        /// <summary>
        /// An assigned subject;
        /// <br />
        /// Оцениваемый предмет;
        /// </summary>
        private Subject? _subject = default(Subject?);


        /// <summary>
        /// A comment (optional);
        /// <br />
        /// Комментарий (опционально);
        /// </summary>
        private string? _comment = default(string?);


        /// <summary>
        /// A pointer to a Student;
        /// <br />
        /// Указатель на ученика;
        /// </summary>
        private Student? _student = default(Student?);


        /// <summary>
        /// A constant value which represents maximum possible grade value;
        /// <br />
        /// Константное значение, представляющее собой максимально возможное значение для оценки;
        /// </summary>
        public static readonly int MAX_VALUE = 100;


        /// <summary>
        /// A constant value which represents minimum possible grade value;
        /// <br />
        /// Константное значение, представляющее собой минимально возможное значение для оценки;
        /// </summary>
        public static readonly int MIN_VALUE = 0;


        #endregion FIELDS



        #region PROPERTIES


        /// <summary>
        /// Property for '_nValue' field;
        /// <br />
        /// Свойства поля '_nValue';
        /// </summary>
        public int? Value
        { 
            get { return _nValue; } 
            set 
            { 
                if (0 <= _nValue && _nValue <= 100) _nValue = value;
                else throw new ArgumentOutOfRangeException();
            }
        }


        /// <summary>
        /// Property for '_date' field;
        /// <br />
        /// Свойства поля '_date';
        /// </summary>
        public DateTime? Date
        {
            get { return _date; }
        }


        /// <summary>
        /// Property for '_subject' field;
        /// <br />
        /// Свойства поля '_subject';
        /// </summary>
        public Subject? Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }


        /// <summary>
        /// Property for '_student' field;
        /// <br />
        /// Свойства поля '_student';
        /// </summary>
        public Student? Student
        {
            get { return _student; }
            set { _student = value; }
        }



        /// <summary>
        /// Property for '_comment' field;
        /// <br />
        /// Свойства поля '_comment';
        /// </summary>
        public string? Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }


        #endregion PROPERTIES



        #region CONSTRUCTION


        public Grade(Subject subject, int nValue)
        {
            _date = new DateTime();
            _date = DateTime.Now;

            _subject = subject;
            _nValue = nValue;
        }


        #endregion CONSTRUCTION


    }

}