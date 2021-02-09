using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Hubs;
using Models.Interfaces;

namespace Models.Methods
{
    public class BayiService : IBayiService
    {
        private TaskestiDataContext context;

        public BayiService(TaskestiDataContext context)
        {
            this.context = context;
        }


        public async Task<IList<BayiListesiViewModel>> GetBayiList()
        {
            var data = await context.Bayiler.Where(x => x.IsActive && !x.IsDelete).Select(
                    x => new BayiListesiViewModel
                    {
                        IsActive = x.IsActive,
                        IsDelete = x.IsDelete,
                        Adresi = x.Adresi,
                        Eposta = x.Eposta,
                        Id = x.Id,
                        Telefon = x.Telefon,
                        SehirAdi = x.Sehirler.SehirAdi,
                        IlceAdi = x.Ilceler.IlceAdi,
                        SirketAdi = x.SirketAdi
                    }).ToListAsync();

            return data;
        }


        public async Task<bool> BayiOlustur(BayiAddViewModel model)
        {
            var result = false;
            var kontrol = await context.Bayiler
                .Where(x => x.IsActive && !x.IsDelete && x.Eposta == model.Eposta)
                .FirstOrDefaultAsync();
            if (kontrol != null)
            {
                result = false;
            }
            else
            {
                var bayi = new Bayiler()
                {
                    IsActive = model.IsActive,
                    IsDelete = model.IsDelete,
                    Adresi = model.Adres,
                    Eposta = model.Eposta,
                    IlceId = model.IlceId,
                    SehirId = model.SehirId,
                    Telefon = model.Telefon,
                    SirketAdi = model.SirketAdi

                };
                context.Bayiler.Add(bayi);
                await context.SaveChangesAsync();
                result = true;
                ProjeHub.GetBayiData();
            }

            return result;
        }



        public async Task<bool> BayiGüncelle(BayiUpdateViewModel model)
        {
            var result = false;
            var data = await context.Bayiler.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (data != null)
            {
                data.IsActive = model.IsActive;
                data.IsDelete = model.IsDelete;
                data.Eposta = model.Eposta;
                data.Adresi = model.Adres;
                data.Telefon = model.Telefon;
                data.SehirId = model.SehirId;
                data.IlceId = model.IlceId;
                data.SirketAdi = model.SirketAdi;
                await context.SaveChangesAsync();
                result = true;
                ProjeHub.GetBayiData();

            }
            else
            {
                result = false;
            }

            return result;
        }



        public async Task<bool> BayiSil(int Id)
        {
            var data = await context.Bayiler.FirstOrDefaultAsync(x => x.Id == Id);
            if (data != null)
            {
                data.IsDelete = true;
                data.IsActive = false;
                await context.SaveChangesAsync();
                ProjeHub.GetBayiData();
                return true;
             
            }

            return false;
        }



        public async Task<BayiGüncelleAndEkleViewModel> GetBayiBilgi(int? Id)
        {
            var model = new BayiGüncelleAndEkleViewModel();
            if (Id != null)
            {
                model = await context.Bayiler.Where(x => x.Id == Id).Select(x => new BayiGüncelleAndEkleViewModel
                {
                    Id = x.Id,
                    Adres = x.Adresi,
                    Eposta = x.Eposta,
                    Telefon = x.Telefon,
                    SehirId = x.SehirId,
                    IlceId = x.IlceId,
                    SirketAdi = x.SirketAdi

                }).FirstOrDefaultAsync();

            }

            return model;

        }
        public async Task<Tuple<IList<Sehirler>, IList<Ilceler>>> GetSehirler()
        {
            var sehirler = await context.Sehirler.ToListAsync();
            var ilceler = await context.Ilceler.ToListAsync();
            var result = new Tuple<IList<Sehirler>, IList<Ilceler>>(sehirler, ilceler);
            return result;
        }

        public async Task<string> GetBayiAdresi(int Id)
        {
            var data = await context.Bayiler.Where(x => x.Id == Id).Select(x => x.Adresi).FirstOrDefaultAsync();
            return data;
        }




        public async Task<IList<Ilceler>> GetIlceList(int sehirId)
        {
            var data = await context.Ilceler.Where(x => x.SehirId == sehirId).ToListAsync();
            return data;


        }



    }
}
