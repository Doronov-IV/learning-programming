namespace MainConcurrencyProject.Model.Calculator.PiNumber
{
    /// <summary>
    /// A set of data necessary for calculating pi number using Monte Carlo method.
    /// <br />
    /// Набор данных, необходимых для рассчёта числа pi методом Монте Карло.
    /// </summary>
    public class PiNumberValueSet
    {



        #region STATE




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                             ↓   FIELDS   ↓                               ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 



        /// <inheritdoc cref="CircleRadius"/>
        private long _circleRadius;



        /// <inheritdoc cref="AmountOfPoints"/>
        private long _amountOfPoints;



        /// <inheritdoc cref="PiNumberResultValue"/>
        private double _piNumberResultValue;




        ///////////////////////////////////////////////////////////////////////////////////////
        ///  ↓                           ↓   PROPERTIES   ↓                             ↓   ///
        /////////////////////////////////////////////////////////////////////////////////////// 




        /// <summary>
        /// The size of the field to be tested upon.
        /// <br />
        /// Размер поля, на котором будет проходить вычисление.
        /// </summary>
        public long CircleRadius
        {
            get { return _circleRadius; }
            set
            {
                _amountOfPoints = value;
            }
        }



        /// <summary>
        /// The quantity of the points to be produced.
        /// <br />
        /// Кол-во точек для генерирования.
        /// </summary>
        public long AmountOfPoints
        {
            get { return _amountOfPoints; }
            set
            {
                _amountOfPoints = value;
            }
        }



        /// <summary>
        /// The result of the calculations.
        /// <br />
        /// Результат вычислений.
        /// </summary>
        public double PiNumberResultValue
        {
            get { return _piNumberResultValue; }
            set
            {
                _piNumberResultValue = Math.Round(value, 5);
            }
        }




        #endregion STATE





        #region CONSTRUCTION




        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="amountOfPoints">
        /// The total quantity of generated points.
        /// <br />
        /// Общее кол-во генерируемых точек.
        /// </param>
        /// <param name="circleRadius">
        /// The size of the field to be inserted with points.
        /// <br />
        /// Размеры поля, в которое будут поступать точки.
        /// </param>
        public PiNumberValueSet(long amountOfPoints, long circleRadius)
        {
            AmountOfPoints = amountOfPoints;
            CircleRadius = circleRadius;
            PiNumberResultValue = 0;
        }



        /// <summary>
        /// Parametrized constructor for alternative value forms.
        /// <br />
        /// Конструктор с параметрами для альтернативных форм записи чисел.
        /// </summary>
        /// <param name="amountOfPoints">
        /// The total quantity of generated points.
        /// <br />
        /// Общее кол-во генерируемых точек.
        /// </param>
        /// <param name="circleRadius">
        /// The size of the field to be inserted with points.
        /// <br />
        /// Размеры поля, в которое будут поступать точки.
        /// </param>
        public PiNumberValueSet(double amountOfPoints, double circleRadius)
        {
            AmountOfPoints = (long)amountOfPoints;
            CircleRadius = (long)circleRadius;
            PiNumberResultValue = 0;
        }



        #endregion CONSTRUCTION



    }
}
