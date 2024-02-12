using DnDesigner.Data;
using DnDesigner.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Web;

namespace DnDesigner.Views.Verify
{
    public class VerifyController : Controller
    {
        private readonly DnDesignerDbContext _context;

        public VerifyController(DnDesignerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string url = HttpContext.Request.GetDisplayUrl();
            var uri = new Uri(url);
            var queryDict = QueryHelpers.ParseQuery(uri.Query);
            string token = queryDict["token"];

            if (token != null)
            {
                List<string> unverifiedUsers = _context.Users.Where(u => u.EmailConfirmed == false).Select(u => u.Id).ToList();
                foreach (string user in unverifiedUsers)
                {
                    if (user != null && token == Hash.CalculateSHA256Hash(user)) 
                    { 
                        IdentityUser identityUser = _context.Users.Find(user);
                        identityUser.EmailConfirmed = true;
                        ViewBag.Message = "Your email has been verified.";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            // If the token is not valid, redirect to the error page
            return RedirectToAction("Index", "Error");
        }
    }
}
