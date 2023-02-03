namespace MainEntityProject.Model.Entities
{
    public class Budget
    {

        public int Id { get; set; }

        public long Value { get; set; }

        public string Currency { get; set; } = null!;

        public Manufacturer? ManufacturerReference { get; set; }




        /// <summary>
        /// Equals method override.
        /// <br />
        /// Переопределение метода "Equals".
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Budget)
            {
                var bdgRef = obj as Budget;
                return bdgRef.Value.Equals(this.Value) && bdgRef.Currency.Equals(this.Currency);
            }

            else return base.Equals(obj);
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Budget()
        {
            Value = 0;
            Currency = "N/A";
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Budget(long value, string currency)
        {
            Value = value;
            Currency = currency;
        }


    }
}
