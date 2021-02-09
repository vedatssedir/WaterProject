using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using PagedList;

namespace Models.Interfaces
{
    public interface ISiparisService
    {
        Task<bool> SiparisEkle(SiparisAddViewModel model);

        Task<IList<Sehirler>> GetSehirler();
        Dictionary<int, string> GetDurumBilgisi();
        Task<bool> SiparisSil(int siparisId);
        Task<IList<Ilceler>> GetIlceBilgi(int sehirId);
        Task<IList<Bayiler>> GetBayiBilgi(int ilceId);
        Task<bool> PanelSiparisMailGonder(int siparisId, int durumId);
        Tuple<IPagedList<SiparisListViewModel>, int> GetSiparisListe(int? page);
        Task<string> GetAciklamaMetin(int id);
        Tuple<IPagedList<SiparisListViewModel>, int> SiparisAra(int? page,string sehirId, string siparistarih);
        Task<string> GetAdresMetin(int id);
    }
}
