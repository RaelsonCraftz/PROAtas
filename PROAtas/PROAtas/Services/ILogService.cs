using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PROAtas.Services
{
    public interface ILogService
    {
        string LogAction(Action action);
        Task<string> LogActionAsync(Task action);
        string LogRequest(Action action);
        Task<string> LogRequestAsync(Task action);
        void MeasureTime(string consoleMessage, Action action);
    }

    public class LogService : ILogService
    {
        public string LogAction(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Houve algum erro genérico: {ex.Message}");
                return "Houve algum erro genérico";
            }
        }

        public async Task<string> LogActionAsync(Task action)
        {
            try
            {
                await action;
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Houve algum erro genérico: {ex.Message}");
                return "Houve algum erro genérico";
            }
        }

        public string LogRequest(Action action)
        {
            try
            {
                action();
                return null;
            }
            catch (WebException exWeb)
            {
                if (exWeb.Response != null)
                    using (var stream = new StreamReader(exWeb.Response.GetResponseStream()))
                        Debug.WriteLine($"Houve um erro de conexão: {JsonConvert.DeserializeObject(stream.ReadToEnd())}");
                else
                    Debug.WriteLine($"Houve um erro de conexão: {exWeb.Message}");

                return "Houve um erro de conexão";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Houve algum erro genérico: {ex.Message}");
                return "Houve algum erro genérico";
            }
        }

        public async Task<string> LogRequestAsync(Task action)
        {
            try
            {
                await action;
                return null;
            }
            catch (WebException exWeb)
            {
                if (exWeb.Response != null)
                    using (var stream = new StreamReader(exWeb.Response.GetResponseStream()))
                        Debug.WriteLine($"Houve um erro de conexão: {JsonConvert.DeserializeObject(stream.ReadToEnd())}");
                else
                    Debug.WriteLine($"Houve um erro de conexão: {exWeb.Message}");

                return "Houve um erro de conexão";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Houve algum erro genérico: {ex.Message}");
                return "Houve algum erro genérico";
            }
        }

        public void MeasureTime(string consoleMessage, Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            action();
            stopWatch.Stop();

            Console.WriteLine($"{consoleMessage} {stopWatch.ElapsedMilliseconds} ms");
        }
    }
}
