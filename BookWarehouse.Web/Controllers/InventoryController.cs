using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using BookWarehouse.Core.Domain;
using BookWarehouse.Core.Service;
using BookWarehouse.Web.Models;

namespace BookWarehouse.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IInventoryItemService _inventory;
        private readonly ITitleService _titles;
        private readonly IWarehouseService _warehouses;

        public InventoryController(IInventoryItemService inventory, ITitleService titles, IWarehouseService warehouses)
        {
            _inventory = inventory;
            _titles = titles;
            _warehouses = warehouses;
        }

        // GET: Warehouse
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            var inventoryItems = _inventory.GetAll();
            var model = Mapper.Map<IEnumerable<InventoryItemModel>>(inventoryItems.ToList());

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(x => x.Title.Name);
                    break;
                case "Price":
                    model = model.OrderBy(x => x.Price);
                    break;
                case "price_desc":
                    model = model.OrderByDescending(x => x.Price);
                    break;
                default:
                    model = model.OrderBy(x => x.Title.Name);
                    break;
            }

            return View(model);
        }

        public ActionResult List(Guid id)
        {
            var inventoryItems = _inventory.Where(x => x.TitleId == id);
            return View("Index", Mapper.Map<IEnumerable<InventoryItemModel>>(inventoryItems.ToList()));
        }

        // GET: Warehouse/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _inventory.Get(id.Value);

            if (item == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<InventoryItemModel>(item));
        }

        // GET: Warehouse/Create
        public ActionResult Create()
        {
            ViewBag.TitleId = new SelectList(_titles.GetAll(), "TitleId", "Name");
            ViewBag.WarehouseId = new SelectList(_warehouses.GetAll(), "WarehouseId", "Name");
            return View();
        }

        // POST: Warehouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InventoryItemId,Edition,Quality,Price,TitleId,WarehouseId")] InventoryItemModel inventoryItem)
        {
            if (ModelState.IsValid)
            {
                _inventory.Create(new InventoryItem
                {
                    TitleId = inventoryItem.TitleId,
                    WarehouseId = inventoryItem.WarehouseId,
                    Edition = inventoryItem.Edition,
                    Price = inventoryItem.Price,
                    Quality = inventoryItem.Quality
                });

                return RedirectToAction("Index");
            }

            ViewBag.TitleId = new SelectList(_titles.GetAll(), "TitleId", "Name", inventoryItem.TitleId);
            ViewBag.WarehouseId = new SelectList(_warehouses.GetAll(), "WarehouseId", "Name", inventoryItem.WarehouseId);
            return View(inventoryItem);
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _inventory.Get(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.TitleId = new SelectList(_titles.GetAll(), "TitleId", "Name", item.TitleId);
            ViewBag.WarehouseId = new SelectList(_warehouses.GetAll(), "WarehouseId", "Name", item.WarehouseId);
            return View(item);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InventoryItemId,Edition,Quality,Price,TitleId,WarehouseId")] InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                _inventory.Update(inventoryItem);
                return RedirectToAction("Index");
            }
            ViewBag.TitleId = new SelectList(_titles.GetAll(), "TitleId", "Name", inventoryItem.TitleId);
            ViewBag.WarehouseId = new SelectList(_warehouses.GetAll(), "WarehouseId", "Name", inventoryItem.WarehouseId);
            return View(inventoryItem);
        }

        // GET: Warehouse/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var item = _inventory.Get(id.Value);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _inventory.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
