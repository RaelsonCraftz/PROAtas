using Foundation;
using PROAtas.iOS.Services;
using PROAtas.Mobile.Services.Platform;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageServiceIOS))]
namespace PROAtas.iOS.Services
{
    public class ImageServiceIOS : IImageService
    {
        public string DirectoryPath { get; set; }

        public ImageServiceIOS()
        {

        }

        public bool CreateDirectory()
        {
            //Get the root path in iOS device.
            string root = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            DirectoryPath = $"{root}/Imagens";

            Directory.CreateDirectory(DirectoryPath);
            var success = Directory.Exists(DirectoryPath);

            Debug.WriteLine($"Was the image directory created? {success}");

            return success;
        }

        public byte[] GetBytesFromLogo()
        {
            var logo = UIImage.FromFile("icLogo.png");
            
            using (NSData imageData = logo.AsPNG())
            {
                byte[] bytes = new byte[imageData.Length];
                Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));

                return bytes;
            }
        }

        public byte[] GetBytesFromStream(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Position = 0;
                imageStream.CopyTo(memoryStream);

                return memoryStream.ToArray();
            }
        }

        public byte[] GetBytesFromPath(string imageName)
        {
            var image = UIImage.FromFile(Path.Combine(DirectoryPath, $"{imageName}.png"));

            throw new System.NotImplementedException();
        }

        public ImageSource GetImageFromFile(string imageName)
        {
            // Get the file from path
            var path = Path.Combine(DirectoryPath, $"{imageName}.png");

            // Return Image Source
            return ImageSource.FromFile(path);
        }

        public Task SaveImageToDirectory(ImageSource imageSource, string imageName)
        {
            throw new System.NotImplementedException();
        }

        public Task<Stream> GetImageFromGalleryAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}