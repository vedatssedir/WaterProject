using System;

namespace DataLayer.ViewModels
{
    public class SiparisListViewModel
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public string Tarih { get; set; }
        public string Il { get; set; }
        public string UrunAdi { get; set; }
        public int? SiparisDurum { get; set; }
        public string Adet { get; set; }
        public string Aciklama { get; set; }
        public int SehirId { get; set; }
    }
}