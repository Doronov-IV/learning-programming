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

            mbtBuilder.HasOne<Price>(e => e.PriceReference).WithMany().HasForeignKey(mbt => mbt.PriceId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            mbtBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference).WithMany().HasForeignKey(mbt => mbt.ManufacturerId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            mbtBuilder.HasOne<Engine>(mbt => mbt.EngineReference).WithMany().HasForeignKey(mbt => mbt.EngineId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            mbtBuilder.HasOne<Gun>(mbt => mbt.GunReference).WithMany().HasForeignKey(mbt => mbt.GunId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        }
    }
}
