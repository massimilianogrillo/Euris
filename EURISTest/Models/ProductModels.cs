using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EURISTest.Models
{
    public class ProductModels
    {
        public class ProductCreate
        {
            [Required(ErrorMessage = "The Field Description is Required")]
            [Display(Name = "Description")]
            public string Description { get; set; }

            [Required(ErrorMessage = "The Field Code is Required")]
            [Display(Name = "Code")]
            public string Code { get; set; }
           
        }
        public class ProductEdit: ProductCreate
        {
            public int Id { get; set; }
        }
    }
}