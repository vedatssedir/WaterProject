using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    public class PanelGirisController : Controller
    {

        private readonly IAuthService authService;

        public PanelGirisController(IAuthService authService)
        {
            this.authService = authService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Siteler = await authService.SiteList();
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            var c = new HttpCookie("Login") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(c);
            Session.Clear();
            return View();
        }


        public async Task<ActionResult> Login(string username,string password,int siteId)
        {
            var result = await authService.GirisYap(username, password,siteId);
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult Logoff()
        {
            authService.LogOff();
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            var c = new HttpCookie("Login") { Expires = DateTime.Now.AddDays(-1) };
            Response.Cookies.Add(c);
            Session.Clear();
            return RedirectToAction("Index", "PanelGiris");
        }




    }
}