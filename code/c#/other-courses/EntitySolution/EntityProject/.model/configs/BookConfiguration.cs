using MainEntityProject.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainEntityProject.Model.Configs
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {

        public void Configure(EntityTypeBuilder<Book> bookBuilder)
        {
            bookBuilder.HasKey(book => book.Id);
            bookBuilder.Property(book => book.Title).IsRequired();
            bookBuilder.Property(book => book.Description).IsRequired();

            bookBuilder.HasOne(b => b.Author).WithMany(a => a.BookList).HasForeignKey(b => b.AuthorId);
        }


    }
}
