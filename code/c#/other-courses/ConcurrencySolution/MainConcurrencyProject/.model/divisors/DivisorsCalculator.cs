using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;

namespace MainConcurrencyProject.Model.Divisors
{
    public class DivisorsCalculator
    {


        #region STATE


        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly DivisorsValueSet DefaultValueSet = new(cielingNumber: 2e8);


        /// <inheritdoc cref="ValueSet"/>
        private DivisorsValueSet _valueSet;


        /// <inheritdoc cref="AmountOfThreads"/>
        private int _amountOfThreads;





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






        /// <summary>
        /// Maximum divisors value.
        /// <br />
        /// Максимальный показатель делителей.
        /// </summary>
        long _localDivisorsValue;


        /// <summary>
        /// The Number of the maximum divisors value.
        /// <br />
        /// Число для максимального показателя делителей.
        /// </summary>
        long _localNumberValue;


        /// <summary>
        /// An array of numbers from 1 to 'cieling'.
        /// <br />
        /// Массив чисел от 1 до "cieling".
        /// </summary>
        private long[]? _numbers;




        #endregion STATE





        #region API



        /// <summary>
        /// Calculate the number with the most amount of divisors.
        /// <br />
        /// Рассчитать число с максимальным числом делителей.
        /// </summary>
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
                    ProcessBunchOfNumbersFast(
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


            _valueSet.ResultNumber = _localNumberValue;
            _valueSet.ResultNumberDivisorsCount = _localDivisorsValue;
            _numbers = null;
            // just in case;
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        #endregion API





        #region LOGIC



        /// <summary>
        /// Process a portion of the numbers array.
        /// <br />
        /// Обработать часть массива чисел.
        /// </summary>
        /// <param name="numbersArray">
        /// The local array of numbers.
        /// <br />
        /// Локальный массив чисел.
        /// </param>
        /// <param name="startIndex">
        /// Starting index.
        /// <br />
        /// Начальный индекс.
        /// </param>
        /// <param name="endIndex">
        /// Ending index.
        /// <br />
        /// Конечный индекс.
        /// </param>
        private void ProcessBunchOfNumbersSlow(long[] numbersArray, long startIndex, long endIndex)
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

            if (_localDivisorsValue < maxDivisorsPair.nMaxDivisorsCounter)
            {
                _localNumberValue = maxDivisorsPair.maxDivisorsNumber;
                _localDivisorsValue = maxDivisorsPair.nMaxDivisorsCounter;
            }
        }



        /// <summary>
        /// Process a portion of the numbers array.
        /// <br />
        /// Обработать часть массива чисел.
        /// </summary>
        /// <param name="numbersArray">
        /// The local array of numbers.
        /// <br />
        /// Локальный массив чисел.
        /// </param>
        /// <param name="startIndex">
        /// Starting index.
        /// <br />
        /// Начальный индекс.
        /// </param>
        /// <param name="endIndex">
        /// Ending index.
        /// <br />
        /// Конечный индекс.
        /// </param>
        private void ProcessBunchOfNumbersFast(long[] numbersArray, long startIndex, long endIndex)
        {
            (long nMaxDivisorsCounter, long maxDivisorsNumber) maxDivisorsPair = (0, 0);
            (long nCurrentNumberDivisorsCounter, long currentNumber) currentPair = (0, 0);

            for (long j = startIndex, jSize = endIndex; j < jSize; j++)
            {
                currentPair = (0, numbersArray[j]);
                for (long i = 1, iSize = (long)Math.Sqrt(numbersArray[j]); i < iSize; i++)
                {
                    if (numbersArray[j] % i == 0)
                    {
                        if (numbersArray[j] % i == i)
                        {
                            currentPair.nCurrentNumberDivisorsCounter++;
                        }
                        else
                        {
                            currentPair.nCurrentNumberDivisorsCounter+=2;
                        }
                    }
                }

                if (maxDivisorsPair.nMaxDivisorsCounter < currentPair.nCurrentNumberDivisorsCounter)
                {
                    maxDivisorsPair = currentPair;
                }
            }

            if (_localDivisorsValue < maxDivisorsPair.nMaxDivisorsCounter)
            {
                _localNumberValue = maxDivisorsPair.maxDivisorsNumber;
                _localDivisorsValue = maxDivisorsPair.nMaxDivisorsCounter;
            }
        }



        /// <summary>
        /// Populate the '_numbers' array with long numbers.
        /// <br />
        /// Заполнить массив "_numbers" числами типа long.
        /// </summary>
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
