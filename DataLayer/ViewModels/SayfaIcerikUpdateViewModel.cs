using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataLayer.ViewModels
{
    public class SayfaIcerikUpdateViewModel
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Text { get; set; }
        public string AltBaslik { get; set; }
        public string AltBaslikText { get; set; }
        public HttpPostedFileBase Resim1 { get; set; }
        public HttpPostedFileBase Resim2 { get; set; }
        public HttpPostedFileBase Resim3 { get; set; }
        public HttpPostedFileBase Resim4 { get; set; }
        public HttpPostedFileBase Video1 { get; set; }

    }
}
