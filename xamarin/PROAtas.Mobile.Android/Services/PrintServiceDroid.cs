using Android.Content;
using AndroidX.Core.Content;
using Java.IO;
using PROAtas.Droid.Services;
using PROAtas.Droid.Views;
using PROAtas.Mobile.Services.Platform;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Forms;
using AppInfo = Xamarin.Essentials.AppInfo;

[assembly: Dependency(typeof(PrintServiceDroid))]
namespace PROAtas.Droid.Services
{
    public class PrintServiceDroid : IPrintService
    {
        public void Print(string fileName, string contentType, MemoryStream stream)
        {
            string root;
            if (Android.OS.Environment.IsExternalStorageEmulated)
                root = Android.OS.Environment.ExternalStorageDirectory.ToString();
            else
                root = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            Java.IO.File myDir = new Java.IO.File($"{root}/Atas");
            myDir.Mkdir();

            Java.IO.File file = new Java.IO.File(myDir, fileName);
            if (file.Exists())
                file.Delete();

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
                Debug.WriteLine($"[{AppInfo.Name}] Print error: {e.Message}\r\nStackTrace: {e.StackTrace}");
            }
            finally
            {
                stream.Dispose();
            }

            if (file.Exists())
            {
                string extension = Android.Webkit.MimeTypeMap.GetFileExtensionFromUrl(Android.Net.Uri.FromFile(file).ToString());
                string mimeType = Android.Webkit.MimeTypeMap.Singleton.GetMimeTypeFromExtension(extension);
                Intent intent = new Intent(Intent.ActionView);
                var contentUri = FileProvider.GetUriForFile(Android.App.Application.Context, Android.App.Application.Context.PackageName + ".fileprovider", file);
                intent.SetDataAndType(contentUri, mimeType);
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.NewTask);
                intent.AddFlags(ActivityFlags.ClearTop);
                MainActivity.Instance.StartActivity(intent);
            }
        }
    }
}