using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    public class BayiController : Controller
    {
        private readonly IBayiService bayiService;

        public BayiController(IBayiService bayiService)
        {
            this.bayiService = bayiService;
        }


        public ActionResult BayiIslem()
        {

            return View();
        }

        public async Task<ActionResult> GetSehirler(int sehirId)
        {
            var data = await bayiService.GetIlceList(sehirId);
            return PartialView("_GetIlceler", data);
        }


        public async Task<ActionResult> GetBayiListesi()
        {
            var data = await bayiService.GetBayiList();
            return PartialView("_BayiListesi", data);
        }
        public async Task<ActionResult> BayiOlustur(BayiAddViewModel model)
        {
            var data = await bayiService.BayiOlustur(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> BayiGuncelle(BayiUpdateViewModel model)
        {
            var data = await bayiService.BayiGüncelle(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> BayiSil(int Id)
        {
            var data = await bayiService.BayiSil(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetBayiBilgiEkran(int? Id)
        {
            var data = await bayiService.GetBayiBilgi(Id);
            var sehirlerIlceler = await bayiService.GetSehirler();
            ViewBag.sehirler = sehirlerIlceler.Item1;
            ViewBag.ilceler = sehirlerIlceler.Item2;
            return PartialView("_GetBayiBilgiEkran", data);
        }
        public async Task<ActionResult> GetBayiAdres(int Id)
        {
            var data = await bayiService.GetBayiAdresi(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}