namespace MainEntityProject.Model.Entities
{
    public class Price
    {

        public int Id { get; set; }

        public long Value { get; set; }

        public string Currency { get; set; } = null!;


        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Price()
        {
            Value = 0;
            Currency = "N/A";
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Price(long value, string currency)
        {
            Value = value;
            Currency = currency;
        }


    }
}
