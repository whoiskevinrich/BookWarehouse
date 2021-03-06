﻿using System;
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
            var warehouses = _warehouses.GetAll();
            return View(Mapper.Map<IEnumerable<WarehouseModel>>(warehouses.ToList()));
        }

        // GET: Warehouse/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var warehouse = _warehouses.Get(id.Value);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<WarehouseModel>(warehouse));
        }

        // GET: Warehouse/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Warehouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WarehouseId,Name")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _warehouses.Create(new Warehouse
                {
                    Name = warehouse.Name
                });
                return RedirectToAction("Index");
            }

            return View(Mapper.Map<WarehouseModel>(warehouse));
        }

        // GET: Warehouse/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var warehouse = _warehouses.Get(id.Value);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<WarehouseModel>(warehouse));
        }

        // POST: Warehouse/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WarehouseId,Name")] Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _warehouses.Update(warehouse);
                return RedirectToAction("Index");
            }
            return View(Mapper.Map<WarehouseModel>(warehouse));
        }

        // GET: Warehouse/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var warehouse = _warehouses.Get(id.Value);
            if (warehouse == null)
            {
                return HttpNotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _warehouses.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
