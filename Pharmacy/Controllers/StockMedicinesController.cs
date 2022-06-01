using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pharmacy.Models;

namespace Pharmacy.Controllers
{
    public class StockMedicinesController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: StockMedicines
        public ActionResult Index()
        {
            var stockMedicines = db.StockMedicines.Include(s => s.Medicine).Include(s => s.Shelf);
            return View(stockMedicines.ToList());
        }

        // GET: StockMedicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMedicine stockMedicine = db.StockMedicines.Find(id);
            if (stockMedicine == null)
            {
                return HttpNotFound();
            }
            return View(stockMedicine);
        }

        // GET: StockMedicines/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Medicines, "Id", "Name");
            ViewBag.ShelfId = new SelectList(db.Shelves, "Id", "ShelfLetter");
            return View();
        }

        // POST: StockMedicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,ShelfId")] StockMedicine stockMedicine)
        {
            if (ModelState.IsValid)
            {
                db.StockMedicines.Add(stockMedicine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Medicines, "Id", "Name", stockMedicine.Id);
            ViewBag.ShelfId = new SelectList(db.Shelves, "Id", "ShelfLetter", stockMedicine.ShelfId);
            return View(stockMedicine);
        }

        // GET: StockMedicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMedicine stockMedicine = db.StockMedicines.Find(id);
            if (stockMedicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Medicines, "Id", "Name", stockMedicine.Id);
            ViewBag.ShelfId = new SelectList(db.Shelves, "Id", "ShelfLetter", stockMedicine.ShelfId);
            return View(stockMedicine);
        }

        // POST: StockMedicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,ShelfId")] StockMedicine stockMedicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stockMedicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Medicines, "Id", "Name", stockMedicine.Id);
            ViewBag.ShelfId = new SelectList(db.Shelves, "Id", "ShelfLetter", stockMedicine.ShelfId);
            return View(stockMedicine);
        }

        // GET: StockMedicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StockMedicine stockMedicine = db.StockMedicines.Find(id);
            if (stockMedicine == null)
            {
                return HttpNotFound();
            }
            return View(stockMedicine);
        }

        // POST: StockMedicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StockMedicine stockMedicine = db.StockMedicines.Find(id);
            db.StockMedicines.Remove(stockMedicine);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
