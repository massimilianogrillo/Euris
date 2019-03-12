using EURIS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace EURIS.Service
{
    public class CatalogManager
    {
        public List<Catalog> GetCatalogs()
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                return context.Catalog.ToList();
            }
        }
        public Catalog GetCatalog(int Id)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                return context.Catalog
                        .Include("CatalogProduct.Product")
                        .Where(w => w.Id == Id).First();
            }
        }
        public void AddCatalog(Catalog c)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                context.Catalog.Add(c);
                context.SaveChanges();
            }
        }
        public void UpdateCatalog(Catalog c)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                var Catalog = context.Catalog.Find(c.Id);
                Catalog.Description = c.Description;
                Catalog.Code = c.Code;
                context.Catalog.Attach(Catalog);
                context.Entry(Catalog).State = System.Data.EntityState.Modified;
                context.SaveChanges();
            }
        }
        public void DeleteCatalog(int IdCatalog)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                var Catalog = context.Catalog.Find(IdCatalog);
                var Cp = context.CatalogProduct.Where(w => w.IdCatalog == IdCatalog);
                foreach (var item in Cp)
                {
                    context.CatalogProduct.Remove(item);
                }
                context.Catalog.Remove(Catalog);
                context.SaveChanges();
            }
        }
        public void AddAssociation(List<CatalogProduct> c)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                foreach (var item in c)
                {
                    var Cp = context.CatalogProduct.Where(w => w.IdCatalog == item.IdCatalog);
                    foreach (var sItem in Cp)
                    {
                        context.CatalogProduct.Remove(sItem);
                    }
                }
                foreach (var item in c)
                {
                    context.CatalogProduct.Add(item);
                }
                
                context.SaveChanges();
            }
        }
    }
}
