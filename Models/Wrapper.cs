using System.Collections.Generic;
namespace ProductsAndCategories.Models
{
    public class Wrapper
    {
        public Product OneProduct{get;set;}
        public Category OneCategory{get;set;}

        public List<Category> AllCategories{get;set;}
        public List<Product> AllProducts{get;set;}

        // public Wrapper(Product oneproduct, List<Category> allcats)
        // {
        //     OneProduct = oneproduct;
        //     AllCategories = allcats;
        // }
    }
}