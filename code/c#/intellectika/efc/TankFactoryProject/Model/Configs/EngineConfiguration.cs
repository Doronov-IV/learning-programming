namespace MainEntityProject.Model.Configs
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Engine> engineBuilder)
        {
            engineBuilder.HasKey(e => e.Id);

            engineBuilder.Property(e => e.HorsePowers);

            engineBuilder.HasOne<Price>(e => e.PriceReference).WithOne().HasForeignKey<Engine>(e => e.PriceId);
            engineBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference).WithOne().HasForeignKey<Engine>(e => e.ManufacturerId);
        }

    }
}
