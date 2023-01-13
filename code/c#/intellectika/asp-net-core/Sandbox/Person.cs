namespace emptyproject
{
    public class Person
    {

        public string Name { get; set; }
        public int Age { get; set; }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Person(string name, int age)
        {
            Name = name;
            age = Age;
        }

    }
}
