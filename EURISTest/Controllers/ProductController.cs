using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;

namespace EURISTest.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult Index()
        {
            ProductManager productManager = new ProductManager();
            List<Product> products = productManager.GetProducts();
            return View(products);
        }
        public ActionResult Create()
        {
            //CatalogManager catManager = new CatalogManager();
            //var _Catalogs = catManager.GetCatalogs();
            //var Model = new Models.ProductModels.ProductCreate
            //{
            //    Catalogs = new SelectList(_Catalogs, "Id", "Description"),
            //};
            return View();
        }
        public ActionResult Add(Models.ProductModels.ProductCreate model)
        {
            ProductManager ProdManager = new ProductManager();
           
            Product NewProduct = new Product {
                //CatalogProduct = cList,
                Code = model.Code,
                Description = model.Description
            };
            ProdManager.AddProduct(NewProduct);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            ProductManager ProdManager = new ProductManager();
            ProdManager.DeleteProduct(Id);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            ProductManager ProdManager = new ProductManager();
            //CatalogManager catManager = new CatalogManager();
            var Prod = ProdManager.GetProduct(Id);
            //var _Catalogs = catManager.GetCatalogs();
            var Model = new Models.ProductModels.ProductEdit
            {
                Description = Prod.Description,
                Code = Prod.Code,
                Id = Prod.Id
                //Catalogs = new SelectList(_Catalogs, "Id", "Description"),
                //IdCatalog = Prod.CatalogProduct.Select(s=> s.IdCatalog).ToArray()
            };
            return View(Model);
        }
        public ActionResult Update(Models.ProductModels.ProductEdit model)
        {
            ProductManager ProdManager = new ProductManager();
            //List<CatalogProduct> cList = new List<CatalogProduct>();
            //foreach (var item in model.IdCatalog)
            //{
            //    cList.Add(new CatalogProduct { IdCatalog = item });
            //}
            Product NewProduct = new Product
            {
                //CatalogProduct = cList,
                Code = model.Code,
                Description = model.Description,
                Id = model.Id
            };
            ProdManager.UpdateProduct(NewProduct);
            return RedirectToAction("Index");
        }
    }
}
