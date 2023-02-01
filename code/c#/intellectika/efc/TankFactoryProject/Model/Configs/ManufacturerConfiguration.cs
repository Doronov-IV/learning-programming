namespace MainEntityProject.Model.Configs
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Manufacturer> manufacturerBuilder)
        {
            manufacturerBuilder.HasKey(m => m.Id);

            manufacturerBuilder.Property(m => m.Name);
            manufacturerBuilder.Property(m => m.CountryName);

            manufacturerBuilder.HasOne(m => m.BudgetReference).WithOne(x => x.ManufacturerReference).HasForeignKey<Manufacturer>(m => m.BudgetId).OnDelete(DeleteBehavior.Cascade).IsRequired(false);

        }

    }
}
