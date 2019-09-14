using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.EntityFrameworkCore;
namespace ProductsAndCategories.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId{get;set;}

        public int ProductId{get;set;}

        public int CategoryId{get;set;}

        public Product Product{get;set;}
        public Category Category{get;set;}

        public DateTime CreatedAt{get;set;} = DateTime.Now;
        public DateTime UpdatedAt{get;set;} = DateTime.Now;
    }
}