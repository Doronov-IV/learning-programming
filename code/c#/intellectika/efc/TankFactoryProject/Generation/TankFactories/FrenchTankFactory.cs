namespace MainEntityProject.Generation.TankFactories
{
    public class FrenchTankFactory : ITankFactory
    {


        #region API



        public MainBattleTank CreateImportedTank()
        {
            return CreateSK105();
        }

        public MainBattleTank CreateNativeTank()
        {
            return CreateLeclercS2();
        }



        #endregion API




        #region Logic



        public MainBattleTank CreateLeclercS2()
        {
            var gunManufacturer = new Manufacturer("Nexter Systems", "France", null);
            var engineManufacturer = new Manufacturer("Wärtsilä", "Finland", null);

            return new MainBattleTank(
                modelName: "Leclerc S1",
                crewCount: 3,
                manufactorerReference: gunManufacturer,
                priceReference: new Price(16_000_000, "€"),
                engineReference: new Engine("V8X SACM", 1500, engineManufacturer, null),
                gunReference: new Gun("CN120-26", 120, 52, gunManufacturer, null)
            );

        }



        public MainBattleTank CreateSK105()
        {
            var engineManufacturer = new Manufacturer("Steyr", "Austria", null);
            var gunManufacturer = new Manufacturer("arsenal de Bourges (ABS)", "France", null);

            return new MainBattleTank(
                modelName: "SK-105 \"Kürassier\"",
                crewCount: 4,
                manufactorerReference: engineManufacturer,
                priceReference: null,
                engineReference: new Engine("Steyr 7FA", 320, engineManufacturer, null),
                gunReference: new Gun("CN 105 G1", 105, 44.019047619, gunManufacturer, null)
            );
        }



        #endregion Logic




        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public FrenchTankFactory()
        {
        }



        #endregion Construction

    }
}
