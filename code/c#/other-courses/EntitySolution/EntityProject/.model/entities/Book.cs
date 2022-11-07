namespace MainEntityProject.Model.Entities
{
    public class Book
    {


        public int Id { get; set; }


        public string Title { get; set; } = null!;


        public string Description { get; set; } = null!;


        public int AuthorId { get; set; }


        public Author? Author { get; set; }


    }
}
