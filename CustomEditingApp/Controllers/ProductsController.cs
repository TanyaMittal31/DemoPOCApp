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

        //ProductID=1&models%5B0%5D.ProductName=Chai&models%5B0%5D.UnitPrice=19&models%5B0%5D.UnitsInStock=39

        // ,int ProductID, string ProductName, decimal UnitPrice, int UnitsInStock

        [HttpPost]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            /*if (ProductID != null && ModelState.IsValid)
            {
                // Find the product in the list and update it
                var existingProduct = _products.FirstOrDefault(p => p.ProductID == ProductID);
                if (existingProduct != null)
                {
                    existingProduct.ProductName = ProductName;
                    existingProduct.UnitPrice = UnitPrice;
                    existingProduct.UnitsInStock = UnitsInStock;
                }
            }

            var product = new Product { ProductID = ProductID, ProductName = ProductName, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock };
*/
            return Json(new[] { product });
        }

        [HttpPost]
        public ActionResult AddProduct([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Product> products)
        {
            var results = new List<Product>();
            if (products != null && ModelState.IsValid)
            {
                foreach (var product in products)
                {
                    //productService.Create(product);
                    _products.Add(product);
                }
            }
            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public async Task<IActionResult> PopupUpdateProduct([DataSourceRequest] DataSourceRequest request, Product product)
        {
            return Json(new[] { product });
        }

        
        /*public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, ProductViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                productService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }*/

    }
}