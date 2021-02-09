using DataLayer.ViewModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Models.Interfaces
{
    public interface IAuthService
    {
        Task<IList<GetSiteViewModel>> SiteList();
        GetKullaniciBilgiViewModel GetIdentity();
        void LogOff();
        Task<bool> GirisYap(string username, string password, int siteId);
    }
}
