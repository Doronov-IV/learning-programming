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



        private static Stopwatch _stopwatch;




        private int _threadCount;

        public int ThreadCount
        {
            get => _threadCount;

            set
            {
                _threadCount = value;
            }
        }




        #endregion STATE





        #region API







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
            _stopwatch = new();
        }



        #endregion CONSTRUCTION



    }
}
