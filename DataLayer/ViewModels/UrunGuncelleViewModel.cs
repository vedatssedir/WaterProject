using System.Web;

namespace DataLayer.ViewModels
{
    public class UrunGuncelleViewModel
    {
        public int Id { get; set; }
        public string UrunAdi { get; set; }
        public string Stok { get; set; }
        public string BirimFiyat { get; set; }
        public string ResimAdi { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public HttpPostedFileBase ResimPath { get; set; }
    }
}