namespace MainEntityProject.Model.Entities
{
    public class Manufacturer
    {

        public int Id { get; set; }

        public string? Name { get; set; } = null!;

        public string? CountryName { get; set; }

        public int? BudgetId { get; set; }

        public Budget? BudgetReference { get; set; }




        /// <summary>
        /// Equals method override.
        /// <br />
        /// Переопределение метода "Equals".
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Manufacturer)
            {
                var manufacturerRef = obj as Manufacturer;
                return manufacturerRef.Name.Equals(this.Name);
            }

            else return base.Equals(obj);
        }




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Manufacturer()
        {
            Name = null;
            CountryName = null;
            BudgetReference = new();
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Manufacturer(string name, string countryName, Budget? budgetReference) : base()
        {
            Name = name;
            CountryName = countryName;
            BudgetReference = budgetReference;
        }

    }
}
