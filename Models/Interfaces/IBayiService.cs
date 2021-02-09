using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;

namespace Models.Interfaces
{
    public interface IBayiService
    {
        Task<IList<BayiListesiViewModel>> GetBayiList();
        Task<bool> BayiOlustur(BayiAddViewModel model);
        Task<bool> BayiGüncelle(BayiUpdateViewModel model);
        Task<bool> BayiSil(int Id);
        Task<BayiGüncelleAndEkleViewModel> GetBayiBilgi(int? Id);
        Task<Tuple<IList<Sehirler>, IList<Ilceler>>> GetSehirler();
        Task<string> GetBayiAdresi(int Id);
        Task<IList<Ilceler>> GetIlceList(int sehirId);
    }
}
