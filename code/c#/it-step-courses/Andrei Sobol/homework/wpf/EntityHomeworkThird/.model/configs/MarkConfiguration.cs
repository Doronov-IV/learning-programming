using EntityHomeworkThird.Model.Entities;

namespace EntityHomeworkThird.Model.Configs
{
    public class MarkConfiguration : IEntityTypeConfiguration<Mark>
    {


        /*
            Добавить таблицу Marks(Оценки). Связать ее с таблицей Subject связью 
        многие к одному - многие оценки могут ссылаться на один предмет.


            Связать таблицу Marks с таблицей Cards связью многие к одному - 
        в одной карте может быть много оценок

        */

        public void Configure(EntityTypeBuilder<Mark> markBuilder)
        {
            markBuilder.HasKey(m => m.Id);
            markBuilder.Property(m => m.Value);

            markBuilder.HasOne(m => m.Card).WithMany(c => c.MarkList);
        }
    }
}
