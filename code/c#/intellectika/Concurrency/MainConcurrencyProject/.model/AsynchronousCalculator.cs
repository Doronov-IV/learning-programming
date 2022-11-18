using System.Diagnostics;
using MainConcurrencyProject.Model.Calculator.PiNumber;
using MainConcurrencyProject.Model.Divisors;

namespace MainConcurrencyProject.Model.Calculator
{
	/// <summary>
	/// A tool which calculates using concurrency.
	/// <br />
	/// Инструмент, который проводит вычисления, используя многопоточность.
	/// </summary>
	public partial class AsynchronousCalculator
    {



        #region STATE




        /// <summary>
        /// Raise progress bar value by the specific one.
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






        /// <summary>
        /// An instance of a calculator, looking for the number with most amount of divisors in specific span.
        /// <br />
        /// Экземпляр калькулятора, который занимается поиском числа с наибольшим кол-вом делителей в заданном промежутке.
        /// </summary>
        private DivisorsCalculator _divisorsCalculator;



        /// <summary>
        /// An instance of a pi calculator.
        /// <br />
        /// Экземпляр вычислителя числа Pi.
        /// </summary>
        private PiNumberCalculator _piCalculator;



        /// <summary>
        /// A timer to get the idea of time, elapsed with processing.
        /// <br />
        /// Секундомер, чтобы иметь представлеине о затраченном времени.
        /// </summary>
        public static Stopwatch stopwatch = new();



        /// <inheritdoc cref="ThreadCount"/>
        private static int threadCount;



        /// <summary>
        /// Amout of threads user chooses to proceed with.
        /// <br />
        /// Кол-во потоков, которое выбирает пользователь.
        /// </summary>
        public static int ThreadCount
        {
            get => threadCount;

            set
            {
                threadCount = value;
            }
        }



        /// <summary>
        /// An instance of the System.Random class.
        /// <br />
        /// Экземпляр класса "System.Random".
        /// </summary>
        public static Random random = new();



        /// <summary>
        /// A manual reset event instance for lockers demo.
        /// <br />
        /// Экземпляр ManualResetEvent для демки локеров.
        /// </summary>
        public static ManualResetEvent maunalResetHandler = new(initialState: true);



        #endregion STATE





        #region API



        /// <summary>
        /// Calculate the pi number.
        /// <br />
        /// Вычислить число "Pi".
        /// </summary>
        /// <returns>
        /// A tuple of: resultNumber - calculation result (Pi number), timeElapsed - elapsed time in mls.
        /// <br />
        /// Кортеж из: "resultNumber" - результат вычисления (число Pi), "timeElapsed" - затраченное время в милисекундах.
        /// </returns>
        public (double resultNumber, double timeElapsed)  CalculatePiNumber()
        {
            _piCalculator.CalculatePiNumberAsync();

            return (_piCalculator.ValueSet.PiNumberResultValue, stopwatch.Elapsed.TotalSeconds);
        }



        /// <summary>
        /// Calculate divisors.
        /// <br />
        /// Вычислить делители.
        /// </summary>
        /// <returns>
        /// A tuple of two values; divisorsAmount - the quantity of divisors found, resultNumber - the number for which the amount of divisors was found, time elapsed - time spent for calculations.
        /// <br />
        /// Кортеж из двух значений; "divisorsAmount" - кол-во найденых делителей, "resultNumber" - число, для которого нашли делители, "timeElapsed" - время, затраченное на вычисления.
        /// </returns>
        public (long divisorsAmount, long resultNumber, double timeElapsed) CalculateDivisors()
        {
            _divisorsCalculator.CalculateDivisorsAsync();

            return (_divisorsCalculator.ValueSet.ResultNumberDivisorsCount, _divisorsCalculator.ValueSet.ResultNumber, stopwatch.Elapsed.TotalSeconds);
        }



        #endregion API




        #region LOGIC



        private void OnProgressbarValuePassed(long value)
        {
            PassProgressbarValueIncrement?.Invoke(value);
        }



        #endregion LOGIC





        #region CONSTRUCTION



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="currentAmountOfThreads">
        /// The amount of threads to proceed with.
        /// <br />
        /// Кол-во потоков для вычислений.
        /// </param>
        public AsynchronousCalculator(int currentAmountOfThreads)
        {
            ThreadCount= currentAmountOfThreads;
            _piCalculator = new();
            _divisorsCalculator = new();
            _divisorsCalculator.PassProgressbarValueIncrement += OnProgressbarValuePassed;
            _piCalculator.PassProgressbarValueIncrement += OnProgressbarValuePassed;
        }



        #endregion CONSTRUCTION



    }
}
