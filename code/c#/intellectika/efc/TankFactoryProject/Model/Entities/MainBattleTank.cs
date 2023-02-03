namespace MainEntityProject.Model.Entities
{
    [Index(nameof(ModelName), IsUnique = true)]
    public class MainBattleTank : ICloneable
    {

        public string? ModelName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }


        public int Id { get; set; }

        public int? CrewCount { get; set; }

        public int? GunId { get; set; }

        public int? EngineId { get; set; }

        public Gun? GunReference { get; set; }

        public Engine? EngineReference { get; set; }




        /// <summary>
        /// Equals method override.
        /// <br />
        /// Переопределение метода "Equals".
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is MainBattleTank)
            {
                var mbtRef = obj as MainBattleTank;
                return mbtRef.ModelName.Equals(this.ModelName);
            }

            else return base.Equals(obj);
        }

        public object Clone()
        {
            return new MainBattleTank(ModelName, CrewCount, ManufacturerReference, PriceReference, GunReference, EngineReference);
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public MainBattleTank()
        {
            GunReference = new();
            EngineReference = new();
            ManufacturerReference = new();
        }



        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public MainBattleTank(string? modelName, int? crewCount, Manufacturer? manufactorerReference, Price? priceReference, Gun? gunReference, Engine? engineReference)
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
