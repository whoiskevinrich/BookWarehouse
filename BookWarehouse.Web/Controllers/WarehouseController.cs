using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookWarehouse.Web.Controllers
{
    public class WarehouseController : Controller
    {
        // GET: Warehouse
        public ActionResult Index()
        {
            return View();
        }
    }
}