using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core.Model.Entities
{
    public class MinuteImage : Entity
    {
        public MinuteImage() { }

        public MinuteImage(MinuteImage model)
        {
            Id = model.Id;

            Name = model.Name;
        }

        public string Name { get; set; }
    }
}
