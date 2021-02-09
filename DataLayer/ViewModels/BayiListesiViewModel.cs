namespace DataLayer.ViewModels
{
    public class BayiListesiViewModel
    {
        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Adresi { get; set; }
        public string SehirAdi { get; set; }
        public string IlceAdi { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}