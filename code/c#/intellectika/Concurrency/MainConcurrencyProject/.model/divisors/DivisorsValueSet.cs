namespace MainConcurrencyProject.Model.Divisors
{
    public class DivisorsValueSet
    {


        #region STATE



        /// <inheritdoc cref="CielingNumber">
        private long _cielingNumber;


        /// <inheritdoc cref="ResultNumber">
        private long _resultNumber;


        /// <inheritdoc cref="ResultNumberDivisorsCount">
        private long _resultNumberDivisorCount;




        /// <summary>
        /// The upper border of the search.
        /// <br />
        /// Верхняя граница поиска.
        /// </summary>
        public long CielingNumber
        {
            get { return _cielingNumber; }
            set
            {
                _cielingNumber = value;
            }
        }


        /// <summary>
        /// The result of the calculations. The number with the most amount of divisors.
        /// <br />
        /// Результат вычислений. Число с наибольшим кол-вом делителей.
        /// </summary>
        public long ResultNumber
        {
            get { return _resultNumber; }
            set
            {
                _resultNumber = value;
            }
        }


        /// <summary>
        /// The amount of the result number divisors.
        /// <br />
        /// Кол-во делителей числа-результата.
        /// </summary>
        public long ResultNumberDivisorsCount
        {
            get { return _resultNumberDivisorCount; }
            set
            {
                _resultNumberDivisorCount = value;
            }
        }



        #endregion STATE




        #region CONSTRUCTION



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="cielingNumber">
        /// The upper border of the search.
        /// <br />
        /// Верхняя граница поиска.
        /// </param>
        public DivisorsValueSet(long cielingNumber)
        {
            _cielingNumber = cielingNumber;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="cielingNumber">
        /// The upper border of the search.
        /// <br />
        /// Верхняя граница поиска.
        /// </param>
        public DivisorsValueSet(double cielingNumber)
        {
            _cielingNumber = (long)cielingNumber;
        }



        #endregion CONSTRUCTION


    }
}
