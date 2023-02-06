using Spectre.Console;

namespace MainEntityProject.Generation.TankFactories
{
    public class SwedishTankFactory : ITankFactory
    {


        #region API



        public MainBattleTank CreateImportedTank()
        {
            return CreateStrv122A();
        }

        public MainBattleTank CreateNativeTank()
        {
            return CreateStrv103A();
        }



        #endregion API




        #region Logic



        public MainBattleTank CreateStrv122A()
        {
            GermanTankFactory germanTankFactory = new();

            var svTank = germanTankFactory.CreateNativeTank();
            svTank.ModelName = "Strv 122A";

            return svTank;
        }



        public MainBattleTank CreateStrv103A()
        {
            var strv103EngineManufactorer = new Manufacturer("Detroit Diesel Corporation", "U.S.A.", null);
            var bofors = new Manufacturer("Bofors", "Sweden", null);

            return new MainBattleTank(
                modelName: "Strv 103A",
                crewCount: 3,
                manufactorerReference: bofors,
                priceReference: null,
                engineReference: new Engine("Detroit diesel 6V53T", 290, strv103EngineManufactorer, null),
                gunReference: new Gun("L74 10,5 cm L/62", 105, 62, bofors, null)
            );
        }



        #endregion Logic




        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public SwedishTankFactory()
        {
        }



        #endregion Construction


    }
}
