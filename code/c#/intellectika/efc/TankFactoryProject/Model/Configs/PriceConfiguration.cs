namespace TankFactory.Model.Configs
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Price> priceBuilder)
        {
            priceBuilder.HasKey(b => b.Id);
            priceBuilder.Property(b => b.Value);
            priceBuilder.Property(b => b.Currency);
        }
    }
}
