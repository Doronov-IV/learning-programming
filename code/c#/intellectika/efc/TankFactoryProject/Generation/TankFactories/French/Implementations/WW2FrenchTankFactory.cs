namespace MainEntityProject.Generation.TankFactories.Implementations
{
    public class WW2FrenchTankFactory : ITankFactory
    {

        public MainBattleTank CreateNativeTank()
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


        public MainBattleTank CreateImportedTank()
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
    }
}
