using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core.Model.Entities
{
    public class Person : Entity
    {
        public Person() { }

        public Person(Person model)
        {
            Id = model.Id;

            Name = model.Name;
            Order = model.Order;

            IdMinute = model.IdMinute;
        }

        public string Name { get; set; }
        public int Order { get; set; }

        public string IdMinute { get; set; }
    }
}
