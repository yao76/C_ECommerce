using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductsAndCategories.Models
{
    public class Category
    {
        [Key]
        public int CategoryId{get;set;}

        [Required]
        [MaxLength(45)]
        [MinLength(2)]
        public string Name{get;set;}


        // list of products for a particular category
        public List<ProductCategory> Associated{get;set;}

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
    }
}