using System;
using System.Diagnostics;
using System.Windows.Controls.Primitives;

namespace MainConcurrencyProject.Model.Calculator.PiNumber
{
    public class PiNumberCalculator
    {


        #region STATE



        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                              ↓   FIELDS   ↓                             ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly PiNumberValueSet DefaultValueSet = new(amountOfPoints: 8e7, circleRadius: 2e7);


        /// <inheritdoc cref="ValueSet"/>
        private PiNumberValueSet? valueSet = null;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   PROPERTIES   ↓                           ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// A set of data for the pi number calculation.
        /// <br />
        /// Набор данных для вычисления числа pi.
        /// </summary>
        public PiNumberValueSet ValueSet
        {
            get { return valueSet; }
            set
            {
                ValueSet = value;
            }
        }




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                          ↓   LOCAL VARIABLES   ↓                         ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <summary>
        /// An array of generated points.
        /// <br />
        /// Массив генерируемых точек.
        /// </summary>
        private Point[]? _points = null;

        private double _squareSideLength;             // a;

        private double _piNumber;

        private long _pointInsideCircleCounter;      // Ncirc;




        ///////////////////////////////////////////////////////////////////////////////////////
        /// ↓                             ↓   DELEGATES   ↓                            ↓    ///
        /////////////////////////////////////////////////////////////////////////////////////// 


        /// <inheritdoc cref="AsynchronousCalculator.ProgressbarIncrementValueDelegate"/>
        public delegate void ProgressbarIncrementValueDelegate(long incrementValue);


        /// <inheritdoc cref="AsynchronousCalculator.PassProgressbarValueIncrement"/>
        public event ProgressbarIncrementValueDelegate PassProgressbarValueIncrement;




        #endregion STATE




        #region API



        /// <summary>
        /// Begin async pi number calsulation via Monte Carlo technique.
        /// <br />
        /// Начать асинхронное вычисление числа pi по методу Монте Карло.
        /// </summary>
        public void CalculatePiNumberAsync()
        {
            if (valueSet is null) throw new NullReferenceException("Value set is not specified by the start of the calculation.");

            _piNumber = 0;
            _pointInsideCircleCounter = 0;
            _points = new Point[valueSet.AmountOfPoints];
            _squareSideLength = ValueSet.CircleRadius / 2;

            FillPointArray();

            Thread[] threads = new Thread[AsynchronousCalculator.ThreadCount];

            for (int i = 0; i < AsynchronousCalculator.ThreadCount; i++)
            {
                var closureIteratorCopy = i;
                var closureStatrIndexCopy = (valueSet.AmountOfPoints / AsynchronousCalculator.ThreadCount) * closureIteratorCopy;
                var closureEndIndexCopy = (valueSet.AmountOfPoints / AsynchronousCalculator.ThreadCount) * (closureIteratorCopy + 1);

                threads[i] = new Thread(() =>
                {
                    CheckBunchOfPoints(
                        pointArray: _points,
                        startIndex: closureStatrIndexCopy,
                        endIndex: closureEndIndexCopy
                    );
                });
            }


            AsynchronousCalculator.stopwatch = new();
            AsynchronousCalculator.stopwatch.Start();
            for (int i = 0; i < AsynchronousCalculator.ThreadCount; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < AsynchronousCalculator.ThreadCount; i++)
            {
                threads[i].Join();
            }
            AsynchronousCalculator.stopwatch.Stop();


            _piNumber = (double)_pointInsideCircleCounter * 16.0 / (double)valueSet.AmountOfPoints;
            valueSet.PiNumberResultValue = _piNumber;
            _points = null;
            // once again, just in case;
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true);
        }



        #endregion API




        #region LOGIC



        /// <summary>
        /// Generate and compare some amount of dots equal to '_overallPointCounter' devided by the 'CurrentTaskCount'.
        /// <br />
        /// Сгенерировать и проверить некоторое кол-во точек, равное "_overallPointCounter" разделить на "CurrentTaskCount".
        /// </summary>
        private void CheckBunchOfPoints(Point[] pointArray, long startIndex, long endIndex)
        {
            long currentThreadIncrementResult = 0;

            for (long j = startIndex; j < endIndex; j++)
            {
                AsynchronousCalculator.maunalResetHandler.WaitOne();

                var point = pointArray[j];

                if (point.Y * point.Y <= GetYCoordinateSqr(point.X, (_squareSideLength)))
                    ++currentThreadIncrementResult;

                PassProgressbarValueIncrement?.Invoke(1);
            }

            Interlocked.Add(ref _pointInsideCircleCounter, currentThreadIncrementResult);
        }



        /// <summary>
        /// Fill the array of _points with random generated units.
        /// <br />
        /// Заполнить массив точек случайно генерируемыми значениями.
        /// </summary>
        private void FillPointArray()
        {
            _points = new Point[valueSet.AmountOfPoints];

            ParallelOptions options = new();
            options.MaxDegreeOfParallelism = AsynchronousCalculator.ThreadCount;

            Parallel.For(0,_points.Length, options, (i) =>
            {
                AsynchronousCalculator.maunalResetHandler.WaitOne();

                _points[i++] = new Point(AsynchronousCalculator.random.NextInt64(0, (long)valueSet.CircleRadius), AsynchronousCalculator.random.NextInt64(0, (long)valueSet.CircleRadius));
            });
        }



        /// <summary>
        /// Get a square of an Y coordinate of a point based on it's x-one and a radius of a circle.
        /// <br />
        /// Получить квадрат y-координаты точки, через её x-координату и радиус круга.
        /// </summary>
        private double GetYCoordinateSqr(double xCoordinate, double radius)
        {
            var sqrY = ((radius * radius) - (xCoordinate * xCoordinate));
            return sqrY;
        }



        #endregion LOGIC




        #region CONSTRUCTION



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public PiNumberCalculator()
        {
            valueSet = DefaultValueSet;
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметром.
        /// </summary>
        /// <param name="valueSet">
        /// A custom value set.
        /// <br />
        /// Кастомный набор значений.
        /// </param>
        public PiNumberCalculator(PiNumberValueSet valueSet)
        {
            this.valueSet = valueSet;
        }



        #endregion CONSTRUCTION

    }
}
