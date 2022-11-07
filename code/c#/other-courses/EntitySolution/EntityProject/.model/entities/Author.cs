namespace MainEntityProject.Model.Entities
{
    public class Author
    {


        public int Id { get; set; }


        public string Name { get; set; } = null!;


        public string WebUrl { get; set; } = null!;


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
