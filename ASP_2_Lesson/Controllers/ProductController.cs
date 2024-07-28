using ASP_2_Lesson.Entities;
using ASP_2_Lesson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ASP_2_Lesson.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> Products { get; set; } = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Bread",
                Category = "FlourProducts",
                Price = 1.5,
                Discount = 10,
                ImagePath="https://images.squarespace-cdn.com/content/v1/637d515697f2794487fadba3/1678986214708-5LDWY9SQAF7AY1X3N11A/krusty_roll.png?format=1500w"
            },
            new Product
            {
                Id = 2,
                Name = "Lays",
                Category = "CipsProducts",
                Price = 3,
                Discount = 15,
                ImagePath = "https://www.bigbasket.com/media/uploads/p/xxl/294283_18-lays-potato-chips-spanish-tomato-tango.jpg"
            },
            new Product
            {
                Id = 3,
                Name = "Red Bull",
                Price = 4,
                Category = "EnergyDrinks",
                Discount = 15,
                ImagePath = "https://bazarstore.az/cdn/shop/products/30011102_1000x.png?v=1693894488"
            }
        };

        public IActionResult Index()
        {
            var ProductVM = new ProductViewModel
            {
                Products = Products
            };
            return View(ProductVM);
        }

        [HttpGet]
        public IActionResult Update(int myid)
        {
            myid -= 1;
            var product = Products[myid];
            var vm = new ProductUpdateViewModel
            {
                Product = product
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel vm, int myid)
        {
            myid -= 1;
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Id == vm.Product.Id)
                {
                    myid = Products[i].Id;
                    break;
                }
            }
            var prod = Products[myid];
            prod.Price = vm.Product.Price;
            prod.Name = vm.Product.Name;
            prod.Category = vm.Product.Category;
            prod.Discount = vm.Product.Discount;
            prod.ImagePath = vm.Product.ImagePath;
            return RedirectToAction("Index", "Product");
        }

        public IActionResult Delete(int myid)
        {
            myid -= 1;
            var prod = Products[myid];
            Products.Remove(prod);
            for (int i = (myid); i < Products.Count; i++)
            {
                Products[i].Id--;
            }
            return RedirectToAction("Index", "Product");
        }

        [HttpGet]
        public IActionResult Add()
        {
            var vm = new ProductAddViewModel
            {
                Product = new Product(),
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(ProductAddViewModel vm)
        {
            Products.Add(vm.Product);
            vm.Product.Id = Products.Count;
            return RedirectToAction("index");
        }
    }
}
