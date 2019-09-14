using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;
using Microsoft.EntityFrameworkCore;


namespace ProductsAndCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            
            return RedirectToAction("Products");
        }
        [HttpGet("products")]
        public IActionResult Products()
        {
            List<Product> allproducts = dbContext.Products.ToList();
            ViewBag.allproducts = allproducts;
            return View();
        }

        [HttpGet("categories")]
        public IActionResult Categories()
        {
            List<Category> allcategories = dbContext.Categories.ToList();
            ViewBag.allcategories = allcategories;
            return View();
        }

        [HttpPost("newproduct")]
        public IActionResult NewProduct(Product formData)
        {
            if(ModelState.IsValid)
            {
                dbContext.Products.Add(formData);
                dbContext.SaveChanges();
                return RedirectToAction("Products");
            }
            return View("Products");
        }

        [HttpPost("newcategory")]
        public IActionResult NewCategory(Category formData)
        {
            if(ModelState.IsValid)
            {
                dbContext.Categories.Add(formData);
                dbContext.SaveChanges();
                return RedirectToAction("Categories");
            }
            ModelState.AddModelError("Name", "Invalid Name");
            return View("Categories");
        }

        [HttpGet("products/{id}")]
        public IActionResult ProductDetails(int id)
        {
            Product prod = dbContext.Products
                .Include(product=>product.Associated)
                .ThenInclude(category=>category.Category)
                .FirstOrDefault(product=>product.ProductId == id);
            System.Console.WriteLine(new string('-',80));
            System.Console.WriteLine(prod.Name);
            List<Category> allcategories = dbContext.Categories
                .OrderByDescending(c=>c.CreatedAt)
                .ToList();

            List<Category> AssociatedCategories = prod.Associated
                .Select(c=>c.Category).ToList();
            ViewBag.UnAssociatedCategories = allcategories.Except(AssociatedCategories);
            Wrapper newwrapper = new Wrapper();
            newwrapper.AllCategories = allcategories;
            newwrapper.OneProduct = prod;
            
            

            // ViewBag.Categories = prod.Categories;
            // System.Console.WriteLine(new string('*',80));
            // System.Console.WriteLine(prod.Categories);
            return View(newwrapper);
        }

        [HttpPost("products/{id}")]
        public IActionResult AddCategory(int id, int categoryId)
        {
            ProductCategory newassociation = new ProductCategory();
            newassociation.CategoryId = categoryId;
            newassociation.ProductId = id;
            dbContext.Add(newassociation);
            dbContext.SaveChanges();
            return RedirectToAction("ProductDetails", new{id=id});
        }

        [HttpGet("categories/{id}")]
        public IActionResult CategoryDetails(int id)
        {Category cat = dbContext.Categories
                .Include(category=>category.Associated)
                .ThenInclude(product=>product.Product)
                .FirstOrDefault(category=>category.CategoryId == id);
            // System.Console.WriteLine(new string('-',80));
            // System.Console.WriteLine(cat.Name);
            List<Product> allproducts = dbContext.Products
                .OrderByDescending(c=>c.CreatedAt)
                .ToList();

            // List of products that are associated with this category
            // allproducts.exept(newlist)

            List<Product> associatedproducts = cat.Associated
                .Select(a=>a.Product)
                .ToList();
            ViewBag.UnAssociatedProducts = allproducts
                .Except(associatedproducts);

            Wrapper newwrapper = new Wrapper();
            newwrapper.AllProducts = allproducts;
            newwrapper.OneCategory = cat;

            return View(newwrapper);
        }
        [HttpPost("categories/{id}")]
        public IActionResult AddProduct(int id, int productId)
        {
            System.Console.WriteLine($"-----------{id}---------");
            System.Console.WriteLine($"-----------{productId}---------");

            ProductCategory newassociation = new ProductCategory();
            newassociation.CategoryId = id;
            newassociation.ProductId = productId;
            dbContext.Add(newassociation);
            dbContext.SaveChanges();
            return RedirectToAction("CategoryDetails", new{id = id});
        }

    }
}
