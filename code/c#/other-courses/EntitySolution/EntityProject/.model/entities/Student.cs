namespace MainEntityProject.Model.Entities
{
    public class Student
    {


        // У студента должны быть поля Id, FirstName, LastName, Birthday, PhoneNumber(Nullable);


        public int Id { get; set; }


        public string FirstName { get; set; } = null!;


        public string LastName { get; set; } = null!;


        public string Birthay { get; set; } = null!;


        public string? PhoneNumber { get; set; }


        public Card Card { get; set; } = null!;

    }
}
