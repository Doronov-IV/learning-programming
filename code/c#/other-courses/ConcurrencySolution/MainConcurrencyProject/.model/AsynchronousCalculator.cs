using System.Diagnostics;
using MainConcurrencyProject.Model.Calculator.PiNumber;

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




        private PiNumberCalculator _piCalculator;



        public static Stopwatch stopwatch = new();




        private int _threadCount;

        public int ThreadCount
        {
            get => _threadCount;

            set
            {
                _threadCount = value;
            }
        }


        public static Random random = new();


        #endregion STATE





        #region API



        public (double resultNumber, long timeElapsed)  CalculatePiNumber()
        {
            _piCalculator.CalculatePiNumberAsync();

            return (_piCalculator.ValueSet.PiNumberResultValue, stopwatch.ElapsedMilliseconds);
        }



        #endregion API





        #region CONSTRUCTION




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public AsynchronousCalculator(int currentAmountOfThreads)
        {
            _piCalculator = new(amountOfThreads: currentAmountOfThreads);
        }



        #endregion CONSTRUCTION



    }
}
