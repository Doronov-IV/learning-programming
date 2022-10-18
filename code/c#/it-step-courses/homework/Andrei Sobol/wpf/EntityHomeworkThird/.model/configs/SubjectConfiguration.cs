using EntityHomeworkThird.Model.Entities;

namespace EntityHomeworkThird.Model.Configs
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {


        /*
        Добавить таблицу Subject[s](Предмет). Связать ее с таблицей Cards 
    отношением многие ко многим - карта может иметь много предметов, 
    каждый предмет может быть привязан к множеству карт
        */


        public void Configure(EntityTypeBuilder<Subject> subjectBuilder)
        {
            subjectBuilder.HasKey(sub => sub.Id);
            subjectBuilder.Property(sub => sub.Name).HasMaxLength(50);

            subjectBuilder.HasMany(card => card.CardList).WithMany(subj => subj.SubjectList);
            
        }
    }
}
