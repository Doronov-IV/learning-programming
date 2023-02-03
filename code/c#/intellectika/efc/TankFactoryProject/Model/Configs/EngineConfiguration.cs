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

            engineBuilder.HasOne<Price>(e => e.PriceReference).WithMany().HasForeignKey(e => e.PriceId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);
            engineBuilder.HasOne<Manufacturer>(e => e.ManufacturerReference).WithMany().HasForeignKey(e => e.ManufacturerId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
        }

    }
}
