namespace TankFactory.Model.Entities
{
    public class Engine
    {

        public string? ModelName { get; set; }

        public int ManufacturerId { get; set; }

        public int PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }

        public int Id { get; set; }

        public int HorsePowers { get; set; } 




        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Engine()
        {
            ModelName = null;
            HorsePowers = 0;
            PriceReference = null;
            ManufacturerReference = null;
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Engine(string modelName, int horsePowers, Manufacturer manufacturerReference, Price? priceReference)
        {
            ModelName = modelName;
            HorsePowers = horsePowers;
            ManufacturerReference = manufacturerReference;
            PriceReference = priceReference;
        }

    }
}
