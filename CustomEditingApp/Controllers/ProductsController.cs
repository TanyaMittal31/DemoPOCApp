using CustomEditingApp.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Telerik.SvgIcons;

namespace CustomEditingApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly List<Product> _products = new List<Product>
    {
        new Product { ProductID = 1, ProductName = "Chai", UnitPrice = 18, UnitsInStock = 39 },
        new Product { ProductID = 2, ProductName = "Chang", UnitPrice = 19, UnitsInStock = 17 },
        // Add more products as needed
    };


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductsRead([DataSourceRequest] DataSourceRequest request)
        {
            /*return View(_products.ToDataSourceResult());*/
            var result = _products.ToDataSourceResult(request);
            return Json(new
            {
                Data = result.Data,
            });
        }

        [HttpPost]
        public IActionResult UpdateProduct([DataSourceRequest] DataSourceRequest request, [FromBody] Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;
            }
            return Json(product);
        }

    }
}
