using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    public class IcerikController : Controller
    {
        private readonly ISayfaIcerikService sayfaIcerikService;

        public IcerikController(ISayfaIcerikService sayfaIcerikService)
        {
            this.sayfaIcerikService = sayfaIcerikService;
        }

        public async Task<ActionResult> SayfaIcerikIslem()
        {
            ViewBag.sayfalar =await sayfaIcerikService.GetSayfaList();
            return View();
        }

        public async Task<ActionResult> GetSayfaIcerikListesi(int? sayfaId)
        {
            IList<Icerikler> list = null;
            if (sayfaId!=null)
            {
                list = await sayfaIcerikService.GetIcerikList(sayfaId);
            }

            return PartialView("_GetSayfaIcerikListesi", list);

        }
        public async Task<ActionResult> SayfaIcerikGuncelle(SayfaIcerikUpdateViewModel model)
        {
            var data = await sayfaIcerikService.SayfaIcerikGuncelle(model);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetText(int Id)
        {
            var data = await sayfaIcerikService.GetText(Id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }







    }
}