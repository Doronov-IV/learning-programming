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
        private int _threadCount;


        /// <summary>
        /// Amout of threads user chooses to proceed with.
        /// <br />
        /// Кол-во потоков, которое выбирает пользователь.
        /// </summary>
        public int ThreadCount
        {
            get => _threadCount;

            set
            {
                _threadCount = value;
            }
        }


        /// <summary>
        /// An instance of the System.Random class.
        /// <br />
        /// Экземпляр класса "System.Random".
        /// </summary>
        public static Random random = new();


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
        public (double resultNumber, long timeElapsed)  CalculatePiNumber()
        {
            _piCalculator.CalculatePiNumberAsync();

            return (_piCalculator.ValueSet.PiNumberResultValue, stopwatch.ElapsedMilliseconds);
        }



        public ((long divisorsAmount, long resultNumber) pair, long timeElapsed) CalculateDivisors()
        {
            _divisorsCalculator.CalculateDivisorsAsync();

            return ((_divisorsCalculator.ValueSet.ResultNumber.divisorsAmount, _divisorsCalculator.ValueSet.ResultNumber.number), stopwatch.ElapsedMilliseconds);
        }



        #endregion API





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
            _piCalculator = new(amountOfThreads: currentAmountOfThreads);
            _divisorsCalculator = new(amountOfThreads: currentAmountOfThreads);
        }



        #endregion CONSTRUCTION



    }
}
