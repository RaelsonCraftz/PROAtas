using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core
{
    public class Minute : Entity<string>
    {
        public Minute() { }

        public Minute(Minute model)
        {
            Id = model.Id;

            Name = model.Name;
            Date = model.Date;
            Start = model.Start;
            End = model.End;
            Favorite = model.Favorite;
            Active = model.Active;

            PeopleQuantity = model.PeopleQuantity;
        }

        public string Name { get; set; }
        public string Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public bool Favorite { get; set; }
        public bool Active { get; set; }

        [Ignore]
        public int PeopleQuantity { get; set; }
    }
}
