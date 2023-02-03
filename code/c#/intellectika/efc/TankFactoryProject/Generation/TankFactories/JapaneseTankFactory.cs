namespace MainEntityProject.Generation.TankFactories
{
    public class JapaneseTankFactory : ITankFactory
    {

        #region API



        public MainBattleTank CreateImportedTank()
        {
            return CreateType90();
        }

        public MainBattleTank CreateNativeTank()
        {
            return CreateType74();
        }



        #endregion API




        #region Logic



        public MainBattleTank CreateType74()
        {
            var gunManufacturer = new Manufacturer("Watervliet Arsenal", "U.S.A.", null);
            var engineManufacturer = new Manufacturer("Mitsubishi Heavy Industries", "Japan", new Budget(3_860_000_000, "¥"));

            return new MainBattleTank(
                modelName: "Type 74",
                crewCount: 4,
                manufactorerReference: gunManufacturer,
                priceReference: new Price(375_000_000, "¥"),
                engineReference: new Engine("Mitsubishi 10ZF Model 21", 750, engineManufacturer, null),
                gunReference: new Gun("M68", 105, 52.8571428571, gunManufacturer, null)
            );

        }



        public MainBattleTank CreateType90()
        {
            GermanTankFactory gtf = new();
            var leopardReference = gtf.CreateNativeTank();
            var rh120GunReference = leopardReference.GunReference;

            var engineManufacturer = new Manufacturer("Mitsubishi Heavy Industries", "Japan", new Budget(3_860_000_000, "¥"));

            return new MainBattleTank(
                modelName: "Type 90 tank",
                crewCount: 3,
                manufactorerReference: engineManufacturer,
                priceReference: new Price(790_000_000, "¥"),
                engineReference: new Engine("Mitsubishi 10ZG", 1500, engineManufacturer, null),
                gunReference: rh120GunReference
            );
        }



        #endregion Logic




        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public JapaneseTankFactory()
        {
        }



        #endregion Construction

    }
}
