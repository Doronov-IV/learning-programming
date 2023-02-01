using MainEntityProject.Model.Entities;

namespace MainEntityProject.Model.Configs
{
    /// <summary>
    /// Fluent builder configuration for the 'Book' class.
    /// <br />
    /// Конфигурация класса "Book" для Fluent Builder.
    /// </summary>
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {


        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Book> bookBuilder)
        {
            bookBuilder.HasKey(book => book.Id);
            bookBuilder.Property(book => book.Title).IsRequired();
            bookBuilder.Property(book => book.Description).IsRequired();

            bookBuilder.HasOne(b => b.Author).WithMany(a => a.BookList).HasForeignKey(b => b.AuthorId);
        }


    }
}
