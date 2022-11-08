using MainEntityProject.Model.Entities;

namespace MainEntityProject.Model.Configs
{
    /// <summary>
    /// Fluent builder configuration for the 'Author' class.
    /// <br />
    /// Конфигурация класса "Автор" для Fluent Builder.
    /// </summary>
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {


        /// <summary>
        /// Configure entity.
        /// <br />
        /// Конфигурировать сущность.
        /// </summary>
        public void Configure(EntityTypeBuilder<Author> authorBuilder)
        {
            authorBuilder.HasKey(author => author.Id);
            authorBuilder.Property(author => author.Name);
            authorBuilder.Property(author => author.WebUrl);
        }


    }
}
