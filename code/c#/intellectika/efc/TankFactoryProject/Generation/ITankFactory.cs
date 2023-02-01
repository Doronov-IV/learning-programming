namespace MainEntityProject.Generation
{
    public interface ITankFactory
    {
        public MainBattleTank CreateNativeTank();

        public MainBattleTank CreateImportedTank();

    }
}
