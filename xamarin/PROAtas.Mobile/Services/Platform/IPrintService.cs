using System.IO;

namespace PROAtas.Mobile.Services.Platform
{
    public interface IPrintService
    {
        void Print(string filename, string contentType, MemoryStream stream);
    }
}
