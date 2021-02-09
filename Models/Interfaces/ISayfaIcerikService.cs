using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;

namespace Models.Interfaces
{
    public interface ISayfaIcerikService
    {
        Task<IList<Sayfalar>> GetSayfaList();
        Task<IList<Icerikler>> GetIcerikList(int? sayfaId);
        Task<bool> SayfaIcerikGuncelle(SayfaIcerikUpdateViewModel model);
        Task<string> GetText(int Id);
    }
}
