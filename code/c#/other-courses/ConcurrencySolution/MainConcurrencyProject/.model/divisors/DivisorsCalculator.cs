using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;

namespace MainConcurrencyProject.Model.Divisors
{
    public class DivisorsCalculator
    {


        #region STATE



        /// <inheritdoc cref="ValueSet"/>
        private DivisorsValueSet _valueSet;


        /// <inheritdoc cref="AmountOfThreads"/>
        private int _amountOfThreads;


        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly DivisorsValueSet DefaultValueSet = new(cielingNumber: 2e7);




        /// <summary>
        /// The set of required values.
        /// <br />
        /// Набор необходимых значений.
        /// </summary>
        public DivisorsValueSet ValueSet
        {
            get { return _valueSet; }
            set 
            { 
                _valueSet = value; 
            }
        }


        /// <summary>
        /// The amount of threads chosen by user.
        /// <br />
        /// Кол-во потоков, выбранное пользователем.
        /// </summary>
        public int AmountOfThreads
        {
            get { return _amountOfThreads; }
            set 
            {
                _amountOfThreads = value;
            }
        }






        long _localDivisorsValue;

        long _localNumberValue;


        private long[] _numbers;




        #endregion STATE





        #region API



        public void CalculateDivisorsAsync()
        {
            FillNumbersArray();


            Thread[] threads = new Thread[_amountOfThreads];

            for (int i = 0, iSize = _amountOfThreads; i < iSize; i++)
            {
                var closureIteratorCopy = i;
                var closureStartIndexCopy = (_valueSet.CielingNumber / _amountOfThreads) * closureIteratorCopy;
                var closureEndIndexCopy = (_valueSet.CielingNumber / _amountOfThreads) * (closureIteratorCopy + 1);

                threads[i] = new Thread(() =>
                {
                    CheckBunchOfNumbers(
                        numbersArray: _numbers,
                        startIndex: closureStartIndexCopy,
                        endIndex: closureEndIndexCopy
                        );
                });
            }


            AsynchronousCalculator.stopwatch = new();
            AsynchronousCalculator.stopwatch.Start();
            for (int i = 0; i < _amountOfThreads; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < _amountOfThreads; i++)
            {
                threads[i].Join();
            }
            AsynchronousCalculator.stopwatch.Stop();


            _valueSet.ResultNumber = _localResultValue;
            _numbers = null;
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        #endregion API





        #region LOGIC



        private void CheckBunchOfNumbers(long[] numbersArray, long startIndex, long endIndex)
        {
            (long nMaxDivisorsCounter, long maxDivisorsNumber) maxDivisorsPair = (0, 0);
            (long nCurrentNumberDivisorsCounter, long currentNumber) currentPair = (0, 0);

            for (long j = startIndex, jSize = endIndex; j < jSize; j++)
            {
                currentPair = (0, numbersArray[j]);
                for (long i = 1, iSize = numbersArray[j]; i < iSize; i++)
                {
                    if (numbersArray[j] % i == 0)
                    {
                        currentPair.nCurrentNumberDivisorsCounter++;
                    }
                }
                
                if (maxDivisorsPair.nMaxDivisorsCounter < currentPair.nCurrentNumberDivisorsCounter)
                {
                    maxDivisorsPair = currentPair;
                }
            }
            
            if (_localResultValue.divisorsValue < maxDivisorsPair.nMaxDivisorsCounter) _localResultValue = maxDivisorsPair;
        }



        private void FillNumbersArray()
        {
            _numbers = new long[_valueSet.CielingNumber];

            for (long i = 0, iSize = _numbers.Length; i < iSize; i++)
            {
                _numbers[i] = i + 1;
            }
        }



        #endregion LOGIC





        #region CONSTRUCTION



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="amountOfThreads">
        /// The quantity of threads to proceed with.
        /// <br />
        /// Кол-во потоков для работы.
        /// </param>
        public DivisorsCalculator(int amountOfThreads)
        {
            _amountOfThreads = amountOfThreads;
            _valueSet = DefaultValueSet;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="amountOfThreads">
        /// The quantity of threads to proceed with.
        /// <br />
        /// Кол-во потоков для работы.
        /// </param>
        /// <param name="valueSet">
        /// The set of values.
        /// <br />
        /// Набор значений.
        /// </param>
        public DivisorsCalculator(DivisorsValueSet valueSet, int amountOfThreads)
        {
            _amountOfThreads = amountOfThreads;
            _valueSet = valueSet;
        }



        #endregion CONSTRUCTION



    }
}
