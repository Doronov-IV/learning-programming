namespace EntityHomeworkThird.Model.Entities
{
    public class Mark
    {


        /*
            Добавить таблицу Marks(Оценки). Связать ее с таблицей Subject связью 
        многие к одному - многие оценки могут ссылаться на один предмет.


            Связать таблицу Marks с таблицей Cards связью многие к одному - 
        в одной карте может быть много оценок

        */


        public int Id { get; set; }


        public byte Value { get; set; }


        public int SubjectId { get; set; }


        public int CardId { get; set; }


        public Subject? Subject { get; set; }


        public Card Card { get; set; } = null!;

    }
}
