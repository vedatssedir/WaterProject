using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ViewModels
{
    public class EmailSendViewModel
    {
        public string EmailAdres { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
    }
}
