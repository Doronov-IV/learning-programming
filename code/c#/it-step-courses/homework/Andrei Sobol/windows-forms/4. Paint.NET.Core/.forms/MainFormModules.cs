using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.NET.Core.Forms
{
    public partial class MainForm
    {


        #region Module: Top Text Menu Strip



        /// <summary>
        /// .
        /// <br />
        /// .
        /// </summary>
        private void OnOpenFileButtonClick(object sender, EventArgs e)
        {
            if (MainOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                MainPictureBox.Image = new Bitmap(MainOpenFileDialog.FileName);
            }
        }



        #endregion Module: Top Text Menu Strip







        #region Module: Top Toolstrip Buttons



        /// <summary>
        /// Handle the instruments button click event.
        /// <br />
        /// Обработать событие клика по кнопке "Instruments".
        /// </summary>
        private void OnShowInstrumentsButtonClick(object sender, EventArgs e)
        {

        }



        #endregion Module: Top Toolstrip Buttons








        #region Module: Main Controls



        /// <summary>
        /// Handle color button click.
        /// <br />
        /// Обработать клик кнопки выбора цвета.
        /// </summary>
        private void OnColorButtonClick(object sender, EventArgs e)
        {
            MainColorDialog.ShowDialog();

        }



        #endregion Module: Main Controls


    }
}
