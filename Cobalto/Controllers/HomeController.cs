using Cobalto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Cobalto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CorreoInfo(string email)
        {
            try
            {
                MailAddress addr = new MailAddress(email);
            }
            catch
            {
                return Json("El email no es válido"); ;
            }

            string emailFrom = "info@cobaltoagenciadigital.com";
            string emailTo = email;

            MailMessage msg = new MailMessage(emailFrom, emailTo);
            msg.Subject = "Info Cobalto Agencia Digital";
            msg.Body = "Aquí te mostraremos información acerca de como hacer crecer tu negocio";

            string[] smpt_credentials = new string[4] { "smtp.sendgrid.net", "587", "apikey", "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" };

            using (SmtpClient smtp_client = new SmtpClient(smpt_credentials[0].ToString(), Convert.ToInt32(smpt_credentials[1].ToString())))
            {
                smtp_client.Credentials = new NetworkCredential(smpt_credentials[2].ToString(), smpt_credentials[3].ToString());
                try
                {
                    smtp_client.Send(msg);
                    return Json("Exitoso");
                }
                catch (Exception)
                {
                    return Json("Fallido");
                }
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
