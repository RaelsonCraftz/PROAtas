using Android.Content;
using Android.Graphics;
using Java.IO;
using PROAtas.Droid.Services;
using PROAtas.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ImageServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class ImageServiceDroid : IImageService
    {
        public string DirectoryPath { get; set; }

        public ImageServiceDroid()
        {

        }

        public bool CreateDirectory()
        {
            string root;
            if (Android.OS.Environment.IsExternalStorageEmulated)
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DirectoryPath = $"{root}/Imagens";

            Java.IO.File imageDir = new Java.IO.File(DirectoryPath);
            var success = imageDir.Mkdir();

            Debug.WriteLine($"Was the image directory created? {success}");

            return success;
        }

        public byte[] GetBytesFromLogo()
        {
            //Get the file from resources
            Bitmap bitmap = BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.icLogo);

            //Convert stream as bytes
            using (var imageStream = new MemoryStream())
            {
                imageStream.Position = 0;
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, imageStream);
                byte[] imageInByte = imageStream.ToArray();

                return imageInByte;
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
            // Get the file from path
            var path = System.IO.Path.Combine(DirectoryPath, $"{imageName}.png");
            Bitmap bitmap = BitmapFactory.DecodeFile(path);

            // Convert stream as bytes
            using (var imageStream = new MemoryStream())
            {
                imageStream.Position = 0;
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, imageStream);
                byte[] imageInByte = imageStream.ToArray();
                return imageInByte;
            }
        }

        public async Task SaveImageToDirectory(ImageSource imageSource, string imageName)
        {
            var image = await GetImageFromImageSource(imageSource, Android.App.Application.Context);
            var path = System.IO.Path.Combine(DirectoryPath, $"{imageName}.png");

            using (var outs = new FileOutputStream(path))
            {
                using (var ms = new MemoryStream())
                {
                    ms.Position = 0;

                    image.Compress(Bitmap.CompressFormat.Png, 100, ms);
                    outs.Write(ms.ToArray());
                }
            }
        }

        public Task<Stream> GetImageFromGalleryAsync()
        {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Start the picture-picker activity (resumes in MainActivity.cs)
            MainActivity.Instance.StartActivityForResult(
                Intent.CreateChooser(intent, "Selecione Imagem"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a GalleryActivity property
            MainActivity.Instance.PickImageTaskCompletionSource = new TaskCompletionSource<Stream>();

            // Return Task object
            return MainActivity.Instance.PickImageTaskCompletionSource.Task;
        }

        #region Helpers

        private async Task<Bitmap> GetImageFromImageSource(ImageSource imageSource, Context context)
        {
            IImageSourceHandler handler;

            if (imageSource is FileImageSource)
            {
                handler = new FileImageSourceHandler();
            }
            else if (imageSource is StreamImageSource)
            {
                handler = new StreamImagesourceHandler();
            }
            else if (imageSource is UriImageSource)
            {
                handler = new ImageLoaderSourceHandler();
            }
            else
            {
                throw new NotImplementedException();
            }

            var originalBitmap = await handler.LoadImageAsync(imageSource, context);

            return originalBitmap;
        }

        #endregion
    }
}