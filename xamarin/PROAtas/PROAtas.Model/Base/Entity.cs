using SQLite;

namespace PROAtas.Model.Base
{
    public class Entity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }
}
