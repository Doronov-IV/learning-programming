using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using TrickAndTreat.Enumeration;


namespace TrickAndTreat.Forms
{
    /// <summary>
    /// The main form of the program, to select the difficulty level;
    /// <br />
    /// Основная форма программы, чтобы выбрать уровень сложности;
    /// </summary>
    public partial class PrimaryForm : Form
    {


        #region LOGIC


        /// <summary>
        /// When the 'Easy' button is clicked;
        /// <br />
        /// Когда кликнут по кнопке "Лёгкий";
        /// </summary>
        private void OnEasyButtonClick(object sender, EventArgs e)
        {
            ShowGameForm(GameDifficulty.Easy);
        }


        /// <summary>
        /// When the 'Medium' button is clicked;
        /// <br />
        /// Когда кликнут по кнопке "Средний";
        /// </summary>
        private void OnMediumButtonClick(object sender, EventArgs e)
        {
            ShowGameForm(GameDifficulty.Medium);
        }


        /// <summary>
        /// When the 'Hard' button is clicked;
        /// <br />
        /// Когда кликнут по кнопке "Сложный";
        /// </summary>
        private void OnHardButtonClick(object sender, EventArgs e)
        {
            ShowGameForm(GameDifficulty.Hard);
        }


        /// <summary>
        /// When the 'Insane' button is clicked;
        /// <br />
        /// Когда кликнут по кнопке "Мастер";
        /// </summary>
        private void OnInsaneButtonClick(object sender, EventArgs e)
        {
            ShowGameForm(GameDifficulty.Insane);
        }


        /// <summary>
        /// Transit to the secondary form;
        /// <br />
        /// Переход к следующей форме;
        /// </summary>
        private void ShowGameForm(GameDifficulty difficulty)
        {
            SecondaryForm newform = new SecondaryForm();

            newform.Difficulty = difficulty;

            newform.ShowDialog();
        }


        #endregion LOGIC




        #region CONSTRUCTION


        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public PrimaryForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// When the form appears on the screen;
        /// <br />
        /// При загрузке формы на экран;
        /// </summary>
        private void OnPrimaryFormLoad(object sender, EventArgs e)
        {
            // write your code here ...
        }


        #endregion CONSTRUCTION


    }
}
