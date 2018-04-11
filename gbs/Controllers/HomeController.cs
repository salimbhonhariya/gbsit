using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gbs.Models;
using MailKit.Net.Pop3;
using Email;
using Microsoft.AspNetCore.Http;

namespace gbs.Controllers
{
    public class HomeController : Controller
    {
        IEmailService _emailService;
        IEmailConfiguration _emailConfiguration;
        private string fileExtension;

        public HomeController(IEmailService emailService , IEmailConfiguration emailConfiguration)
        {
            _emailService = emailService;
            _emailConfiguration = emailConfiguration;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Architecture()
        {
            return View();
        }
        public IActionResult WDevelopment()
        {
            return View();
        }
        public IActionResult CloudMigration()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewData["Message"] = "About us.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact us.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult QA()
        {
            ViewData["Message"] = "Our QA testers have years of experience in the following tests.";

            return View("Views/Home/QualityAssurance.cshtml");
        }

        public FileResult VarmaProfile()
        {
            ViewData["Message"] = "Chief MuleSoft Architect Profile.";
            string f1 = "SrihariVarma_Profile.doc";
            return Download(f1);
        }

        public FileResult SalimProfile()
        {
            ViewData["Message"] = "Chief Azure Architect Profile.";
            string f2 = "SalimBhonhariya_Profile.doc";
            return Download(f2);
        }

        public FileResult MubinaProfile()
        {
            ViewData["Message"] = "Chief Quality Testing Architect Profile.";
            string f3 = "MubinaSalim_Profile.docx";
            return Download(f3);
        }

        public FileResult Download(string fileName)
        {
            var filepath = $"Docs/{fileName}";
            byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
            return File(fileBytes, "application/x-msdownload", fileName);
        }

        public string ResolveLocation()
        {
            // Check the User Agent
            var agent = Request.Headers["User-Agent"].ToString();

            // Check if IOS
            if ((agent.Contains("ios")) || (agent.Contains("android")))
            {
                // It is an IOS device, redirect to the specified location here
                fileExtension = ".pdf";
            }
            else
            {
                // It is an Android device, redirect to the specified location here
                fileExtension = ".doc";
            }
            return fileExtension;
        }
        [HttpPost]
        public ActionResult SendEmail(IFormCollection  collection)
        {
            string first_name = collection["first_name"];
            string last_name = collection["last_name"];
            string email = collection["email"];
            string firstname = collection["first_name"];
            string phone = collection["phone"];
            string company = collection["company"];
            string comment = collection["comment"];

            if (!string.IsNullOrEmpty(company))
            {
                if (company.Contains("yes"))
                {
                    company = "Message from Company";
                }
                else if (company.Contains("no"))
                {
                    company = "Message from Individual";
                }
            }
            else
            {
                company = "From Company or Individual email";
            }

            EmailMessage msg = new EmailMessage
            {
                FirstName = first_name,
                LastName = last_name,
                phoneNumber = phone,
                Content = comment,
                FromAddresses = email,
                ToAddresses = _emailConfiguration.PopUsername,
                Subject = company
            };
            if (!_emailService.Send(msg))
            {

            }

            return View("ThanksforContactingUs");

        }

        //public FileResult VarmaProfile()
        //{
        //    ViewData["Message"] = "Chief MuleSoft Architect Profile.";
        //    string extension = ResolveLocation();
        //    string f1 = "SrihariVarma_Profile" + extension;
        //    return Download(f1);
        //}
        //public FileResult SalimProfile()
        //{
        //    ViewData["Message"] = "Chief Azure Architect Profile.";
        //    string extension = ResolveLocation();
        //    string f2 = "SrihariVarma_Profile" + extension;
        //    return Download(f2);
        //}
        //public FileResult MubinaProfile()
        //{
        //    ViewData["Message"] = "Chief Quality Testing Architect Profile.";
        //    string extension = ResolveLocation();
        //    string f3 = "SrihariVarma_Profile" + extension;
        //    return Download(f3);
        //}

        //public FileResult Download(string fileName)
        //{
        //    var filepath = $"Docs/{fileName}";
        //    byte[] fileBytes = System.IO.File.ReadAllBytes(filepath);
        //    return File(fileBytes, "application/x-msdownload", fileName);
        //}
    }
}
