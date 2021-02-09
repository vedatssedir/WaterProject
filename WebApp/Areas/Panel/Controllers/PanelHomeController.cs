using System.Threading.Tasks;
using System.Web.Mvc;
using DataLayer.DataModels;
using Models.Interfaces;

namespace WebApp.Areas.Panel.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PanelHomeController : Controller
    {

        private readonly IPanelHomeService panelHomeService;

        public PanelHomeController(IPanelHomeService panelHomeService)
        {
            this.panelHomeService = panelHomeService;
        }


        public ActionResult PanelAdmin()
        {
            return View();
        }

        public async Task<ActionResult> SiparisSayisi()
        {
            var result = await panelHomeService.SiparisSayisi();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        



    }
}