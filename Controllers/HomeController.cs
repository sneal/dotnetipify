using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetIpify
{
  public class HomeController : Controller {
      public IActionResult Index()
      {
          var client = new HttpClient();
          client.DefaultRequestHeaders.Accept.Clear();
          var result = client.GetStringAsync("https://api.ipify.org?format=text");
          result.Wait(5000);
          ViewBag.ServerIP = result.Result;
          return View();
      }
  }
}