namespace TankFactory.Model.Entities
{
    public class Budget
    {

        public int Id { get; set; }

        public long Value { get; set; }

        public string Currency { get; set; } = null!;


        public Manufacturer? ManufacturerReference { get; set; }



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
