using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    public class EmailController : Controller
    {
        private readonly IEmailService emailService;

        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }


        public ActionResult EmailIslem()
        {
            return View();
        }

        public async Task<ActionResult> GetEMailAdress(int Id)
        {
            var data = await emailService.GetEMailAdress(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> EmailList()
        {
            var data =await emailService.EmailList();
            return PartialView("_EmailList", data);
        }

        public async Task<ActionResult> EmailOlustur(EmailAddViewModel model)
        {
            var data = await emailService.EmailOlustur(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> EmailGonder(EmailSendViewModel model)
        {
            var data = await emailService.EmailGonder(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

    }
}