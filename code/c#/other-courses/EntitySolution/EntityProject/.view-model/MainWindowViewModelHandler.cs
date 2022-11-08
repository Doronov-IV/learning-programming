using MainEntityProject.Model.Context;
using MainEntityProject.Model.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MainEntityProject.ViewModel
{
    public partial class MainWindowViewModel
    {



        #region HANDLERS



        /// <summary>
        /// Handle 'Clear Database' button click event.
        /// <br />
        /// Обработать событие нажатия на кнопку "Clear Database".
        /// </summary>
        public async void OnClearTablesButtonClickAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (CurrentDatabaseContext context = new CurrentDatabaseContext(_connectionOptions))
                    {
                        context.Database.ExecuteSqlRaw("DELETE FROM [Books]");
                        context.Database.ExecuteSqlRaw("DELETE FROM [Authors]");

                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Connection failed.\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }




        /// <summary>
        /// Handle the 'Fill Tables' click event.
        /// <br />
        /// Обработать событие клика по кнопке "Fill Tables".
        /// </summary>
        public async void OnDoFillTablesClickAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (CurrentDatabaseContext context = new CurrentDatabaseContext(_connectionOptions))
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



        /// <summary>
        /// Handle 'Change Authors' button click event.
        /// <br />
        /// Обработать событие клика по кнопке "Change Authors".
        /// </summary>
        private async void OnChangeAuthorsClickAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    using (CurrentDatabaseContext context = new CurrentDatabaseContext(_connectionOptions))
                    {
                        context.Authors.AsEnumerable().Select(a => { if (a.Name.Contains("Defoe")) a.Name = "Definitely not Daniel Defoe"; return a; }).ToList();

                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Update failed.\n\nException: {ex.Message}", "Exception.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }



        #endregion HANDLERS




        #region SQL CONNECTION



        /// <summary>
        /// Create JSON SQL-connection file.
        /// <br />
        /// Создать JSON файл конфигурации SQL-подключения.
        /// </summary>
        private void InitializeSQLConnection()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile(".config/appsettings.json");

            var config = builder.Build();
            string _connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CurrentDatabaseContext>();
            _connectionOptions = optionsBuilder
                .UseSqlServer(_connectionString)
                .Options;
        }



        #endregion SQL CONNECTION



    }
}
