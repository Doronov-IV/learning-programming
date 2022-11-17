using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;
using MainConcurrencyProject.ViewModel;
using System.Globalization;

namespace MainConcurrencyProject.Model.Divisors
{
    public class DivisorsCalculator
    {


        #region STATE



        //



        /// <summary>
        /// Raise progress bar value by the specific value.
        /// <br />
        /// Увеличить значение програссбара на определенное значение.
        /// </summary>
        /// <param name="incrementValue">
        /// The specific value to add to the pb's one.
        /// <br />
        /// Конкретное значение, чтобы добавить к показателю прогрессбара.
        /// </param>
        public delegate void ProgressbarIncrementValueDelegate(long incrementValue);


        /// <summary>
        /// Add specific value to the progressbar's one.
        /// <br />
        /// Добавить определённое значение к значению прогрессбара.
        /// </summary>
        public event ProgressbarIncrementValueDelegate PassProgressbarValueIncrement;



        //



        /// <summary>
        /// Maximum divisors value.
        /// <br />
        /// Максимальный показатель делителей.
        /// </summary>
        private long _localDivisorsValue;


        /// <summary>
        /// The Number of the maximum divisors value.
        /// <br />
        /// Число для максимального показателя делителей.
        /// </summary>
        private long _localNumberValue;


        /// <summary>
        /// An array of numbers from 1 to 'cieling'.
        /// <br />
        /// Массив чисел от 1 до "cieling".
        /// </summary>
        private long[]? _numbers;



        //



        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly DivisorsValueSet DefaultValueSet = new(cielingNumber: 2e8);


        /// <inheritdoc cref="ValueSet"/>
        private DivisorsValueSet valueSet;


        /// <inheritdoc cref="AmountOfThreads"/>
        private int amountOfThreads;


        
        //



        /// <summary>
        /// The set of required values.
        /// <br />
        /// Набор необходимых значений.
        /// </summary>
        public DivisorsValueSet ValueSet
        {
            get { return valueSet; }
            set 
            { 
                valueSet = value; 
            }
        }


        /// <summary>
        /// The amount of threads chosen by user.
        /// <br />
        /// Кол-во потоков, выбранное пользователем.
        /// </summary>
        public int AmountOfThreads
        {
            get { return amountOfThreads; }
            set 
            {
                amountOfThreads = value;
            }
        }



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

            (long startIndex, long endIndex)[] startEndIndexPairs = new (long startIndex, long endIndex)[amountOfThreads];

            Thread[] threads = new Thread[amountOfThreads];
            Task<(long divisorsCounter, long numberItself)>[] tasks = new Task<(long, long)>[amountOfThreads];

            for (int i = 0, iSize = amountOfThreads; i < iSize; i++)
            {
                startEndIndexPairs[i] = new ((valueSet.CielingNumber / amountOfThreads) * i, (valueSet.CielingNumber / amountOfThreads) * (i + 1));
            }


            /*
            for (int i = amountOfThreads-1, iSize = 0; i > iSize; --i)
            {
                for (int j = 0; j < amountOfThreads - i; j++)
                {
                    startEndIndexPairs[i].startIndex += (valueSet.CielingNumber / amountOfThreads) / (amountOfThreads/2);
                    startEndIndexPairs[i - 1].endIndex += (valueSet.CielingNumber / amountOfThreads) / (amountOfThreads/2);
                }
            }
            */
            
            AsynchronousCalculator.stopwatch = new();
            AsynchronousCalculator.stopwatch.Start();
            for (int i = 0, iSize = amountOfThreads; i < iSize; i++)
            {
                (long startIndexClosureCopy, long endIndexClosureCopy) currentCopy = startEndIndexPairs[i];

                tasks[i] = ProcessBunchOfNumbersFastAsync (
                        numbersArray: _numbers,
                        startIndex: currentCopy.startIndexClosureCopy,
                        endIndex: currentCopy.endIndexClosureCopy
                        );
            }

            List<(long divisorsCounter, long numberItself)> tasksResult = new();

            foreach (Task<(long, long)> task in tasks)
            {
                var result = task.GetAwaiter().GetResult();
                tasksResult.Add(result);
            }


            var resPair = tasksResult.FirstOrDefault(p => p.divisorsCounter == tasksResult.Max(t => t.divisorsCounter));    // the first pair with the max divisors count;

            _localNumberValue = resPair.numberItself;
            _localDivisorsValue= resPair.divisorsCounter;

            AsynchronousCalculator.stopwatch.Stop();
            


            valueSet.ResultNumber = _localNumberValue;
            valueSet.ResultNumberDivisorsCount = _localDivisorsValue;
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
            var startIndexCopy = startIndex;
            var endIndexCopy = endIndex;

            await Task.Run(() =>
            {
                maxDivisorsPair = (0, 0);
                (long nCurrentNumberDivisorsCounter, long currentNumber) currentPair = (0, 0);

                if (endIndex > valueSet.CielingNumber)
                {
                    endIndexCopy = valueSet.CielingNumber;
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

                    PassProgressbarValueIncrement?.Invoke(1);

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
            _numbers = new long[valueSet.CielingNumber];

            Parallel.For(0, valueSet.CielingNumber, (i) => 
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
            this.amountOfThreads = amountOfThreads;
            valueSet = DefaultValueSet;
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
        public DivisorsCalculator(DivisorsValueSet valueSet, int amountOfThreads) : this(amountOfThreads)
        {
            this.valueSet = valueSet;
        }



        #endregion CONSTRUCTION



    }
}
