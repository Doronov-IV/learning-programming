using MainConcurrencyProject.Model.Calculator;
using MainConcurrencyProject.Model.Calculator.PiNumber;
using MainConcurrencyProject.ViewModel;
using System.Globalization;

namespace MainConcurrencyProject.Model.Divisors
{
    public class DivisorsCalculator
    {


        #region STATE



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   DELEGATES   ↓                            ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <inheritdoc cref="AsynchronousCalculator.ProgressbarIncrementValueDelegate"/>
        public delegate void ProgressbarIncrementValueDelegate(long incrementValue);


        /// <inheritdoc cref="AsynchronousCalculator.PassProgressbarValueIncrement"/>
        public event ProgressbarIncrementValueDelegate PassProgressbarValueIncrement;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                          ↓   LOCAL VARIABLES   ↓                         ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


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




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   FIELDS   ↓                              ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly DivisorsValueSet DefaultValueSet = new(cielingNumber: 2e8);


        /// <inheritdoc cref="ValueSet"/>
        private DivisorsValueSet valueSet;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                            ↓   PROPERTIES   ↓                            ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


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

            (long startIndex, long endIndex)[] startEndIndexPairs = new (long startIndex, long endIndex)[AsynchronousCalculator.ThreadCount];

            Thread[] threads = new Thread[AsynchronousCalculator.ThreadCount];
            Task<(long divisorsCounter, long numberItself)>[] tasks = new Task<(long, long)>[AsynchronousCalculator.ThreadCount];

            for (int i = 0, iSize = AsynchronousCalculator.ThreadCount; i < iSize; i++)
            {
                startEndIndexPairs[i] = new ((valueSet.CielingNumber / AsynchronousCalculator.ThreadCount) * i, (valueSet.CielingNumber / AsynchronousCalculator.ThreadCount) * (i + 1));
            }


            /*
            for (int i = AsynchronousCalculator.ThreadCount-1, iSize = 0; i > iSize; --i)
            {
                for (int j = 0; j < AsynchronousCalculator.ThreadCount - i; j++)
                {
                    startEndIndexPairs[i].startIndex += (valueSet.CielingNumber / AsynchronousCalculator.ThreadCount) / (AsynchronousCalculator.ThreadCount/2);
                    startEndIndexPairs[i - 1].endIndex += (valueSet.CielingNumber / AsynchronousCalculator.ThreadCount) / (AsynchronousCalculator.ThreadCount/2);
                }
            }
            */

            AsynchronousCalculator.stopwatch = new();
            AsynchronousCalculator.stopwatch.Start();
            for (int i = 0, iSize = AsynchronousCalculator.ThreadCount; i < iSize; i++)
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

            ParallelOptions options = new();
            options.MaxDegreeOfParallelism = AsynchronousCalculator.ThreadCount;

            Parallel.For(0, valueSet.CielingNumber, options, (i) => 
            {
                _numbers[i++] = i;
            });

            _numbers = _numbers.AsParallel().OrderBy(x => AsynchronousCalculator.random.Next()).ToArray();
        }



        #endregion LOGIC






        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public DivisorsCalculator()
        {
            valueSet = DefaultValueSet;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="valueSet">
        /// The set of values.
        /// <br />
        /// Набор значений.
        /// </param>
        public DivisorsCalculator(DivisorsValueSet valueSet) : this()
        {
            this.valueSet = valueSet;
        }



        #endregion CONSTRUCTION



    }
}
