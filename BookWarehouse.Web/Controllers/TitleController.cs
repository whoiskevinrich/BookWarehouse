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
    public class TitleController : Controller
    {
        private readonly IWarehouseService _warehouses;
        private readonly ITitleService _titles;
        private readonly IInventoryItemService _inventoryItems;

        public TitleController(IWarehouseService warehouses, ITitleService titles, IInventoryItemService inventoryItems)
        {
            _warehouses = warehouses;
            _titles = titles;
            _inventoryItems = inventoryItems;
        }

        // GET: Title
        public ActionResult Index()
        {
            var titles = _titles.GetAll();
            return View(Mapper.Map<IEnumerable<TitleModel>>(titles.ToList()));
        }

        // GET: Title/Details/5
        public ActionResult Details(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var title = _titles.Get(id.Value);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // GET: Title/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Title/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TitleId,Isbn,YearPublished,Name")] Title title)
        {
            if (ModelState.IsValid)
            {
                _titles.Create(new Title
                {
                    Name = title.Name,
                    YearPublished = title.YearPublished,
                    Isbn = title.Isbn,
                });
                return RedirectToAction("Index");
            }

            return View(title);
        }

        // GET: Title/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var title = _titles.Get(id.Value);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Title/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TitleId,Isbn,YearPublished,Name")] Title title)
        {
            if (ModelState.IsValid)
            {
                _titles.Update(title);
                return RedirectToAction("Index");
            }
            return View(title);
        }

        // GET: Title/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var title = _titles.Get(id.Value);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Title/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _titles.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
