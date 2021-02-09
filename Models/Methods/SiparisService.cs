using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Hubs;
using Models.Interfaces;
using PagedList;

namespace Models.Methods
{
    public class SiparisService : ISiparisService
    {

        private readonly TaskestiDataContext context;
        public SiparisService(TaskestiDataContext context)
        {
            this.context = context;
        }
        public async Task<bool> SiparisEkle(SiparisAddViewModel model)
        {
            var result = false;
            var siparis = new Siparis
            {
                AdSoyad = model.AdSoyad,
                Adet = model.Adet,
                EPosta = model.EPosta,
                SiparisTarih = model.SiparisTarih,
                SehirId = model.IlId,
                UrunId = model.UrunId,
                Adres = model.Adres,
                Telefon = model.Telefon,
                IsActive = true,
                IsDelete = false,
                Aciklama = model.Aciklama,

            };
            context.Siparis.Add(siparis);
            var sonuc = await context.SaveChangesAsync();

            if (sonuc > 0)
            {
                result = true;
                ProjeHub.GetData();
            }

            return result;
        }
        public async Task<IList<Sehirler>> GetSehirler()
        {
            var data = await context.Sehirler.ToListAsync();
            return data;
        }
        public async Task<string> GetAciklamaMetin(int id)
        {
            var data = await context.Siparis.Where(x => x.IsActive && !x.IsDelete && x.Id == id).Select(x => x.Aciklama)
                .FirstOrDefaultAsync();
            return data;
        }
        public async Task<string> GetAdresMetin(int id)
        {
            var data = await context.Siparis.Where(x => x.IsActive && !x.IsDelete && x.Id == id).Select(x => x.Adres)
                .FirstOrDefaultAsync();
            return data;
        }
        public IQueryable<SiparisListViewModel> SiparisListesi()
        {
            var data = context.Siparis.Include(x => x.Sehirler)
                .OrderBy(x => x.SiparisTarih)
                .Where(x => x.IsActive && !x.IsDelete).Select(x => new SiparisListViewModel
                {
                    AdSoyad = x.AdSoyad,
                    Adet = x.Adet,
                    Adres = x.Adres,
                    Eposta = x.EPosta,
                    Id = x.Id,
                    UrunAdi = x.Urun.UrunAdi,
                    Il = x.Sehirler.SehirAdi,
                    Tarih = x.SiparisTarih,
                    Telefon = x.Telefon,
                    SiparisDurum = x.SiparisDurum,
                    Aciklama = x.Aciklama,
                    SehirId = x.SehirId
                });
            return data;
        }
        public Tuple<IPagedList<SiparisListViewModel>, int> GetSiparisListe(int? page)
        {

            var pageSize = 12;
            var pageIndex = 1;
            var recordNo = 0;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            IPagedList<SiparisListViewModel> siparisListe;
            siparisListe = SiparisListesi().ToPagedList(pageIndex, pageSize);
            if (pageIndex != 1)
            {
                recordNo = ((pageIndex - 1) * pageSize);
            }
            var result = new Tuple<IPagedList<SiparisListViewModel>, int>(siparisListe, recordNo);
            return result;
        }
        public Tuple<IPagedList<SiparisListViewModel>, int> SiparisAra(int? page, string sehirId, string siparistarih)
        {
            int pageSize = 12;
            int pageIndex = 1;
            int recordNo = 0;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var shId = Convert.ToInt32(sehirId);
            IPagedList<SiparisListViewModel> data = null;
            if (shId != 0 && string.IsNullOrEmpty(siparistarih))
            {
                data = SiparisListesi().Where(x => x.SehirId == shId).ToPagedList(pageIndex, pageSize);
            }

            if (shId==0 && !string.IsNullOrEmpty(siparistarih))
            {
                data = SiparisListesi().Where(x => x.Tarih.Contains(siparistarih)).ToPagedList(pageIndex, pageSize);
            }
            if (shId!=0 && !string.IsNullOrEmpty(siparistarih))
            {
                data = SiparisListesi().Where(x => x.SehirId == shId && x.Tarih.Contains(siparistarih)).ToPagedList(pageIndex, pageSize);
            }
            if (pageIndex != 1)
            {
                recordNo = ((pageIndex - 1) * pageSize);
            }
            var result = new Tuple<IPagedList<SiparisListViewModel>, int>(data, recordNo);

            return result;
        }
        public Dictionary<int, string> GetDurumBilgisi()
        {
            var list = new Dictionary<int, string> { { 1, "Talep Alındı." }, { 2, "Sipariş Hazırlanıyor." }, { 3, "Siparis Teslim edildi." } };
            return list;
        }
        public async Task<bool> SiparisSil(int siparisId)
        {
            var data = await context.Siparis.FirstOrDefaultAsync(x => x.Id == siparisId);
            if (data == null) return false;
            data.IsActive = false;
            data.IsDelete = true;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IList<Ilceler>> GetIlceBilgi(int sehirId)
        {
            var data = await context.Ilceler.Where(x => x.SehirId == sehirId).ToListAsync();
            return data;
        }
        public async Task<IList<Bayiler>> GetBayiBilgi(int ilceId)
        {
            var data = await context.Bayiler
                .Where(x => x.IsActive && !x.IsDelete && x.IlceId == ilceId).
                ToListAsync();
            return data;
        }
        public async Task<bool> PanelSiparisMailGonder(int siparisId, int durumId)
        {
            var result = false;
            var messagedata = string.Empty;
            try
            {
                var data = await context.Siparis.Where(x => x.IsActive && !x.IsDelete && x.Id == siparisId)
                        .FirstOrDefaultAsync();
                if (data != null && durumId != 0)
                {
                    data.SiparisDurum = durumId;
                    await context.SaveChangesAsync();
                    switch (durumId)
                    {
                        case 1:
                            messagedata = "Talebiniz alınmıştır";
                            break;
                        case 2:
                            messagedata = "Sipariş Hazırlanıyor";
                            break;
                        case 3:
                            messagedata = "Siparişiniz teslim edildi";
                            break;
                    }

                    var message = new MailMessage { From = new MailAddress("vedatssedir@gmail.com", "Taşkesti") };
                    message.To.Add(data.EPosta ?? string.Empty);
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;
                    message.Subject = "Sipariş Durum";
                    message.IsBodyHtml = true;
                    message.Body = $"Merhabalar;\n\n {data.Urun.UrunAdi} adlı {data.Adet} adet ürün {messagedata}";
                    message.Priority = MailPriority.High;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Credentials = new NetworkCredential("vedatssedir@gmail.com", "vedatSedir2323"),
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        Port = 587
                    };
                    smtp.Send(message);
                    result = true;
                    ProjeHub.GetData();
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
                result = false;
            }

            return result;
        }
    }
}
