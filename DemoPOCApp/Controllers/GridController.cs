using DemoPOCApp.Data;
using DemoPOCApp.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using static NuGet.Packaging.PackagingConstants;

namespace DemoPOCApp.Controllers
{
    public class GridController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public GridController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = Enumerable.Range(0, 50).Select(i => new OrderViewModel
            {
                OrderID = i,
                Freight = i * 10,
                OrderDate = new DateTime(2016, 9, 15).AddDays(i % 7),
                ShipName = "ShipName " + i,
                ShipCity = "ShipCity " + i
            });

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
            //var orders = _dbContext.Orders.Select(o => new
            //{
            //    o.OrderID,
            //    o.OrderDate,
            //    o.ShipName,
            //    o.ShipCity
            //});

            //return Json(orders.ToDataSourceResult(request), System.Web.Mvc.JsonRequestBehavior.AllowGet);
        }

        public IActionResult ProductsRead([DataSourceRequest] DataSourceRequest request)
        {

            var result = _dbContext.Orders.ToDataSourceResult(request);
            return Json(new
            {
                Data = result.Data,
            });
        }

        [HttpPost]
        public IActionResult Orders_Destroy([DataSourceRequest] DataSourceRequest request, Order order)
        {
            if (ModelState.IsValid)
            {
                var entity = _dbContext.Orders.FirstOrDefault(o => o.OrderID == order.OrderID);
                if (entity != null)
                {
                    _dbContext.Orders.Remove(entity);
                    _dbContext.SaveChanges();  // Save changes to commit deletion
                }
            }

            return Json(new[] { order }.ToDataSourceResult(request, ModelState));
        }
    }
}
