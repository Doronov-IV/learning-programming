using Spectre.Console;

namespace MainEntityProject.Generation.TankFactories
{
    public class GermanTankFactory : ITankFactory
    {


        #region API



        public MainBattleTank CreateImportedTank()
        {
            return CreateT72M();
        }

        public MainBattleTank CreateNativeTank()
        {
            return CreateLeopard2A5();
        }



        #endregion API




        #region Logic



        public MainBattleTank CreateT72M()
        {
            var zavod = new Manufacturer("Uralgovnozavod", "U.S.S.R", null);

            return new MainBattleTank(
                modelName: "T-72M",
                crewCount: 3,
                manufactorerReference: zavod,
                priceReference: new Price(1_000_000, "$"),
                engineReference: new Engine("V46", 780, zavod, new Price(168_000, "₽")),
                gunReference: new Gun("2A46M", 125, 48, zavod, null)
            );

        }



        public MainBattleTank CreateLeopard2A5()
        {
            var leoEngineManufacturer = new Manufacturer("Renk", "Germany", new Budget(82_700_000, "€"));
            var rheinmetall = new Manufacturer("Rheinmetall", "Germany", new Budget(850_000_000, "€"));

            return new MainBattleTank(
                modelName: "Leopard 2A5",
                crewCount: 4,
                manufactorerReference: rheinmetall,
                priceReference: new Price(5_600_000, "$"),
                engineReference: new Engine("MTU MB 873 Ka-501", 1500, leoEngineManufacturer, new Price(650000, "€")),
                gunReference: new Gun("Rh120 L/44", 120, 44, rheinmetall, null)
            );
        }



        #endregion Logic




        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public GermanTankFactory()
        {
        }



        #endregion Construction


    }
}
