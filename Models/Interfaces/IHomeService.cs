using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;

namespace Models.Interfaces
{
    public interface IHomeService
    {
        Task<IList<Icerikler>> HomeList(int? sayfaId);
        Task<IList<Urun>> GetUrunListesi();
        Task<IList<Sehirler>> GetSehirListesi();
        Task<IList<Ilceler>> GetIlceListesi();
        Task<UrunDetay> GetUrunDetay(int urunId);
        Task<bool> GetDetayKontrol(int urunId);
    }
}
