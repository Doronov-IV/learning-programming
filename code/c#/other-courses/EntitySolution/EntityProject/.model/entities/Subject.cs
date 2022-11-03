namespace MainEntityProject.Model.Entities
{
    public class Subject
    {

        /*
            Добавить таблицу Subject[s](Предмет). Связать ее с таблицей Cards 
        отношением многие ко многим - карта может иметь много предметов, 
        каждый предмет может быть привязан к множеству карт
        */


        public int Id { get; set; }


        public string Name { get; set; } = null!;

        
        public int? CardId { get; set; }


        public List<Card>? CardList { get; set; }



        /// <summary>
        /// Default constructor;
        /// <br />
        /// Конструктор по умолчанию;
        /// </summary>
        public Subject()
        {
            CardList = new();
        }
    }
}
