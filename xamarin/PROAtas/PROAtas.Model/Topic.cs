using PROAtas.Model.Base;

namespace PROAtas.Model
{
    public class Topic : Entity
    {
        public int Order { get; set; }
        public string Text { get; set; }

        public int IdMinute { get; set; }
    }
}
