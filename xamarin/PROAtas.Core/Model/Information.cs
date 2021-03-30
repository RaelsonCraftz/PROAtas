using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core
{
    public class Information : Entity
    {
        public Information() { }

        public Information(Information model)
        {
            Id = model.Id;

            Order = model.Order;
            Text = model.Text;

            IdTopic = model.IdTopic;
        }

        public int Order { get; set; }
        public string Text { get; set; }

        public int IdTopic { get; set; }
    }
}
