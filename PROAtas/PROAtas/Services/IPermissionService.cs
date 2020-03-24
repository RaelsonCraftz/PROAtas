using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace PROAtas.Services
{
    public interface IPermissionService
    {
        Task<bool> RequestStoragePermission();
    }

    public class PermissionService : IPermissionService
    {
        public async Task<bool> RequestStoragePermission()
        {
            var statusWrite = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            var statusRead = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (statusWrite != PermissionStatus.Granted)
                statusWrite = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (statusRead != PermissionStatus.Granted)
                statusRead = await Permissions.RequestAsync<Permissions.StorageRead>();

            return statusWrite == PermissionStatus.Granted && statusRead == PermissionStatus.Granted;
        }
    }
}
