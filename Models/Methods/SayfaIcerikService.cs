using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Hubs;
using Models.Interfaces;

namespace Models.Methods
{
    public class SayfaIcerikService : ISayfaIcerikService
    {
        private readonly TaskestiDataContext context;
        private GetKullaniciBilgiViewModel user;
        private readonly IAuthService authService;
        public SayfaIcerikService(TaskestiDataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
            user = authService.GetIdentity();
        }


        public async Task<IList<Sayfalar>> GetSayfaList()
        {
            var data = await context.Sayfalar.ToListAsync();
            return data;
        }



       
        public async Task<IList<Icerikler>> GetIcerikList(int? sayfaId)
        {
            var data = await context.Icerikler
                .Where(x => x.IsActive && !x.IsDelete && x.SayfaId == sayfaId && x.SiteId == user.SiteId).ToListAsync();
            return data;
        }

        private string SaveDocument(HttpPostedFileBase document)
        {
            if (document == null) return string.Empty;
            var file = Path.GetFileName(document.FileName);
            var filename = $"{Guid.NewGuid()}-{file}";
            var path = Path.Combine(HttpContext.Current.Server.MapPath($"~/DocumentFiles/AnaSayfaResimler/{filename}"));
            var filePath = $"DocumentFiles/AnaSayfaResimler/{filename}";
            document.SaveAs(path);
            return filePath;
        }
        public async Task<bool> SayfaIcerikGuncelle(SayfaIcerikUpdateViewModel model)
        {
            var result = false;
            var data = await context.Icerikler
                .FirstOrDefaultAsync(x => x.Id == model.Id && x.IsActive);
            if (data != null)
            {
                if (model.Baslik != null) data.Baslik = model.Baslik;
                if (model.Text != null) data.Text = model.Text;
                if (model.AltBaslik != null) data.AltBaslik = model.AltBaslik;
                if (model.AltBaslikText != null) data.AltBaslikText = model.AltBaslikText;
                if (model.Resim1 != null) data.Resim1 = SaveDocument(model.Resim1);
                if (model.Resim2 != null) data.Resim2 = SaveDocument(model.Resim2);
                if (model.Resim3 != null) data.Resim3 = SaveDocument(model.Resim3);
                if (model.Resim4 != null) data.Resim4 = SaveDocument(model.Resim4);
                if (model.Video1 != null) data.Video1 = SaveDocument(model.Video1);
                await context.SaveChangesAsync();
                result = true;
            }

            return result;
        }



       



        public async Task<string> GetText(int Id)
        {
            var data = await context.Icerikler.Where(x => x.IsActive && !x.IsDelete).Select(x => x.Text)
                .FirstOrDefaultAsync();
            return data;
        }



    }
}
