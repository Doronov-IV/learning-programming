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

            mbtBuilder.HasOne<Price>(e => e.PriceReference).WithOne().HasForeignKey<MainBattleTank>(mbt => mbt.PriceId);
            mbtBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference).WithOne().HasForeignKey<MainBattleTank>(mbt => mbt.ManufacturerId);
            mbtBuilder.HasOne<Engine>(mbt => mbt.EngineReference).WithOne().HasForeignKey<MainBattleTank>(mbt => mbt.EngineId);
            mbtBuilder.HasOne<Gun>(mbt => mbt.GunReference).WithOne().HasForeignKey<MainBattleTank>(mbt => mbt.GunId);
        }
    }
}
