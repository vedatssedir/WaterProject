using System;

namespace DataLayer.ViewModels
{
    public class SiparisAddViewModel
    {
        public string SiparisTarih { get; set; } = DateTime.Now.ToShortDateString();
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string EPosta { get; set; }
        public int UrunId { get; set; }
        public string Adet { get; set; }
        public int IlId { get; set; }
        public string Adres { get; set; }
        public string Aciklama { get; set; }
    }
}