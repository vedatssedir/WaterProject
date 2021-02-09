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

namespace Models.Methods
{
    public class EmailService:IEmailService
    {
        private readonly TaskestiDataContext context;
        private static GetKullaniciBilgiViewModel user;
        private IAuthService AuthService;
        public EmailService(TaskestiDataContext context, IAuthService authService)
        {
            this.context = context;
            AuthService = authService;
            user = authService.GetIdentity();
        }

    

        public async Task<bool> EmailOlustur(EmailAddViewModel model)
        {
            var result = false;
            var emaildata = new EPostalar
            {
                AdSoyad = model.AdSoyad,
                Eposta = model.Eposta,
                Konu = model.Konu,
                Mesaj = model.Mesaj,
                Telefon = model.Telefon,
                EmailTarih = DateTime.Now,
                SiteId =user.SiteId
            };
            context.EPostalar.Add(emaildata);
            var sonuc = await context.SaveChangesAsync();
            if (sonuc > 0)
            {
                result = true;
                ProjeHub.GetEmailData();
            }
            else
            {
                result = false;
            }
            return result;
        }


        public async Task<bool> EmailSil(int Id)
        {
            var data = await context.EPostalar.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null) return false;
            context.EPostalar.Remove(data);
            await context.SaveChangesAsync();
            ProjeHub.GetEmailData();
            return true;
        }

        public async Task<IList<EmailListViewModel>> EmailList()
        {
            var data = await context.EPostalar.Where(x => x.SiteId == user.SiteId).Select(x => new EmailListViewModel
            {
                Id = x.Id,
                Eposta =x.Eposta,
                AdSoyad = x.AdSoyad,
                Konu =x.Konu,
                Mesaj = x.Mesaj,
                Telefon = x.Telefon,
                EmailTarihi = x.EmailTarih

            }).ToListAsync();
            return data;
        }


        public async Task<string> GetEMailAdress(int Id)
        {
            var data = await context.EPostalar.Where(x => x.Id == Id).Select(x => x.Eposta).FirstOrDefaultAsync();
            return data;
        }


        public async Task<bool> EmailGonder(EmailSendViewModel model)
        {
            var result = false;
            var message = new MailMessage { From = new MailAddress("vedatssedir@gmail.com", "Taşkesti") };
            message.To.Add(model.EmailAdres ?? string.Empty);
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = $"{model.Konu}";
            message.IsBodyHtml = true;
            message.Body = $"Merhabalar;{model.Mesaj}";
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
            return true;
        }



    }
}
