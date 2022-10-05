

namespace EntityHomeworkSecond.Model.Entities
{
    public class Card
    {


        // У карты студента должны быть ID, SerialNumber;


        public int Id { get; set; }


        public int SerialNumber { get; set; }


        public Student? Student { get; set; }


    }
}
