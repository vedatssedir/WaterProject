using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;

namespace Models.Interfaces
{
    public interface IUrunService
    {
        Task<IList<Urun>> UrunListesi();
        Task<bool> UrunEkle(ProductAddViewModel model);
        Task<UrunGuncelleViewModel> GetUrunBilgi(int? id);
        Task<bool> UrunGuncelle(UrunGuncelleViewModel model);
        Task<bool> UrunSil(int? Id);
        Task<UrunDetayViewModel> GetUrunDetay(int? Id);
        Task<bool> UrunDetayOlustur(UrunDetayAddViewModel model);
        Task<UrunDetayUpdateViewModel> GetUrunDetayGuncelle(int? Id);
       


    }
}
