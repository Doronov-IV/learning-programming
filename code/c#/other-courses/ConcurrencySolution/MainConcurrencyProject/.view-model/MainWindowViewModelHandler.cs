using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Controls;

namespace MainConcurrencyProject.ViewModel
{
    /// <summary>
    /// Tier 1 - handlers, tier 2 - various secondary methods, tier 3 - auxiliary methods, tier 4 - dispatches, tier 5 - generic i/o.
    /// <br />
    /// Tier 1 - обработчики, tier 2 - различные второстепенные методы, tier 3 - вспомогательные методы, tier 4 - диспатчи и tier 5 - генерализованный ввод/вывод.
    /// </summary>
    public partial class MainWindowViewModel
    {

        // handle controls events

        #region HANDLERS - Tier 1



        /// <summary>
        /// Handle 'Do Action' button click.
        /// <br />
        /// Обработать клик по кнопке "Do Action".
        /// </summary>
        private async void OnDoActionButtonClickAsync()
        {
            await BeginPiNumberCalculationSetUp();
        }



        #endregion HANDLERS - Tier 1





        // handle topics events

        #region SECONDARY - Tier 2



        /// <summary>
        /// Run Parametrized thread demo.
        /// <br />
        /// Запустить демо Потоки с параметрами.
        /// </summary>
        private void RunHellos()
        {
            Thread myThread1 = new Thread(new ParameterizedThreadStart(Print));
            Thread myThread2 = new Thread(Print);
            Thread myThread3 = new Thread(message => Application.Current.Dispatcher.Invoke(() => OutputCollection.Add(message.ToString())));

            myThread1.Start("Hello");
            myThread2.Start("Привет");
            myThread3.Start("Salut");
        }



        /// <summary>
        /// Run shared resources demo.
        /// <br />
        /// Запустить демку по разделяемым ресурсам.
        /// </summary>
        private void RunCounter()
        {
            Thread thread;

            // to lock/unlock, switch methods in tier 3 region.
            for (int i = 0, iSize = 5; i < iSize; ++i)
            {
                thread = new(Print);
                thread.Name = $"Thread №{i}";
                thread.Start(1);
                //thread.Join();
            }

            Thread.Sleep(2000);
            ShowOutput(_largeMessage);
        }



        #endregion SECONDARY - Tier 2





        // handle excercises events

        #region AUXILIARY - Tier 3 



        /// <summary>
        /// Call messagebox to display output message.
        /// <br />
        /// Вызвать messagebox, чтобы отобразить сообщение для вывода.
        /// </summary>
        private void Print(object? message)
        {
            if (message is not null)
            {
                if (message is string)
                {
                    ShowOutput(message.ToString());
                }
                else if (message is int)
                {
                    DoSharedExampleWithSemaphore((int)message);
                }
            }
        }



        #endregion AUXILIARY - Tier 3 





        // dispatched functions for different api

        #region ELEMENTARY - Tier 4



        /// <summary>
        /// Increment a number in a loop W/O locking.
        /// <br />
        /// Проинкрементировать число в цикле БЕЗ лока.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedNumberExampleWithoutLocker(int number)
        {
            DoIncrement(number);
        }



        /// <summary>
        /// Increment a number in a loop W/ locking.
        /// <br />
        /// Проинкрементировать число в цикле С локом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithLocker(int number)
        {
            lock (_locker)
            {
                DoIncrement(number);
            }
        }



        /// <summary>
        /// Increment a number in a loop W/ auto reset event.
        /// <br />
        /// Проинкрементировать число в цикле С авто ресет ивентом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithAutoResetEvent(int number)
        {
            _autoResetHandler.WaitOne();
            DoIncrement(number);
            _autoResetHandler.Set();
        }



        /// <summary>
        /// Increment a number in a loop W/ mutex.
        /// <br />
        /// Проинкрементировать число в цикле С мьютексом.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithMutex(int number)
        {
            _mutex.WaitOne();
            DoIncrement(number);
            _mutex.ReleaseMutex();
        }



        /// <summary>
        /// Increment a number in a loop W/ semaphore.
        /// <br />
        /// Проинкрементировать число в цикле С семафором.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoSharedExampleWithSemaphore(int number)
        {
            _semaphore.WaitOne();
            DoIncrement(number);
            _semaphore.Release();
        }



        #endregion ELEMENTARY - Tier 4





        // actual generic i/o

        #region ELEMENTARY - Tier 5




        /// <summary>
        /// Increment a number in a loop.
        /// <br />
        /// Проинкрементировать число в цикле.
        /// </summary>
        /// <param name="number">
        /// A number for increment.
        /// <br />
        /// Число для инкремента.
        /// </param>
        private void DoIncrement(int number)
        {
            for (int i = 0, iSize = 6; i < iSize; ++i)
            {
                _largeMessage += Thread.CurrentThread.Name + ": " + number.ToString() + "\n";
                number++;
                Thread.Sleep(10);
            }
        }


        /// <summary>
        /// Show messagebox output message.
        /// <br />
        /// Отобразить сообщение в messagebox'е.
        /// </summary>
        private void ShowOutput(string message)
        {
            Application.Current.Dispatcher.Invoke(() => OutputCollection.Add(message));
        }




        #endregion ELEMENTARY - Tier 5






        #region Property Changed Handlers



        //



        #endregion Property Changed Handlers






        #region PI CALCULATION


        private Random random = new Random();

        // defines;
        private double dMaxAmountOfDots;    // limit_Nmax  = 1e7;
        private double dMaxCircleRadius;    // limit_a = 1e6;
        private double dStartingRadius;     // min_a = 100;


        private double squareSideLength;    // a;


        private Point point;                // double x,y,Pi;
        private double piNumber;
        private long counter;               // i;


        private long dotsInsideCircle;      // Ncirc;
        private long overallDotsCount;      // Nmax;


        private int currentTasksCount = 0;



        /// <summary>
        /// Begin parsing input data as well as calculating pi number.
        /// <br />
        /// Начать парсинг введённых данных, вместе с вычислением числа pi.
        /// </summary>
        private async Task BeginPiNumberCalculationSetUp()
        {
            //Random random = new Random();

            // defines parse
            dMaxAmountOfDots = double.Parse(MaxAmountOfDots);    // limit_Nmax  = 1e7;
            dMaxCircleRadius = double.Parse(MaxCircleRadious);   // limit_a = 1e6;
            dStartingRadius = double.Parse(StartingRadius);      // min_a = 100;

            squareSideLength = dMaxCircleRadius;                 // a;
                                                                 
            piNumber = 0;                                        // pi number;

            //counter = 0;                                         // i;

            dotsInsideCircle = 0;                                // Ncirc;
            overallDotsCount = (long)dMaxAmountOfDots;           // Nmax;


            // measuring time and calculating the number;
            await Task.Run(BeginCalculatePiNumber);

            // time and the number result output;

            var a = (double)dotsInsideCircle / (double)overallDotsCount;
            piNumber = a * 16;
            resultPiNumber = piNumber.ToString();
            elapsedTime = _stopwatch.Elapsed.TotalSeconds.ToString();

        }



        /// <summary>
        /// Begin the actual pi number calculation via Monte Carlo technique.
        /// <br />
        /// Начать само вычисление числа pi через метод Монте Карло.
        /// </summary>
        private async Task BeginCalculatePiNumber()
        {
            //Task task;
            //while (counter < overallDotsCount)
            //{
            //    point.X = random.NextInt64(0, (long)squareSideLength);
            //    point.Y = random.NextInt64(0, (long)squareSideLength);
            //
            //    //if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) lock (_locker) { dotsInsideCircle++; }
            //    //lock (_locker) { counter++; }
            //
            //    if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) dotsInsideCircle++;
            //    counter++;
            //}



            //Parallel.For(0, (long)overallDotsCount, counter =>
            //{
            //    point.X = random.NextInt64(0, (long)squareSideLength);
            //    point.Y = random.NextInt64(0, (long)squareSideLength);
            //
            //    if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) lock (_locker) { dotsInsideCircle++; }
            //    lock (_locker) { counter++; }
            //
            //    //if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) dotsInsideCircle++;
            //    //counter++;
            //});

            Point[]? points = new Point[overallDotsCount];
            
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(random.NextInt64(0, (long)squareSideLength), random.NextInt64(0, (long)squareSideLength));
            }

            currentTasksCount = 10;

            Thread[] threads = new Thread[currentTasksCount];

            _stopwatch.Reset();
            _stopwatch.Start();

            for (int i = 0; i < currentTasksCount; i++)
            {
                var x = i;

                var statrIndex = (overallDotsCount / currentTasksCount) * x;

                var endIndex = (overallDotsCount / currentTasksCount) * (x + 1);

                threads[i] = new Thread(() =>
                {
                    GenerateAndCheckDots(points, statrIndex, endIndex, x);
                });

                threads[i].Start();
            }

            //for (int i = 0; i < currentTasksCount; i++)
            //{
            //    threads[i].Join();
            //}

            await Task.Delay(5000);

            points = null;
            _stopwatch.Stop();

        }



        /// <summary>
        /// Generate and compare some amount of dots equal to 'overallDotsCount' devided by the 'CurrentTaskCount'.
        /// <br />
        /// Сгенерировать и проверить некоторое кол-во точек, равное "overallDotsCount" разделить на "CurrentTaskCount".
        /// </summary>
        private void GenerateAndCheckDots(Point[] arr, long startIndex, long endIndex, int threadIndex)
        {
            if (startIndex < endIndex)
            {
                long j = startIndex;
                try
                {
                    for (; j < endIndex; j++)
                    {
                        var point = arr[j];

                        if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2)))
                            Interlocked.Increment(ref dotsInsideCircle);

                        //Interlocked.Increment(ref counter);

                        //if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) dotsInsideCircle++;
                        //counter++;
                    }
                }
                catch (Exception ex)
                {
                    Application.Current.Dispatcher.Invoke(() => OutputCollection.Add($"Exception. threadIndex was {threadIndex} startIndex was {startIndex}, endIndex was {endIndex}, array length was {arr.Length}."));
                }
            }
        }



        /// <summary>
        /// Get a square rectangle sides' length for comparison via Monte Carlo method.
        /// <br />
        /// Получить длины сторон для проверки через метод Монте Карло.
        /// </summary>
        private double GetSquareForCircle(double xCoordinate, double radius)
        {
            return ((radius * radius) - (xCoordinate * xCoordinate));
        }


        private void CalculateSyncronous()
        {
            Point[]? points = new Point[overallDotsCount];

            for (int i = 0; i < points.Length; i++)
            {
                points[i] = new Point(random.NextInt64(0, (long)squareSideLength), random.NextInt64(0, (long)squareSideLength));
            }

            currentTasksCount = 10;

            Thread[] threads = new Thread[currentTasksCount];

            _stopwatch.Reset();
            _stopwatch.Start();

            for (int i = 0; i < currentTasksCount; i++)
            {
                long j = (overallDotsCount / currentTasksCount) * i;
                long endIndex = (overallDotsCount / currentTasksCount) * (i + 1);
                try
                {
                    for (; j < endIndex; j++)
                    {
                        var point = points[j];

                        if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2)))
                            Interlocked.Increment(ref dotsInsideCircle);

                        Interlocked.Increment(ref counter);

                        //if (point.Y * point.Y <= GetSquareForCircle(point.X, (squareSideLength / 2))) dotsInsideCircle++;
                        //counter++;
                    }
                }
                catch
                {

                }
            }



            points = null;
            _stopwatch.Stop();
        }



        #endregion PI CALCULATION



    }
}
