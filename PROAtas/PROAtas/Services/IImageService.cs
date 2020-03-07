using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface IImageService
    {
        string DirectoryPath { get; set; }
        byte[] GetFileFromDrawable();
        Task<Stream> GetImageStreamAsync();
    }
}
