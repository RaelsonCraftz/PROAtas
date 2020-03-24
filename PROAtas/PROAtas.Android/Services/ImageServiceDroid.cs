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
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, imageStream);
                byte[] imageInByte = imageStream.ToArray();
                return imageInByte;
            }
        }

        public byte[] GetBytesFromStream(Stream imageStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                imageStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public byte[] GetBytesFromPath(string imageName)
        {
            // Get the file from path
            Bitmap bitmap = BitmapFactory.DecodeFile($"{DirectoryPath}/{imageName}");

            // Convert stream as bytes
            using (var imageStream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, imageStream);
                byte[] imageInByte = imageStream.ToArray();
                return imageInByte;
            }
        }

        public async Task SaveImageToDirectory(Stream imageStream, string imageName)
        {
            using (var stream = new MemoryStream())
            {
                await imageStream.CopyToAsync(stream);

                // Creating the file
                Java.IO.File myDir = new Java.IO.File(DirectoryPath);
                myDir.Mkdir();
                Java.IO.File file = new Java.IO.File(myDir, $"{imageName}");
                if (file.Exists())
                    file.Delete();

                // Saving the content
                try
                {
                    FileOutputStream outs = new FileOutputStream(file);
                    stream.Position = 0;
                    outs.Write(stream.ToArray());
                    outs.Flush();
                    outs.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine($"Save image error: {e.Message}");
                }
                finally
                {
                    stream.Dispose();
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
    }
}