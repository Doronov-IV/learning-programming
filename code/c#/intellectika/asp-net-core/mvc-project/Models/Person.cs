namespace mvcproject.Models
{
    public class Person
    {

        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Person()
        {
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Person(string id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

    }
}
