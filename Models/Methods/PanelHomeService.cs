using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using Models.Interfaces;

namespace Models.Methods
{
    public class PanelHomeService:IPanelHomeService
    {
        private readonly TaskestiDataContext context;

        public PanelHomeService(TaskestiDataContext context)
        {
            this.context = context;
        }

        public async Task<int> SiparisSayisi()
        {
            var data = await context.Siparis.CountAsync(x => x.IsActive);
            return data;
        }


    }
}
