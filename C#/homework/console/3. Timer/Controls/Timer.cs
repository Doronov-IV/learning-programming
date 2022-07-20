using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Tools.Formatting;

using Spectre.Console;

namespace Homework.NET
{
    public class Timer
    {


        #region KEY_STATES

        protected bool isRunning;

        protected int nSeconds;

        protected int nMinutes;

        protected int nHours;

        protected readonly int nMillisecondsPerTick;

        #endregion KEY_STATES



        #region AUXILIARY_STATES

        private readonly int TIME_X_POSITION = 7;

        private readonly int TIME_Y_POSITION = 1;

        private readonly int MENU_X_POSITION = 0;

        private readonly int MENU_Y_POSITION = 4;

        protected readonly string sPlaceHolder = "               ";

        #endregion AUXILIARY_STATES



        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />;
        /// Конструктор по умолчанию;
        /// </summary>
        public Timer()
        {
            isRunning = false;

            nMillisecondsPerTick = 1000;
        }


        #endregion CONSTRUCTION



        #region MECHANICS


        /// <summary>
        /// The main method;
        /// <br />;
        /// Основной метод;
        /// </summary>
        public void Start()
        {
            ShowGeneralInfo();
            char cKey = Console.ReadKey(true).KeyChar;
            switch (cKey)
            {
                case '1':
                    if (!isRunning && nSeconds == 0) Start(nHours, nMinutes, nSeconds);
                    else Stop(); 
                    break;
                case '2':
                    Continue();
                    break;
            }
            Start();
        }


        /// <summary>
        /// Increment the minutes counter;
        /// <br />
        /// Увеличить счётчик минут;
        /// </summary>
        private void IncreaseMinutes()
        {
            if (nMinutes < 59) ++nMinutes;
            else
            {
                nMinutes = 0;
                ++nHours;
            }
        }


        #endregion MECHANICS



        #region CONTROLS


        /// <summary>
        /// Stop the timer;
        /// <br />
        /// Остановить таймер;
        /// </summary>
        public void Stop()
        {
            ShowGeneralInfo();
            if (!isRunning) Reset();
            else isRunning = false;
            ShowTime();
        }


        /// <summary>
        /// Continue the timer;
        /// <br />
        /// Продолжить отсчёт;
        /// </summary>
        public void Continue()
        {
            if (!isRunning && (nSeconds + nMinutes + nHours) > 0) Start(nHours, nMinutes, nSeconds);
        }


        /// <summary>
        /// Reset the timer;
        /// <br />
        /// Сбросить отсчёт;
        /// </summary>
        public void Reset()
        {
            Console.Clear();
            nSeconds = nMinutes = nHours = 0;
            ShowTime();
        }


        #endregion CONTROLS



        #region THREADING


        /// <summary>
        /// Start counting time (again);
        /// <br />
        /// Начать отсчёт времени;
        /// </summary>
        /// <param name="nHour">Текущий показатель часов;</param>
        /// <param name="nMinute">Текущий показатель минут;</param>
        /// <param name="nSecond">Текущий показатель секунд;</param>
        public void Start(int nHour, int nMinute, int nSecond)
        {
            Thread tCurrentThread = new Thread(StartTimerThread);
            tCurrentThread.Start();
            isRunning = true;
        }


        /// <summary>
        /// Start timer thread;
        /// <br />
        /// Создать поток таймера;
        /// </summary>
        private void StartTimerThread()
        {
            while (isRunning)
            {
                ShowTime();
                SleepForTick();
            }
        }


        /// <summary>
        /// Sleep for a tick;
        /// <br />
        /// Уснуть на тик;
        /// </summary>
        private void SleepForTick()
        {
            if (isRunning)
            {
                Thread.Sleep(nMillisecondsPerTick);
                if (nSeconds < 59) nSeconds++;
                else
                {
                    IncreaseMinutes();
                    nSeconds = 0;
                }
            }
        }


        #endregion THREADING



        #region VISUAL


        /// <summary>
        /// Show time;
        /// <br />
        /// Показать время;
        /// </summary>
        private void ShowTime()
        {
            Thread.Sleep(30);
            Console.SetCursorPosition(TIME_X_POSITION, TIME_Y_POSITION);
            Thread.Sleep(30);
            AnsiConsole.Write(new Markup("[underline]" + nHours.ToString("00") + ":" +
                nMinutes.ToString("00") + ":" + nSeconds.ToString("00") + "[/]"));
            Thread.Sleep(30);
        }

        /// <summary>
        /// Show controlls menu;
        /// <br />
        /// Показать меню действий;
        /// </summary>
        private void ShowGeneralInfo()
        {
            Console.SetCursorPosition(MENU_X_POSITION, MENU_Y_POSITION);
            if (!isRunning && nSeconds == 0) 
                AnsiConsole.Write(new Markup("[blue]> [/]1. Start" + sPlaceHolder + "\n" + sPlaceHolder));
            else if (!isRunning && nSeconds != 0) 
                AnsiConsole.Write(new Markup("[blue]> [/]1. Reset" + sPlaceHolder + "\n[blue]> [/]2. Continue"));
            else AnsiConsole.Write(new Markup("[blue]> [/]1. Stop" + sPlaceHolder + "\n" + sPlaceHolder));
        }


        #endregion VISUAL



    }
}
