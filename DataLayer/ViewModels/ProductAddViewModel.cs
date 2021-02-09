using System.Web;

namespace DataLayer.ViewModels
{
    public class ProductAddViewModel
    {
        public string  UrunAdi { get; set; }
        public string Stok { get; set; }
        public string BirimFiyat { get; set; }
        public HttpPostedFileBase ResimPath { get; set; }

    }
}