using PROAtas.Model.Base;
using SQLite;

namespace PROAtas.Model
{
    public class Person : Entity
    {
        public string Name { get; set; }
        public int Order { get; set; }

        public int IdMinute { get; set; }
    }
}
