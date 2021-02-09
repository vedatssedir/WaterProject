using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{

    public class UrunController : Controller
    {
        private readonly IUrunService urunService;

        public UrunController(IUrunService urunService)
        {
            this.urunService = urunService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> UrunListesi()
        {
            var data = await urunService.UrunListesi();
            return PartialView("_UrunListesi", data);
        }
        public async Task<ActionResult> UrunEkle(ProductAddViewModel model)
        {
            var result = await urunService.UrunEkle(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> UrunGuncellemeEkran(int? id)
        {
            var model = await urunService.GetUrunBilgi(id);
            return PartialView("_UrunGuncellemeEkran", model);
        }


        public async Task<ActionResult> GetUrunDetayEkran(int? id)
        {
            var model = await urunService.GetUrunDetay(id);
            return PartialView("_GetUrunDetayEkran", model);
        }
        public async Task<ActionResult> GetUrunDetayGuncelleEkran(int? id)
        {
            var model = await urunService.GetUrunDetayGuncelle(id);
            return PartialView("_GetUrunDetayGuncelleEkran", model);
        }

        public async Task<ActionResult> UrunOlustur(UrunDetayAddViewModel model)
        {
            var data = await urunService.UrunDetayOlustur(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> UrunGuncelle(UrunGuncelleViewModel model)
        {
            var result = await urunService.UrunGuncelle(model);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> UrunSil(int? Id)
        {
            var result = await urunService.UrunSil(Id);
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}