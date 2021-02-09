using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class BayiAddViewModel
    {
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public string Adres { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public int SehirId { get; set; }
        public int? IlceId { get; set; }
        public string SirketAdi { get; set; }
    }
}
