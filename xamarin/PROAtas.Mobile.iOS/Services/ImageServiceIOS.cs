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
            //Get the root path in iOS device.
            string root = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            DirectoryPath = $"{root}/Imagens";
        }

        private TaskCompletionSource<Stream> _taskCompletionSource;
        private UIImagePickerController _imagePicker;

        public bool CreateDirectory()
        {
            Directory.CreateDirectory(DirectoryPath);
            var success = Directory.Exists(DirectoryPath);

            Debug.WriteLine($"Was the image directory created? {success}");

            return success;
        }

        public byte[] GetBytesFromLogo()
        {
            var logo = UIImage.FromBundle("icLogo");
            
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

            using (NSData imageData = image.AsPNG())
            {
                byte[] bytes = new byte[imageData.Length];
                Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));

                return bytes;
            }
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
            // Create and define UIImagePickerController
            _imagePicker = new UIImagePickerController
            {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary),
            };

            // Set event handlers
            _imagePicker.FinishedPickingMedia += OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentViewController(_imagePicker, true, null);

            // Return Task object
            _taskCompletionSource = new TaskCompletionSource<Stream>();

            // Remember to dispose the stream within the task
            return _taskCompletionSource.Task;
        }

        #region Helpers

        private void OnImagePickerFinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            UIImage image = e.EditedImage ?? e.OriginalImage;

            if (image == null)
            {
                UnregisterEventHandlers();
                _taskCompletionSource.SetResult(null);
            }

            // Convert UIImage to .NET Stream object
            NSData data = e.ReferenceUrl.PathExtension.Equals("PNG") || e.ReferenceUrl.PathExtension.Equals("png")
                ? image.AsPNG()
                : image.AsJPEG(1);

            // Remember to dispose this at the end!!
            Stream stream = data.AsStream();

            // Prevent memory leaks
            UnregisterEventHandlers();

            // Set the Stream as the completion of the Task
            _taskCompletionSource.SetResult(stream);
            _imagePicker.DismissModalViewController(true);
        }

        private void OnImagePickerCancelled(object sender, EventArgs e)
        {
            // Prevent memory leaks
            UnregisterEventHandlers();

            _taskCompletionSource.SetResult(null);
            _imagePicker.DismissModalViewController(true);
        }

        private void UnregisterEventHandlers()
        {
            _imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            _imagePicker.Canceled -= OnImagePickerCancelled;
        }

        #endregion
    }
}