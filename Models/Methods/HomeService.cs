using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Interfaces;

namespace Models.Methods
{
    public class HomeService : IHomeService
    {
        private readonly TaskestiDataContext context;
        private GetKullaniciBilgiViewModel user;
        private readonly IAuthService authService;

        public HomeService(TaskestiDataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
            user = authService.GetIdentity();
        }


        public async Task<IList<Icerikler>> HomeList(int? sayfaId)
        {
            if (sayfaId == null) return null;
            var data = await context.Icerikler
                .Where(x => x.IsActive && !x.IsDelete && x.SayfaId == sayfaId && x.SiteId==user.SiteId).ToListAsync();
            return data;

        }


        public async Task<IList<Urun>> GetUrunListesi()
        {
            var data = await context.Urun.Where(x => x.IsActive && !x.IsDelete).ToListAsync();
            return data;
        }

        public async Task<IList<Sehirler>> GetSehirListesi()
        {
            var data = await context.Sehirler.ToListAsync();
            return data;
        }

        public async Task<IList<Ilceler>> GetIlceListesi()
        {
            var data = await context.Ilceler.ToListAsync();
            return data;
        }

        public async Task<UrunDetay> GetUrunDetay(int urunId)
        {
            var data = await context.UrunDetay.FirstOrDefaultAsync(x => x.UrunId == urunId);
            return data;
        }

        public async Task<bool> GetDetayKontrol(int urunId)
        {
            var result = false;
            var kontrol = await context.UrunDetay.Where(x => x.UrunId == urunId).FirstOrDefaultAsync();
            if (kontrol == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

    }
}
