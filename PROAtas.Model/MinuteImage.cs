using SQLite;

namespace PROAtas.Model
{
    public class MinuteImage
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        [Ignore]
        public byte[] ImageBytes { get; set; }
    }
}
