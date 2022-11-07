using MainEntityProject.Model.Entities;

namespace MainEntityProject.Model.Configs
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {


        public void Configure(EntityTypeBuilder<Author> authorBuilder)
        {
            authorBuilder.HasKey(author => author.Id);
            authorBuilder.Property(author => author.Name);
            authorBuilder.Property(author => author.WebUrl);
        }


    }
}
