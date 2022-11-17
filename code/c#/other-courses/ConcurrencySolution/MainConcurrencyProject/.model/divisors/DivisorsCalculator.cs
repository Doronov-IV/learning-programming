using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;
using MainConcurrencyProject.ViewModel;
using System.Globalization;

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
        public static readonly DivisorsValueSet DefaultValueSet = new(cielingNumber: 4e7);


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
        public async Task CalculateDivisorsAsync()
        {
            FillNumbersArrayAsync();

            (long startIndex, long endIndex)[] startEndIndexPairs = new (long startIndex, long endIndex)[_amountOfThreads];

            Thread[] threads = new Thread[_amountOfThreads];
            Task<(long divisorsCounter, long maxDivisorsNumber)>[] tasks = new Task<(long divisorsCounter, long maxDivisorsNumber)>[_amountOfThreads];

            for (int i = 0, iSize = _amountOfThreads; i < iSize; i++)
            {
                startEndIndexPairs[i] = new ((_valueSet.CielingNumber / _amountOfThreads) * i, (_valueSet.CielingNumber / _amountOfThreads) * (i + 1));
            }


            /*
            for (int i = _amountOfThreads-1, iSize = 0; i > iSize; --i)
            {
                for (int j = 0; j < _amountOfThreads - i; j++)
                {
                    startEndIndexPairs[i].startIndex += (_valueSet.CielingNumber / _amountOfThreads) / (_amountOfThreads/2);
                    startEndIndexPairs[i - 1].endIndex += (_valueSet.CielingNumber / _amountOfThreads) / (_amountOfThreads/2);
                }
            }
            */


            
            AsynchronousCalculator.stopwatch = new();
            AsynchronousCalculator.stopwatch.Start();
            for (int i = 0, iSize = _amountOfThreads; i < iSize; i++)
            {
                (long startIndexClosureCopy, long endIndexClosureCopy) currentCopy = startEndIndexPairs[i];

                tasks[i] = ProcessBunchOfNumbersFastAsync (
                        numbersArray: _numbers,
                        startIndex: currentCopy.startIndexClosureCopy,
                        endIndex: currentCopy.endIndexClosureCopy
                        );
            }

            List<(long divisorsCounter, long maxDivisorsNumber)> tasksResult = new();

            foreach (Task<(long divisorsCounter, long maxDivisorsNumber)> task in tasks)
            {
                var result = task.GetAwaiter().GetResult();
                tasksResult.Add(result);
            }

            // getting first fit value from the result list;
            long tempRes = tasksResult.Max(t => t.divisorsCounter);                         // max divisors count;
            var resPair = tasksResult.FirstOrDefault(p => p.divisorsCounter == tempRes);    // the first pair with this value;

            _localNumberValue = resPair.maxDivisorsNumber;
            _localDivisorsValue= resPair.divisorsCounter;

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
        private async Task<(long divisorsCount, long divisorsNumber)> ProcessBunchOfNumbersFastAsync(long[] numbersArray, long startIndex, long endIndex)
        {
            (long maxDivisorsCount, long maxDivisorsNumber) maxDivisorsPair = (0, 0);

            await Task.Run(() =>
            {
                maxDivisorsPair = (0, 0);
                (long nCurrentNumberDivisorsCounter, long currentNumber) currentPair = (0, 0);

                var startIndexCopy = startIndex;
                var endIndexCopy = endIndex;

                if (endIndex > _valueSet.CielingNumber)
                {
                    endIndexCopy = _valueSet.CielingNumber;
                }

                for (long j = startIndexCopy, jSize = endIndexCopy; j < jSize; j++)
                {
                    AsynchronousCalculator.maunalResetHandler.WaitOne();

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
                                currentPair.nCurrentNumberDivisorsCounter += 2;
                            }
                        }
                    }

                    if (maxDivisorsPair.maxDivisorsCount < currentPair.nCurrentNumberDivisorsCounter)
                    {
                        maxDivisorsPair = currentPair;
                    }
                }

            });

                return maxDivisorsPair;
        }



        /// <summary>
        /// Populate the '_numbers' array with long numbers.
        /// <br />
        /// Заполнить массив "_numbers" числами типа long.
        /// </summary>
        private void FillNumbersArrayAsync()
        {
            _numbers = new long[_valueSet.CielingNumber];

            Parallel.For(0, _valueSet.CielingNumber, (i) => 
            {
                _numbers[i++] = i;
            });

            _numbers = _numbers.AsParallel().OrderBy(x => AsynchronousCalculator.random.Next()).ToArray();
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
