namespace MainEntityProject.Model.Configs
{
    public class MainBattleTankConfiguration : IEntityTypeConfiguration<MainBattleTank>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<MainBattleTank> mbtBuilder)
        {
            mbtBuilder.HasKey(g => g.Id);

            mbtBuilder.Property(mbt => mbt.CrewCount);

            mbtBuilder.HasOne<Price>(e => e.PriceReference);
            mbtBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference);
            mbtBuilder.HasOne<Engine>(mbt => mbt.EngineReference);
            mbtBuilder.HasOne<Gun>(mbt => mbt.GunReference);
        }
    }
}
