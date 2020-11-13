using System.IO;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface IPrintService
    {
        void Print(string filename, string contentType, MemoryStream stream);
    }
}
