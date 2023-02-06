using MainEntityProject.Generation.TankFactories.Implementations;

namespace MainEntityProject.Generation.TankFactories
{
    public class FrenchTankFactory : ITankFactory
    {

        public ITankFactory Dependency { get; set; }


        #region API



        public MainBattleTank CreateImportedTank()
        {
            return Dependency.CreateImportedTank();
        }

        public MainBattleTank CreateNativeTank()
        {
            return Dependency.CreateNativeTank();
        }



        #endregion API




        #region Construction



        /// <summary>
        /// Default constructor.
        /// <br />
        /// Конструктор по умолчанию.
        /// </summary>
        public FrenchTankFactory()
        {
        }



        /// <summary>
        /// Dependency constructor.
        /// <br />
        /// Конструктор для зависимости.
        /// </summary>
        public FrenchTankFactory(ITankFactory dependency)
        {
            if (dependency.GetType() != this.GetType())
                Dependency = dependency;
        }






        #endregion Construction

    }
}
