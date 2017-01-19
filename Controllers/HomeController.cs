using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIpify
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ServerIP = GetServerPublicIP();
            ViewBag.ClientIP = GetClientPublicIP();
            return View();
        }

        private string GetServerPublicIP()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            var result = client.GetStringAsync("https://api.ipify.org?format=text");
            if (result.Wait(5000))
            {
                return result.Result;
            }
            return "Unknown";
        }

        // Based on SO answer http://stackoverflow.com/a/36316189
        private string GetClientPublicIP()
        {
            string ip = SplitCsv(GetHeaderValue("X-Forwarded-For")).FirstOrDefault();
            if (string.IsNullOrWhiteSpace(ip) && HttpContext?.Connection?.RemoteIpAddress != null)
            {
                ip = HttpContext.Connection.RemoteIpAddress.ToString();
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = GetHeaderValue("REMOTE_ADDR");
            }
            if (string.IsNullOrWhiteSpace(ip))
            {
                ip = "Unknown";
            }
            return ip;
        }

        private string GetHeaderValue(string headerName)
        {
            StringValues values;
            if (HttpContext?.Request?.Headers?.TryGetValue(headerName, out values) ?? false)
            {
                return values.ToString();
            }
            return "";
        }

        private IList<string> SplitCsv(string csvList)
        {
            if (string.IsNullOrWhiteSpace(csvList))
            {
                return new List<string>();
            }

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable<string>()
                .Select(s => s.Trim())
                .ToList();
        }
    }
}