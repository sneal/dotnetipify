using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIpify
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ServerIP = GetServerPublicIP();
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
    }
}