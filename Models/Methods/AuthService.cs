using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Models.Interfaces;

namespace Models.Methods
{
    public class AuthService : IAuthService
    {
        private readonly TaskestiDataContext context;

        public AuthService(TaskestiDataContext context)
        {
            this.context = context;
        }


        public async Task<IList<GetSiteViewModel>> SiteList()
        {
            var data = await context.Site.Where(x => x.IsActive && !x.IsDelete).Select(x => new GetSiteViewModel
            {
                Id = x.Id,
                SiteAdi = x.SiteAdi
            }).ToListAsync();
            return data;
        }

        private IList<Claim> GetClaims(GetKullaniciBilgiViewModel model)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,model.KullaniciAdi),
                new Claim(ClaimTypes.PrimarySid,model.SiteId.ToString())
            };
            model.Roller.ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x));
            });
            return claims;
        }

        private IAuthenticationManager _authManager;
        public IAuthenticationManager AuthenticationManager
        {
            get => _authManager ?? (_authManager = HttpContext.Current.GetOwinContext().Authentication);
            set => _authManager = value;
        }

        private void SignIn(IList<Claim> claims, bool hatirla)
        {
            var claimsIdentity = new ClaimsIdentity(claims,
                DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = hatirla }, claimsIdentity);
            HttpContext.Current.User = new ClaimsPrincipal(AuthenticationManager.AuthenticationResponseGrant.Principal);
        }
        public void LogOff()
        {
            _authManager = HttpContext.Current.GetOwinContext().Authentication;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }


        public GetKullaniciBilgiViewModel GetIdentity()
        {
            var data = new GetKullaniciBilgiViewModel();
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            if (identity.IsAuthenticated)
            {
                data.Email = identity.Claims.Where(x => x.Type == ClaimTypes.Email).Select(x => x.Value)
                    .FirstOrDefault();
                var siteId = identity.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(x => x.Value)
                .FirstOrDefault();
                data.SiteId = int.Parse(siteId ?? string.Empty);
                data.KullaniciAdi = identity.Claims.Where(x => x.Type == ClaimTypes.Name).Select(x => x.Value)
                     .FirstOrDefault();
                data.Roller = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
                    .ToList();
            }

            return data;
        }


        public async Task<bool> GirisYap(string username, string password, int siteId)

        {
            var result = false;
            var kontrol = await context.Kullanici.Where(x => x.KullaniciAdi == username && x.IsActive && !x.IsDelete).Select(x => x.SiteId).FirstOrDefaultAsync();
            if (kontrol == siteId)
            {
                var checkUser = await context.Kullanici.Where(x =>
                        x.IsActive && !x.IsDelete && x.KullaniciAdi == username && x.Sifre == password)
                    .Select(y => new GetKullaniciBilgiViewModel
                    {
                        KullaniciAdi = y.KullaniciAdi,
                        Roller = y.KullaniciRol.Select(x => x.Rol.RolAdi).ToList(),
                        SiteId = siteId
                    }).FirstOrDefaultAsync();

                if (checkUser != null)
                {
                    var claims = GetClaims(checkUser);
                    if (claims != null)
                    {
                        SignIn(claims, true);
                        FormsAuthentication.SetAuthCookie(checkUser.KullaniciAdi, true);
                        HttpContext.Current.Session.Add("sifre", password);
                        result = true;
                    }
                }
                else
                {
                    result = false;
                }

            }

            return result;

        }





    }
}
