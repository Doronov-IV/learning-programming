using MainEntityProject.Model.Context;
using MainEntityProject.Model.Entities;
using Microsoft.Data.SqlClient;

namespace MainEntityProject.ViewModel
{
    public partial class MainWindowViewModel
    {

        #region HANDLERS








        /// <summary>
        /// Handle connect button click event;
        /// <br />
        /// Обработать нажатие кнопки "Connect";
        /// </summary>
        public void OnConnectButtonClick()
        {
            // Connection string инициализируется в конструкторе вью-модели.
            connectionString = $@"Server=.\{ServerName};Database = master;Trusted_Connection=true;Encrypt=false";

            using (SqlConnection connection = new(connectionString))
            {
                try
                {
                    connection.Open();

                    connection.Close();

                    ConnectionStatus.Toggle();

                    // Если не работает Entity, воткните после "Server=" символы ".\";
                    connectionString = $@"Server=.\{ServerName};Database = MainEFCproject;Trusted_Connection=true;Encrypt=false";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed. (location .view-model/Handler/OnConnectButtonClick)\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }




        /// <summary>
        /// Handle main action button click event.
        /// <br />
        /// Обработать событие клика по кнопке "Do Action".
        /// </summary>
        public async void OnDoActionButtonClickAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (CurrentDatabaseContext context = new CurrentDatabaseContext())
                    {
                        List<Book> bookList1 = new();
                        List<Book> bookList2 = new();
                        List<Book> bookList3 = new();

                        Author author1 = new() { Name = "Mark Twain", WebUrl = "https://en.wikipedia.org/wiki/Mark_Twain" };
                        Author author2 = new() { Name = "Daniel Defoe", WebUrl = "https://en.wikipedia.org/wiki/Daniel_Defoe" };
                        Author author3 = new() { Name = "Jules Verne", WebUrl = "https://en.wikipedia.org/wiki/Jules_Verne" };

                        Book book1 = new() { Author = author1, Title = "The Adventures of Tom Sawyer", Description = "lorem100" };
                        Book book2 = new() { Author = author1, Title = "Adventures of Huckleberry Finn", Description = "lorem100" };

                        Book book3 = new() { Author = author2, Title = "Robinson Crusoe", Description = "lorem100" };
                        Book book4 = new() { Author = author2, Title = "Captain Singleton", Description = "lorem100" };

                        Book book5 = new() { Author = author3, Title = "Twenty Thousand Leagues Under the Seas", Description = "lorem100" };
                        Book book6 = new() { Author = author3, Title = "The Mysterious Island", Description = "lorem100" };

                        bookList1.Add(book1);
                        bookList1.Add(book2);

                        bookList2.Add(book3);
                        bookList2.Add(book4);

                        bookList3.Add(book5);
                        bookList3.Add(book6);

                        author1.BookList = bookList1;
                        author2.BookList = bookList2;
                        author3.BookList = bookList3;

                        context.Authors.AddRange(author1, author2);
                        context.Books.AddRange(book1, book2, book3, book4, book5, book6);

                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed.\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }



        #endregion HANDLERS

    }
}
