using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using Pharmacy.Models;
using Services;

namespace Pharmacy.Controllers
{
    public class SalesController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.StockMedicine);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.StockMedicineId = new SelectList(db.StockMedicines.Include(s => s.Medicine).Select(sm=>sm.Medicine), "Id", "Name");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SalesDate,Quantity,CustomerContact,CustomerName,Price,Amount,Discount,StockMedicineId")] Sales sales)
        {

            var items = db.StockMedicines.Include(s => s.Medicine).Select(sm => sm.Medicine).ToList();
            if (ModelState.IsValid)
            {

                db.Sales.Add(sales);
                db.SaveChanges();
                StockMedicine stockMedicine = db.StockMedicines.Find(sales.StockMedicineId);

                if (stockMedicine != null)
                {
                    stockMedicine.Quantity = stockMedicine.Quantity - sales.Quantity;
                    db.Entry(stockMedicine).State = EntityState.Modified;
                    db.SaveChanges();
                    if (stockMedicine.Quantity <= stockMedicine.MinQuantity)
                    {
                        var medicineName = items.Find(m => m.Id == stockMedicine.Id).Name;
                       new StockAlertService().SendNotification(medicineName, stockMedicine.Quantity);
                    }  
                    
                    return RedirectToAction("Index");
                }

            }

            ViewBag.StockMedicineId = new SelectList(items, "Id", "Name");
            return View(sales);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.StockMedicineId = new SelectList
             (db.StockMedicines.Include(s => s.Medicine).Select(sm => sm.Medicine), "Id", "Name");


            return View(sales);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SalesDate,Quantity,CustomerContact,CustomerName,Price,Amount,Discount,StockMedicineId")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StockMedicineId = new SelectList(db.StockMedicines, "Id", "Id", sales.StockMedicineId);
            return View(sales);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sales sales = db.Sales.Find(id);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sales sales = db.Sales.Find(id);
            db.Sales.Remove(sales);
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
