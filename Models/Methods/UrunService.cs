using DataLayer.DataModels;
using DataLayer.ViewModels;
using Models.Hubs;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Models.Methods
{
    public class UrunService : IUrunService
    {
        private readonly TaskestiDataContext context;

        public UrunService(TaskestiDataContext context)
        {
            this.context = context;
        }



        public async Task<IList<Urun>> UrunListesi()
        {
            var data = await context.Urun.Where(x => x.IsActive && !x.IsDelete).ToListAsync();
            return data;
        }

        private string SaveDocument(HttpPostedFileBase document)
        {
            if (document == null) return string.Empty;
            var file = Path.GetFileName(document.FileName);
            var filename = $"{Guid.NewGuid()}-{file}";
            var path = Path.Combine(HttpContext.Current.Server.MapPath($"~/DocumentFiles/UrunResimler/{filename}"));
            var filePath = $"DocumentFiles/UrunResimler/{filename}";
            document.SaveAs(path);
            return filePath;
        }
        public async Task<bool> UrunEkle(ProductAddViewModel model)
        {
            var result = false;
            var imagePath = string.Empty;
            var kontrol = await context.Urun
                .FirstOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.UrunAdi == model.UrunAdi);
            if (kontrol == null)
            {
                if (model.ResimPath != null)
                {
                    imagePath = SaveDocument(model.ResimPath);
                }
                var data = new Urun
                {
                    UrunAdi = model.UrunAdi,
                    UrunResmi = imagePath == string.Empty ? null : imagePath,
                    Stok = model.Stok,
                    IsActive = true,
                    IsDelete = false,
                    BirimFiyat = model.BirimFiyat,
                };
                context.Urun.Add(data);
                await context.SaveChangesAsync();
                result = true;
                ProjeHub.GetUrunData();
            }
            else
            {
                result = false;
            }
            return result;
        }


        public async Task<UrunGuncelleViewModel> GetUrunBilgi(int? id)
        {
            var model = new UrunGuncelleViewModel();
            model.ResimAdi = "DocumentFiles/unnamed.png";
            if (id != null)
            {
                model = await context.Urun.Where(x => x.IsActive && !x.IsDelete && x.Id == id).Select(x =>
                    new UrunGuncelleViewModel
                    {
                        Id = x.Id,
                        BirimFiyat = x.BirimFiyat,
                        ResimAdi = x.UrunResmi,
                        Stok = x.Stok,
                        UrunAdi = x.UrunAdi,
                        IsActive = x.IsActive,
                        IsDelete = x.IsDelete
                    }).FirstOrDefaultAsync();
            }

            return model;
        }


        public async Task<bool> UrunGuncelle(UrunGuncelleViewModel model)
        {
            var result = false;
            try
            {
                var data = await context.Urun.FirstOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.Id == model.Id);
                if (data != null)
                {
                    data.UrunAdi = model.UrunAdi;
                    data.Stok = model.Stok;
                    data.BirimFiyat = model.BirimFiyat;
                    if (model.ResimPath != null)
                    {
                        var silinecekResim = HttpContext.Current.Server.MapPath($"~/Taskesti/images/resimler/{model.ResimPath}");
                        if (!string.IsNullOrEmpty(silinecekResim))
                        {
                            if (File.Exists(silinecekResim))
                            {
                                File.Delete(silinecekResim);
                            }
                        }
                        data.UrunResmi = SaveDocument(model.ResimPath);
                    }
                    await context.SaveChangesAsync();
                    result = true;
                    ProjeHub.GetUrunData();
                }
            }
            catch (Exception e)
            {
                var message = e.Message;
                throw;
            }

            return result;
        }
        public async Task<bool> UrunSil(int? Id)
        {
            var result = false;
            var data = await context.Urun.FirstOrDefaultAsync(x => x.Id == Id);
            if (data == null) return false;
            data.IsActive = false;
            data.IsDelete = true;
            var sonuc = await context.SaveChangesAsync();
            if (sonuc > 0)
            {
                result = true;
                ProjeHub.GetUrunData();
            }
            else
            {
                result = false;
            }

            return result;

        }


        public async Task<bool> UrunDetayOlustur(UrunDetayAddViewModel model)
        {
            var result = false;
            var kontrol = await context.UrunDetay.FirstOrDefaultAsync(x => x.UrunId == model.UrunId);
            if (kontrol != null)
            {
                result = false;
            }
            else
            {
                var urunDetay = new UrunDetay
                {
                    UrunId = model.UrunId,
                    UrunAciklama = model.UrunAciklama,
                    UrunAdi = model.UrunAdi,
                    UrunResim = model.ResimPath,
                    UrunStok = int.Parse(model.UrunStok)
                };
                context.UrunDetay.Add(urunDetay);
                var sonuc = await context.SaveChangesAsync();
                if (sonuc > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }

            }

            return result;
        }




        public async Task<UrunDetayViewModel> GetUrunDetay(int? Id)
        {
            var data = await context.Urun.Where(x => x.Id == Id && x.IsActive).Select(x => new UrunDetayViewModel
            {
                UrunAdi = x.UrunAdi,
                UrunStok = x.Stok,
                UrunId = x.Id,
                ResimPath = x.UrunResmi


            }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<UrunDetayUpdateViewModel> GetUrunDetayGuncelle(int? Id)
        {
            var data = await context.UrunDetay.Where(x => x.UrunId == Id).Select(x => new UrunDetayUpdateViewModel
            {
                UrunAciklama = x.UrunAciklama,
                UrunAdi = x.UrunAdi,
                UrunId = x.UrunId,
                UrunStok =x.UrunStok
            }).FirstOrDefaultAsync();
            return data;
        }



       



    }
}
