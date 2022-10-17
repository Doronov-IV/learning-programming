global using System;
global using System.Collections.Generic;
global using System.ComponentModel;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Windows;
global using Microsoft.EntityFrameworkCore;
global using System.Collections.ObjectModel;

using EntityHomeworkSecond.Model;
using Prism.Commands;
using EntityHomeworkSecond.Model.Context;
using EntityHomeworkSecond.Model.Entities;

namespace EntityHomeworkSecond.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {


        #region Property changed


        /// <summary>
        /// Propery changed event handler;
        /// <br />
        /// Делегат-обработчик события 'property changed';
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        /// Handler-method of the 'property changed' delegate;
        /// <br />
        /// Метод-обработчик делегата 'property changed';
        /// </summary>
        /// <param name="propName">The name of the property;<br />Имя свойства;</param>
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


        #endregion Property changed





        #region COMMANDS


        public DelegateCommand FillDatabaseClickCommand { get; }


        #endregion COMMANDS





        #region HANDLING


        private void OnFillDatabaseClick()
        {
            using (LocalDbContext context = new())
            {
                try
                {
                    Card card1;
                    Card card2;

                    Card card3;

                    Student student1 = new() { FirstName = "John", LastName = "von Neumann", Birthay = "28/12/1903", PhoneNumber = "88005553535" };
                    Student student2 = new() { FirstName = "Ada", LastName = "Lovelace", Birthay = "10/12/1815", PhoneNumber = "88005553534" };

                    card1 = new() { Student = student1 };
                    card2 = new() { Student = student2 };
                    // Проверил, не запушилось, потому что ссылка на студента повторяется;
                    card3 = new() { Student = student2 };

                    student1.Card = card1;
                    student2.Card = card2;

                    context.Students.AddRange(student1, student2);
                    context.Cards.AddRange(card1, card2, card3);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion HANDLING







        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModel()
        {
            FillDatabaseClickCommand = new DelegateCommand(OnFillDatabaseClick);
        }



    }
}
