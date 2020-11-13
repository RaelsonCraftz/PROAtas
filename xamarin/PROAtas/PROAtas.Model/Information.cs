using PROAtas.Model.Base;
using SQLite;

namespace PROAtas.Model
{
    public class Information : Entity
    {
        public int Order { get; set; }
        public string Text { get; set; }

        public int IdTopic { get; set; }
    }
}
