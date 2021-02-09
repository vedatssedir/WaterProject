using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    public class SiparisController : Controller
    {
        private readonly ISiparisService siparisService;

        public SiparisController(ISiparisService siparisService)
        {
            this.siparisService = siparisService;
        }

        // GET: Panel/Siparis
        public async Task<ActionResult> SiparisIslem()
        {
            ViewBag.sehirler = await siparisService.GetSehirler();
            return View();
        }

        public async Task<ActionResult> SiparisEkle(SiparisAddViewModel model)
        {
            var result = await siparisService.SiparisEkle(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SiparisListesi(int? page)
        {
            var data = siparisService.GetSiparisListe(page);
            ViewBag.RecodNo = data.Item2;
            ViewBag.sehirId = "";
            ViewBag.siparistarihi = "";
            ViewBag.SiparisDurum = siparisService.GetDurumBilgisi();
            return PartialView("_SiparisListesi", data);
        }


        public ActionResult SiparisAra(int? page, string sehirId, string siparisTarih)
        {
            var data = siparisService.SiparisAra(page, sehirId, siparisTarih);
            ViewBag.RecodNo = data.Item2;
            ViewBag.sehirId = sehirId;
            ViewBag.siparistarihi = siparisTarih;
            ViewBag.SiparisDurum = siparisService.GetDurumBilgisi();
            return PartialView("_SiparisListesi", data);
        }



      

        public async Task<ActionResult> GetIlceler(int sehirId)
        {
            var data = await siparisService.GetIlceBilgi(sehirId);
            return PartialView("_GetIlceler", data);
        }


        public async Task<ActionResult> GetBayiBilgi(int ilceId)
        {
            var data = await siparisService.GetBayiBilgi(ilceId);
            return PartialView("_GetBayiBilgi", data);
        }


        public async Task<ActionResult> PanelDurumBildir(int siparisId,int durumId)
        {
            var data = await siparisService.PanelSiparisMailGonder(siparisId, durumId);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetAciklamaMetin(int Id)
        {
            var data = await siparisService.GetAciklamaMetin(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAdresMetin(int Id)
        {
            var data = await siparisService.GetAdresMetin(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


    }
}