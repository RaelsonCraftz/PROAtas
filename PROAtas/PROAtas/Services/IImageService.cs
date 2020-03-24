using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface IImageService
    {
        bool CreateDirectory();
        byte[] GetBytesFromLogo();
        byte[] GetBytesFromStream(Stream imageStream);
        byte[] GetBytesFromPath(string imageName);
        Task SaveImageToDirectory(Stream imageStream, string imageName);
        Task<Stream> GetImageFromGalleryAsync();
    }
}
