using Newtonsoft.Json.Linq;

namespace MainEntityProject.Model.Entities
{
    [Index(nameof(ModelName), IsUnique = true)]
    public class Gun
    {

        public string? ModelName { get; set; }

        public int? ManufacturerId { get; set; }

        public int? PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }

        public int? Id { get; set; }

        public int? CaliberMillimetres { get; set; }

        public double? LengthInCalibers { get; set; }




        /// <summary>
        /// Equals method override.
        /// <br />
        /// Переопределение метода "Equals".
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is Gun)
            {
                var gunRef = obj as Gun;
                return gunRef.ModelName.Equals(this.ModelName);
            }

            else return base.Equals(obj);
        }



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public Gun()
        {
            ModelName = null;
            CaliberMillimetres = 0;
            LengthInCalibers = 0;

            PriceReference = null;
            ManufacturerReference = null;
        }


        /// <summary>
        /// Parametrized constructor.
        /// <br />
        /// Параметризованный конструктор.
        /// </summary>
        public Gun(string? modelName, int caliberMillimetres, double lengthInCalibers, Manufacturer? manufacturerReference, Price? priceReference) : base()
        {
            ModelName = modelName;
            CaliberMillimetres = caliberMillimetres;
            LengthInCalibers = lengthInCalibers;
            ManufacturerReference = manufacturerReference;
            PriceReference = priceReference;
        }
    }
}
