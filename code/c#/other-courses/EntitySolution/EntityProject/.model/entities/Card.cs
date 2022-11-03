namespace MainEntityProject.Model.Entities
{
    public class Card
    {


        // У карты студента должны быть ID, SerialNumber;


        public int Id { get; set; }


        public int SerialNumber { get; set; }


        public Student Student { get; set; } = null!;


        public List<Subject>? SubjectList { get; set; }


        public List<Mark>? MarkList { get; set; }



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Card()
        {
            SubjectList = new();
            MarkList = new();
        }
    }
}
