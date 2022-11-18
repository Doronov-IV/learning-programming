namespace MainEntityProject.Model.Entities
{
    /// <summary>
    /// An author of one or some books.
    /// <br />
    /// Автор одной или нескольких книг.
    /// </summary>
    public class Author
    {


        public int Id { get; set; }


        public string Name { get; set; } = null!;


        public string? WebUrl { get; set; }


        public List<Book> BookList { get; set; } = null!;



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Author()
        {
            BookList = new();
        }


    }
}
