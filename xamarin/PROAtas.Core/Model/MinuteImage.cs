using PROAtas.Core.Base;
using SQLite;

namespace PROAtas.Core
{
    public class MinuteImage : Entity
    {
        public MinuteImage() { }

        public MinuteImage(MinuteImage model)
        {
            Id = model.Id;

            Name = model.Name;
            ImageBytes = model.ImageBytes;
        }

        public string Name { get; set; }

        [Ignore]
        public byte[] ImageBytes { get; set; }
    }
}
