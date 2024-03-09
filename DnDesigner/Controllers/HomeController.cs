using DnDesigner.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YourNamespace.Services;

namespace DnDesigner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult IndexAsync()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index()
        {
            // Call the SendEmailAsync function
            string toEmail = "";
            string subject = "Hello";
            string message = "This is a test email.";

            try
            {
                await EmailSender.SendEmailAsync(toEmail, subject, message);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to send email: {ex.Message}");
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}