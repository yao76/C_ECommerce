using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductsAndCategories.Models
{
    public class Product
    {
        [Key]
        public int ProductId{get;set;}

        [Required]
        [MaxLength(45)]
        [MinLength(2)]
        public string Name{get;set;}

        [Required]
        [MinLength(2)]
        public string Description{get;set;}

        [Required]
        [Range(0,int.MaxValue)]
        public decimal Price{get;set;}

        // List of categories for a particular product
        public List<ProductCategory> Associated{get;set;}

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;


    }
}