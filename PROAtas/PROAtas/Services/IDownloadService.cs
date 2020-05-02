using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface IDownloadService
    {
        Task<byte[]> DownloadImageUrl(string url);
    }

    public class DownloadService : IDownloadService
    {
        const int timeout = 15;
        readonly HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(timeout) };

        public async Task<byte[]> DownloadImageUrl(string url)
        {
            using (var response = await client.GetAsync(url))
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    return await response.Content.ReadAsByteArrayAsync();
                else
                    return null;
        }
    }
}
