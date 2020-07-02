using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemoApp.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MVCDemoApp.Controllers
{
    public class ProductController : Controller
    {

        ProductDataAccessLayer objProduct = new ProductDataAccessLayer();

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> lstProduct = new List<Product>();

            lstProduct = objProduct.GetAllProducts().ToList();

            return View(lstProduct);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Product product)
        {
            if (ModelState.IsValid)
            {
                objProduct.AddProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product objProductNew = objProduct.GetProductData(id);

            if (objProductNew == null)
            {
                return NotFound();
            }
            return View(objProductNew);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind]Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objProduct.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product product = objProduct.GetProductData(id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product product = objProduct.GetEmployeeData(id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

    }
}
