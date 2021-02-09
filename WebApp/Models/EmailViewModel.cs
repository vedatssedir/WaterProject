using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class EmailViewModel
    {
        public string AdSoyad { get; set; }
        public string  Eposta { get; set; }
        public string Konu { get; set; }
        public string Telefon{ get; set; }
        public string Mesaj { get; set; }
    }
}