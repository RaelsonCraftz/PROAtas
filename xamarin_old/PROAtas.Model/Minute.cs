using PROAtas.Model.Base;
using SQLite;

namespace PROAtas.Model
{
    public class Minute : Entity
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool Active { get; set; }

        [Ignore]
        public int PeopleQuantity { get; set; }
    }
}
