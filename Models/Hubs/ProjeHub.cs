using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Models.Hubs
{
    [HubName("phub")]
    public class ProjeHub : Hub
    {
        public static void GetData()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.refreshData();
        }

        public static void GetUrunData()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.getUrunData();
        }


        public static void GetBayiData()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.refreshBayiData();
        }

        public static void GetSiparisListe()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.getsiparislistesi();
        }

        public static void GetEmailData()
        {

            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.emailRefreshData();
        }


        public static void GetIcerikData(int sayfaId)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<ProjeHub>();
            context.Clients.All.IcerikData(sayfaId);
        }



    }
}