using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core
{
    public class Topic : Entity
    {
        public Topic() { }

        public Topic(Topic model)
        {
            Id = model.Id;

            Order = model.Order;
            Text = model.Text;

            IdMinute = model.IdMinute;
        }

        [Ignore]
        public int Order { get; set; }
        public string Text { get; set; }

        public string IdMinute { get; set; }
    }
}
