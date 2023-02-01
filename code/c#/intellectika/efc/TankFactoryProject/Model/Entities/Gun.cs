namespace MainEntityProject.Model.Entities
{
    public class Gun
    {

        public string? ModelName { get; set; }

        public int ManufacturerId { get; set; }

        public int PriceId { get; set; }

        public Manufacturer? ManufacturerReference { get; set; }

        public Price? PriceReference { get; set; }

        public int Id { get; set; }

        public int CaliberMillimetres { get; set; }

        public int LengthInCalibers { get; set; }



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
    }
}
