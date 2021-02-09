using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class UrunDetayUpdateViewModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int? UrunStok { get; set; }
        public string UrunAciklama { get; set; }
        public string ResimPath { get; set; }
    }
}
