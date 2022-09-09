using System;

using System.Threading;

using TrickAndTreat.Enumeration;


namespace TrickAndTreat.Forms
{
    /// <summary>
    /// The secondary form that contains the game itself;
    /// <br />
    /// Второстепенная форма, которая содержит саму игру;
    /// </summary>
    public partial class SecondaryForm : Form
    {


        #region PROPERTIES


        /// <summary>
        /// The amount of clicks taken by the agile button;
        /// <br />
        /// Кол-во кликов по убегающей кнопке;
        /// </summary>
        private static int _positiveScore = 0;


        /// <summary>
        /// The amount of missed clicks, i.e. failed attemts to click the button;
        /// <br />
        /// Кол-во промахов, т.е. неудачных попыток нажать на кнопку;
        /// </summary>
        private static int _negativeScore = 0;


        /// <summary>
        /// The default 'nates of the agile button;
        /// <br />
        /// Кординаты кнопки по умолчанию;
        /// </summary>
        private static Point DefaultButtonLocation = new Point(570, 475);


        /// <summary>
        /// A difficulty level of the game;
        /// <br />
        /// Уровень сложности игры;
        /// </summary>
        public GameDifficulty Difficulty { get; set; }


        #endregion PROPERTIES




        #region LOGIC



        #region MECHANICS


        /// <summary>
        /// When the coursor gets to the button location;
        /// <br />
        /// Когда курсор коснётся кнопки;
        /// </summary>
        private void OnAgileButtonMouseEnter(object sender, EventArgs e)
        {
            Thread tread = new Thread(new ThreadStart(ChangeLocation));

            tread.IsBackground = true;

            tread.Start();
        }


        /// <summary>
        /// Change the button 'nates;
        /// <br />
        /// Изменить координаты кнопки;
        /// </summary>
        private void ChangeLocation()
        {
            // prepairing;
            Random rand = new Random();
            Thread.Sleep((GameDifficulty.Insane - Difficulty) * 50);
            int nRandomX = 0;
            int nRandomY = nRandomX;
            var oldlocation = this.AgileButton.Location;

            // <coordinates roll>;
            do
            {
                nRandomX = rand.Next(AgileButton.Width, this.MinimumSize.Width - AgileButton.Width * 2);
                nRandomY = rand.Next(AgileButton.Height, this.MinimumSize.Height - AgileButton.Height * 2);
            }
            while
            (
            Math.Abs(oldlocation.X - nRandomX) <= AgileButton.Size.Width
         && Math.Abs(oldlocation.Y - nRandomY) <= AgileButton.Size.Height
            );
            // <coordinates roll />;

            this.Invoke(new Action(() => this.AgileButton.Location = new Point(nRandomX, nRandomY)));
        }


        /// <summary>
        /// When the user does click the button;
        /// <br />
        /// Когда пользователю всё же удаётся нажать на кнопку;
        /// </summary>
        private void OnAgileButtonMouseClick(object sender, EventArgs e)
        {
            // If the user doesn't have enought score to win the game;
            if (!GetVictoryCondition()) 
            {
                _positiveScore++;

                PositiveScoreLabel.Text = _positiveScore.ToString();

                if (!PositiveScoreLabel.Visible) PositiveScoreLabel.Visible = true;
            }
            // When it's enough score for the victory;
            else
            {
                MessageBox.Show("You've won!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
        }


        /// <summary>
        /// When the user misses the button and clicks somewhere else;
        /// <br />
        /// Когда пользователь промахивается и попадает в пустоту;
        /// </summary>
        private void OnSecondaryFormClick(object sender, EventArgs e)
        {
            _negativeScore++;

            NegariveScoreLabel.Text = _negativeScore.ToString();

            // If it's 'Insane' mode, show the score and check for the defeat;
            if (Difficulty == GameDifficulty.Insane)
            {
                if (!NegariveScoreLabel.Visible) NegariveScoreLabel.Visible = true;

                if (GetInsaneModLooseCondition()) 
                {
                    MessageBox.Show("Better luck next time", "You've lost!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Close();
                }
            }
        }


        /// <summary>
        /// When the game form is closed;
        /// <br />
        /// Когда форма с игрой закрывается;
        /// </summary>
        private void OnSecondaryFormFormClosed(object sender, FormClosedEventArgs e)
        {
            _negativeScore = 0;
            _positiveScore = 0;

            Difficulty = GameDifficulty.Undefined;
        }


        /// <summary>
        /// When the user hits the green-with-two-arrows button;
        /// <br />
        /// Когда пользователь нажмёт зелёную кнопку с двумя стрелками;
        /// </summary>
        private void OnRestartButtonClick(object sender, EventArgs e)
        {
            Restart();
        }


        /// <summary>
        /// When the user hits the red button;
        /// <br />
        /// [!] Available in the 'Insane' mode only;
        /// <br />
        /// Когда пользователь нажмёт на красную кнопку;
        /// <br />
        /// [!] Доступно только в режиме "Мастер";
        /// </summary>
        private void OnGiveUpButtonClick(object sender, EventArgs e)
        {
            GiveUp();
        }


        #endregion MECHANICS



        #region AUXILIARY


        /// <summary>
        /// Check up the condition leading to the 'Victory' ending;
        /// <br />
        /// Проверить условие, ведущее к сценарию "победа";
        /// </summary>
        /// <returns>true - if player got enough positive score, otherwise false;<br />true - если игрок заработал достаточно очков, иначе false;</returns>
        private bool GetVictoryCondition()
        {
            return _positiveScore >= ((GameDifficulty.Insane - Difficulty) + 1) * 3;
        }


        /// <summary>
        /// Check up the condition leading to the 'Defeat' ending;
        /// <br />
        /// [!] Available in the 'Insane' mode only;
        /// <br />
        /// Проверить условие, ведущее к сценарию "поражение";
        /// <br />
        /// Доступно только в режиме "Мастер";
        /// </summary>
        /// <returns>true - if player got enough negative score, otherwise false;<br />true - если игрок заработал достаточно очков промаха, иначе false;</returns>
        private bool GetInsaneModLooseCondition()
        {
            return _negativeScore >= 300;
        }


        /// <summary>
        /// Give up and loose the game;
        /// <br />
        /// [!] Available in the 'Insane' mode only;
        /// <br />
        /// Сдаться и проиграть;
        /// <br />
        /// [!] Доступно только в режиме "Мастер";
        /// </summary>
        private void GiveUp()
        {
            _negativeScore = 300;
            OnSecondaryFormClick(null, null);
        }


        /// <summary>
        /// Restart the current game;
        /// <br />
        /// Начать текущую игру сначала;
        /// </summary>
        private void Restart()
        {
            _negativeScore = 0;
            NegariveScoreLabel.Text = _negativeScore.ToString();
            NegariveScoreLabel.Visible = false;

            _positiveScore = 0;
            PositiveScoreLabel.Text = _positiveScore.ToString();
            PositiveScoreLabel.Visible = false;

            AgileButton.Location = DefaultButtonLocation;
        }


        /// <summary>
        /// Reset the current form properties;
        /// <br />
        /// Сбросить текущие настройки формы;
        /// </summary>
        private void ResetFormProperties()
        {
            _negativeScore = 0;
            NegariveScoreLabel.Text = _negativeScore.ToString();
            NegariveScoreLabel.Visible = false;

            _positiveScore = 0;
            PositiveScoreLabel.Text = _positiveScore.ToString();
            PositiveScoreLabel.Visible = false;

            AgileButton.Location = DefaultButtonLocation;
        }


        #endregion AUXILIARY



        #endregion LOGIC




        #region CONSTRUCTION


        /// <summary>
        /// Default ctor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public SecondaryForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// When the form appears on the screen;
        /// <br />
        /// При загрузке формы на экран;
        /// </summary>
        private void OnSecondaryFormLoad(object sender, EventArgs e)
        {
            InitializeForm();
        }


        /// <summary>
        /// Adjust several options and counters;
        /// <br />
        /// Подогнать некоторые настройки и счётчики;
        /// </summary>
        private void InitializeForm()
        {
            // reset the counters;
            ResetFormProperties();

            // an 'insane' mode setting;
            GiveUpButton.Visible = false;

            // set up the difficulty properties;
            ApplyDifficulty();
        }


        /// <summary>
        /// Apply all difficulty related settings;
        /// <br />
        /// Применить все возможные настройки сложности;
        /// </summary>
        private void ApplyDifficulty()
        {
            switch (Difficulty)
            {
                case GameDifficulty.Easy:
                    SetEasyDifficulty();
                    break;

                case GameDifficulty.Medium:
                    SetMediumDifficulty();
                    break;

                case GameDifficulty.Hard:
                    SetHardDifficulty();
                    break;

                case GameDifficulty.Insane:
                    SetInsaneDifficulty();
                    break;
            }

            this.Text = "T.N.T. - " + this.Difficulty.ToString();
        }



        #region DIFFICULTIES


        /// <summary>
        /// Set the 'Easy' difficulty level;
        /// <br />
        /// Установить уровень сложности 'Лёгкий';
        /// </summary>
        private void SetEasyDifficulty()
        {
            AgileButton.Margin = new System.Windows.Forms.Padding(1);
        }


        /// <summary>
        /// Set the 'Medium' difficulty level;
        /// <br />
        /// Установить уровень сложности 'Средний';
        /// </summary>
        private void SetMediumDifficulty()
        {
            AgileButton.Margin = new System.Windows.Forms.Padding(2);
        }


        /// <summary>
        /// Set the 'Hard' difficulty level;
        /// <br />
        /// Установить уровень сложности 'Сложный';
        /// </summary>
        private void SetHardDifficulty()
        {
            AgileButton.Margin = new System.Windows.Forms.Padding(3);
        }


        /// <summary>
        /// Set the 'Insane' difficulty level;
        /// <br />
        /// Установить уровень сложности 'Мастер';
        /// </summary>
        private void SetInsaneDifficulty()
        {
            AgileButton.Margin = new System.Windows.Forms.Padding(4);

            GiveUpButton.Visible = true;
        }


        #endregion DIFFICULTIES



        #endregion CONSTRUCTION


    }
}