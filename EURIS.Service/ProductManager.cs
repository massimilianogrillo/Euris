using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EURIS.Entities;
using System.Data.Entity;

namespace EURIS.Service
{
    public class ProductManager
    {
        public List<Product> GetProducts()
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                return context.Product.ToList();
            }
        }

        public Product GetProduct(int Id)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                return context.Product.Include("CatalogProduct")
                        .Where(w=> w.Id == Id).FirstOrDefault();
            }
        }
        public void AddProduct(Product p)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                try
                {
                    context.Product.Add(p);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public void UpdateProduct(Product p)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                var Product = context.Product.Find(p.Id);
                Product.Code = p.Code;
                Product.Description = p.Description;
                //foreach (var item in p.CatalogProduct)
                //{
                //    var Cp = context.CatalogProduct.Where(w => w.IdProduct == p.Id && w.IdCatalog == item.IdCatalog);
                //    foreach (var sItem in Cp)
                //    {
                //        context.CatalogProduct.Remove(sItem);
                //    }
                //}
                //Product.CatalogProduct = p.CatalogProduct;
                context.Entry<Product>(Product);
                context.SaveChanges();
            }
        }
        public void DeleteProduct(int IdProduct)
        {
            using (LocalDbEntities context = new LocalDbEntities())
            {
                List<CatalogProduct> cPList = new List<CatalogProduct>();
                var Product = context.Product.Find(IdProduct);
                var Cp = context.CatalogProduct.Where(w => w.IdProduct == IdProduct);
                foreach (var item in Cp)
                {
                    context.CatalogProduct.Remove(item);
                }
                context.Product.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
