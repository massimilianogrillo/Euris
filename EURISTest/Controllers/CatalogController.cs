using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EURIS.Service;
using EURIS.Entities;
namespace EURISTest.Controllers
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/

        public ActionResult Index()
        {
            CatalogManager catManager = new CatalogManager();
            List<Catalog> catalogs = catManager.GetCatalogs();
            return View(catalogs);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Add(Models.CatalogModels.CatalogCreate model)
        {
            CatalogManager catManager = new CatalogManager();
            catManager.AddCatalog(new Catalog { Description = model.Description,Code = model.Code });
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            CatalogManager catManager = new CatalogManager();
            var Catalog = catManager.GetCatalog(Id);
            var Model = new Models.CatalogModels.CatalogEdit {
                Description = Catalog.Description,
                Id = Catalog.Id,
                Code = Catalog.Code
            };
            return View(Model);
        }
        public ActionResult Update(Models.CatalogModels.CatalogEdit model)
        {
            CatalogManager catManager = new CatalogManager();
            catManager.UpdateCatalog(new Catalog { Description = model.Description,Id = model.Id,Code = model.Code });
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            CatalogManager catManager = new CatalogManager();
            catManager.DeleteCatalog(Id);
            return RedirectToAction("Index");
        }
        public ActionResult Details(int Id)
        {
            CatalogManager catManager = new CatalogManager();
            Catalog cat = catManager.GetCatalog(Id);
            var Model = new Models.CatalogModels.CatalogDetails {
                Code = cat.Code,
                Description = cat.Description,
                Products = cat.CatalogProduct.Select(s=> s.Product).ToList()
            };
            return View(Model);
        }
        public ActionResult Association(int Id, bool? isError)
        {
            CatalogManager catManager = new CatalogManager();
            ProductManager prodManager = new ProductManager();
            var Catalog = catManager.GetCatalog(Id);
            var Products = prodManager.GetProducts();
            var Model = new Models.CatalogModels.AssociationProducts
            {
                Description = Catalog.Description,
                IdCatalog = Catalog.Id,
                Code = Catalog.Code,
                Products = new SelectList(Products, "Id", "Description"),
                IsError = !isError.HasValue ? false : isError.Value
            };
            return View(Model);
            //Catalogs = new SelectList(_Catalogs, "Id", "Description"),
        }
        public ActionResult DoAssociation(Models.CatalogModels.AssociationProducts model)
        {
            if (model.IdProducts == null || model.IdProducts.Count() == 0)
            {
                return RedirectToAction("Association", new { Id = model.IdCatalog, isError = true });
            }
            CatalogManager catManager = new CatalogManager();
            List<CatalogProduct> cList = new List<CatalogProduct>();
            foreach (var item in model.IdProducts)
            {
                cList.Add(new CatalogProduct { IdCatalog = model.IdCatalog, IdProduct = item });
            }
            catManager.AddAssociation(cList);
            return RedirectToAction("Details", new { Id = model.IdCatalog });
        }
    }
}
