using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PROAtas.Services
{
    public interface IImageService
    {
        bool CreateDirectory();
        byte[] GetBytesFromLogo();
        byte[] GetBytesFromStream(Stream imageStream);
        byte[] GetBytesFromPath(string imageName);
        ImageSource GetImageFromFile(string imageName);
        Task SaveImageToDirectory(ImageSource imageSource, string imageName);
        Task<Stream> GetImageFromGalleryAsync();
    }
}
