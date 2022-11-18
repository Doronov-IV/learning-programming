namespace MainEntityProject.Model.Entities
{
    /// <summary>
    /// A book. An instance of a 'Books' table record.
    /// <br />
    /// Кинга. Экземпляр записи в таблице "Книги".
    /// </summary>
    public class Book
    {

        public int Id { get; set; }


        public string Title { get; set; } = null!;


        public string Description { get; set; } = null!;


        public int AuthorId { get; set; }


        public Author? Author { get; set; }


    }
}
