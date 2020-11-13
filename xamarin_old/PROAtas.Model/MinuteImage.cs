using PROAtas.Model.Base;
using SQLite;

namespace PROAtas.Model
{
    public class MinuteImage : Entity
    {
        public string Name { get; set; }
        [Ignore]
        public byte[] ImageBytes { get; set; }
    }
}
