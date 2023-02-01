namespace TankFactory.Model.Configs
{
    public class GunConfiguration : IEntityTypeConfiguration<Gun>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Gun> gunBuilder)
        {
            gunBuilder.HasKey(g => g.Id);

            gunBuilder.Property(g => g.CaliberMillimetres);
            gunBuilder.Property(g => g.LengthInCalibers);

            gunBuilder.HasOne<Price>(e => e.PriceReference);
            gunBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference);
        }

    }
}
