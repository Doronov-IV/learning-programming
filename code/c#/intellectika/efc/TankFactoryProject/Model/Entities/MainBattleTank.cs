namespace MainEntityProject.Model.Entities
{
    public class MainBattleTank
    {

        public string? ModelName { get; set; }

        public int ManufacturerId { get; set; }

        public int PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }


        public int Id { get; set; }

        public int CrewCount { get; set; }

        public int GunId { get; set; }

        public int EngineId { get; set; }

        public Gun? GunReference { get; set; }

        public Engine? EngineReference { get; set; }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainBattleTank()
        {
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public MainBattleTank(string? modelName, int crewCount, Manufacturer? manufactorerReference, Price? priceReference, Gun? gunReference, Engine? engineReference)
        {
            ModelName = modelName;
            CrewCount = crewCount;
            ManufacturerReference = manufactorerReference;
            PriceReference = priceReference;
            GunReference = gunReference;
            EngineReference = engineReference;
        }


    }
}
