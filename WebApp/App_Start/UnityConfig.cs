using Models.Interfaces;
using Models.Methods;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
namespace WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IBayiService, BayiService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<ISiparisService, SiparisService>();
            container.RegisterType<IUrunService, UrunService>();
            container.RegisterType<IHomeService, HomeService>();
            container.RegisterType<IPanelHomeService, PanelHomeService>();
            container.RegisterType<ISayfaIcerikService, SayfaIcerikService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}