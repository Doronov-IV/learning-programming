namespace MainEntityProject.Model.Configs
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {

        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Budget> budgetBuilder)
        {
            budgetBuilder.HasKey(b => b.Id);
            budgetBuilder.Property(b => b.Value);
            budgetBuilder.Property(b => b.Currency);
            budgetBuilder.HasOne(b => b.ManufacturerReference).WithOne(m => m.BudgetReference).HasForeignKey<Manufacturer>(x => x.BudgetId);
        }

    }
}
