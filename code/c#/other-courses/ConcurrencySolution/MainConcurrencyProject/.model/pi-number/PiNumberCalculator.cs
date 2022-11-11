using System;
using System.Diagnostics;

namespace MainConcurrencyProject.Model.Calculator.PiNumber
{
    public class PiNumberCalculator
    {


        #region STATE



        /// <summary>
        /// Default value set.
        /// <br />
        /// Набор значений по умолчанию.
        /// </summary>
        public static readonly PiNumberValueSet DefaultValueSet = new(amountOfPoints: 2048e5, circleRadius: 1024e4);



        /// <inheritdoc cref="ValueSet"/>
        private PiNumberValueSet? _valueSet = null;




        /// <summary>
        /// A set of data for the pi number calculation.
        /// <br />
        /// Набор данных для вычисления числа pi.
        /// </summary>
        public PiNumberValueSet ValueSet
        {
            get { return _valueSet; }
            set
            {
                ValueSet = value;
            }
        }




        private int _amountOfThreads;

        private Random _random;

        private double squareSideLength;             // a;

        private double piNumber;
        private long counter;                        // i;


        private long pointsInsideCircleCounter;      // Ncirc;
        private long overallPointCount;              // Nmax;



        #endregion STATE




        #region API



        /// <summary>
        /// Begin async pi number calsulation via Monte Carlo technique.
        /// <br />
        /// Начать асинхронное вычисление числа pi по методу Монте Карло.
        /// </summary>
        public void CalculatePiNumberAsync()
        {
            if (_valueSet is null) throw new NullReferenceException("Value set is not specified by the start of the calculation.");

            Thread[] threads = new Thread[_amountOfThreads];

            for (int i = 0; i < _amountOfThreads; i++)
            {
                var closureIteratorCopy = i;

                var closureStatrIndexCopy = (_valueSet.AmountOfPoints / _amountOfThreads) * closureIteratorCopy;

                var closureEndIndexCopy = (_valueSet.AmountOfPoints / _amountOfThreads) * (closureIteratorCopy + 1);

                threads[i] = new Thread(() =>
                {
                    GenerateAndCheckPoints(
                        pointArray: points,
                        startIndex: closureStatrIndexCopy,
                        endIndex: closureEndIndexCopy
                    );
                });
            }

            for (int i = 0; i < _amountOfThreads; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < _amountOfThreads; i++)
            {
                threads[i].Join();
            }
        }



        #endregion API




        #region LOGIC



        /// <summary>
        /// Fill the array of points with random generated units.
        /// <br />
        /// Заполнить массив точек случайно генерируемыми значениями.
        /// </summary>
        private void FillPointArray()
        {
            points = new Point[_valueSet.AmountOfPoints];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(random.NextInt64(0, (long)squareSideLength), random.NextInt64(0, (long)squareSideLength));
            }
        }



        #endregion LOGIC




        #region CONSTRUCTION





        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public PiNumberCalculator(int amountOfThreads)
        {
            if (_valueSet is null) _valueSet = DefaultValueSet;
            _amountOfThreads = amountOfThreads;
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
        public PiNumberCalculator(PiNumberValueSet valueSet, int amountOfThreads)
        {
            _valueSet = valueSet;
            _amountOfThreads = amountOfThreads;
        }




        #endregion CONSTRUCTION

    }
}
