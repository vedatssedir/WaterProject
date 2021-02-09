using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
   public  class EmailListViewModel
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public DateTime? EmailTarihi { get; set; }

    }
}
