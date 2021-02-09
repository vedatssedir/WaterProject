using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class GetKullaniciBilgiViewModel
    {
        public string KullaniciAdi { get; set; }
        public int SiteId { get; set; }
        public string Email { get; set; }
        public List<string> Roller { get; set; }
    }
}
