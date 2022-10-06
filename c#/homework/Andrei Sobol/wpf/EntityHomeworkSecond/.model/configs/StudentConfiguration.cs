using EntityHomeworkSecond.Model.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityHomeworkSecond.Model.Configs
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {


        // У студента должны быть поля Id, FirstName, LastName, Birthday, PhoneNumber(Nullable);


        public void Configure(EntityTypeBuilder<Student> studentBuilder)
        {
            studentBuilder.HasKey(stu => stu.Id);
            studentBuilder.Property(stu => stu.FirstName).HasMaxLength(24);
            studentBuilder.Property(stu => stu.LastName).HasMaxLength(24);
            studentBuilder.Property(stu => stu.Birthay).HasMaxLength(10);
            studentBuilder.Property(stu => stu.PhoneNumber).HasMaxLength(14).IsRequired();

            studentBuilder.HasOne(s => s.Card).WithOne(c => c.Student);
        }


    }
}
