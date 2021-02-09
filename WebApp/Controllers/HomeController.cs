using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Models.Interfaces;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService homeService;
        private readonly IUrunService urunService;

        public HomeController(IHomeService homeService, IUrunService urunService)
        {
            this.homeService = homeService;
            this.urunService = urunService;
        }

        public string ChangeCultureLang(string lang, string returnUrl)
        {
            if (lang != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            var cookie = new HttpCookie("Language") { Value = lang };
            HttpContext.Response.Cookies.Add(cookie);
            return returnUrl;
        }
        public async Task<ActionResult> Index(int? sayfaId=1)
        {
            ViewData["menu"] = "ANASAYFA";
            var data = await homeService.HomeList(sayfaId);

            return View(data);
        }

        public async Task<ActionResult> Kurumsal(int? sayfaId=2)
        {
            var data = await homeService.HomeList(sayfaId);
            return View(data);
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Belgeler()
        {
            return View();
        }
        public async Task<ActionResult> SiparisVer()
        {
            ViewBag.Urunler = await homeService.GetUrunListesi();
            ViewBag.Iller = await homeService.GetSehirListesi();
            ViewBag.ilceler = await homeService.GetIlceListesi();
            return View();
        }
        public async Task<ActionResult> Urunler()
        {
            var data = await urunService.UrunListesi();
            return View(data);
        }



        public async Task<ActionResult> UrunDetay(int urunId)
        {
            var data = await homeService.GetUrunDetay(urunId);
            return View(data);
        }


        public async Task<ActionResult> GetDetayKontrol(int urunId)
        {
            var data = await homeService.GetDetayKontrol(urunId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }
}