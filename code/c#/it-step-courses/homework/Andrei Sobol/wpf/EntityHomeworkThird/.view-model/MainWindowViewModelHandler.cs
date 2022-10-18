using EntityHomeworkThird.Model.Context;
using EntityHomeworkThird.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace EntityHomeworkThird.ViewModel
{
    /// <summary>
    /// A way to decompose viewmodel by separating handler methods;
    /// <br />
    /// Способ декомпозировать вью-подель, отделив методы обработки;
    /// </summary>
    public class MainWindowViewModelHandler
    {


        #region PROPERTIES



        /// <summary>
        /// ViewModel reference;
        /// <br />
        /// Ссылка на вью-модель;
        /// </summary>
        private MainWindowViewModel _ViewModelReference;



        #endregion PROPERTIES






        #region HANDLERS


        public void OnFillButtonClick()
        {
            using (CurrentDatabaseContext context = new())
            {
                try
                {
                    Card card1;
                    Card card2;

                    Student student1 = new() { FirstName = "John", LastName = "von Neumann", Birthay = "28/12/1903", PhoneNumber = "88005553535" };
                    Student student2 = new() { FirstName = "Ada", LastName = "Lovelace", Birthay = "10/12/1815", PhoneNumber = "88005553534" };

                    card1 = new() { Student = student1 };
                    card2 = new() { Student = student2 };

                    student1.Card = card1;
                    student2.Card = card2;

                    context.Students.AddRange(student1, student2);
                    context.Cards.AddRange(card1, card2);

                    List<Card> tempCardList = new();
                    tempCardList.Add(card1);
                    tempCardList.Add(card2);

                    Subject subject1 = new() { CardList = tempCardList, Name = "Computer Science" };
                    Subject subject2 = new() { CardList = tempCardList, Name = "Programming" };

                    context.Subjects.AddRange(subject1, subject2);


                    Mark mark1 = new() { Card = card1, Subject = subject1, Value = 75 };
                    Mark mark2 = new() { Card = card1, Subject = subject2, Value = 100 };

                    Mark mark3 = new() { Card = card2, Subject = subject1, Value = 100 };
                    Mark mark4 = new() { Card = card2, Subject = subject2, Value = 175 };

                    context.Marks.AddRange(mark1, mark2, mark3, mark4);


                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        public void OnClearButtonClick()
        {
            using (CurrentDatabaseContext context = new())
            {
                try
                {
                    // Здесь я пробовал сделать через контекст, но SSMS затупил и не отображал изменения в базе;
                    // Понял это уже когда переделал через команды бывшего ADO;
                    using (SqlConnection adoConnection = new(_ViewModelReference.ConnectionString))
                    {
                        adoConnection.Open();

                        string comandText = "USE DoronovEntityCoreThird DELETE FROM Marks; DELETE FROM Subjects; DELETE FROM Cards; DELETE FROM Students;";

                        SqlCommand command = new(comandText, adoConnection);

                        command.ExecuteNonQuery();

                        adoConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong.\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        #endregion HANDLERS






        #region CONSTRUCTION



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public MainWindowViewModelHandler(MainWindowViewModel ViewModelReference)
        {
            _ViewModelReference = ViewModelReference;
        }



        #endregion CONSTRUCTION


    }
}
