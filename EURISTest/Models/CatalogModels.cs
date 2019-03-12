using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Models
{
    public class CatalogModels
    {
        public class CatalogCreate
        {
            [Required(ErrorMessage ="The Field Description is Required")]
            [Display(Name = "Description")]
            public string Description { get; set; }

            [Required(ErrorMessage = "The Field Code is Required")]
            [Display(Name = "Code")]
            public string Code { get; set; }
        }
        public class CatalogEdit: CatalogCreate
        {
            public int Id { get; set; }
        }
        public class CatalogDetails
        {
            public string Description { get; set; }

            public string Code { get; set; }

            public int[] IdCatalog { get; set; }

            public List<EURIS.Entities.Product> Products { get; set; }
        }

        public class AssociationProducts
        {
            public string Description { get; set; }
            public string Code { get; set; }
            public int IdCatalog { get; set; }
            public int[] IdProducts { get; set; }
            public SelectList Products { get; set; }
            public bool IsError { get; set; }
        }

    }

}