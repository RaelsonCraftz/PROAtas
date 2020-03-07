using SQLite;

namespace PROAtas.Model
{
    public class Information
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }

        public int IdTopic { get; set; }
    }
}
