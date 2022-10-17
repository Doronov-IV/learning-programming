using EntityHomeworkThird.Model.Entities;

namespace EntityHomeworkThird.Model.Configs
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {


        // У карты студента должны быть ID, SerialNumber;


        public void Configure(EntityTypeBuilder<Card> cardBuilder)
        {
            cardBuilder.HasKey(card => card.Id);
            cardBuilder.Property(card => card.SerialNumber).IsRequired();
        }


    }
}
