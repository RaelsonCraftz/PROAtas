using Android.Content;
using Android.Graphics;
using PROAtas.Droid.Activities;
using PROAtas.Droid.Services;
using PROAtas.Services;
using System;
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
            string root;
            if (Android.OS.Environment.IsExternalStorageEmulated)
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            DirectoryPath = $"{root}/Imagens";


            Java.IO.File imageDir = new Java.IO.File(DirectoryPath);
            imageDir.Mkdir();
        }

        public byte[] GetFileFromDrawable()
        {
            //Get the file
            Bitmap bitmap = BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, Resource.Drawable.icLogo);
            MemoryStream imageStream = new MemoryStream();
            //Convert stream as bytes
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, imageStream);
            byte[] imageInByte = imageStream.ToArray();
            return imageInByte;
        }

        public Task<Stream> GetImageStreamAsync()
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