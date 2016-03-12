using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BookWarehouse.Core.Service;

namespace BookWarehouse.Web.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly IWarehouseService _warehouses;

        public WarehouseController(IWarehouseService warehouses)
        {
            _warehouses = warehouses;
        }

        // GET: Warehouse
        public ActionResult Index()
        {
            return View();
        }
    }
}