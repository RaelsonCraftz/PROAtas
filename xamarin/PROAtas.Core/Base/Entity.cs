using SQLite;

namespace PROAtas.Core.Base
{
    public class Entity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }

    public class Entity<T>
    {
        [PrimaryKey]
        public T Id { get; set; }
    }
}
