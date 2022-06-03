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
    public class PurchasesController : Controller
    {
        private ContextDB db = new ContextDB();

        // GET: Purchases
        public ActionResult Index()
        {
            var purchases = db.Purchases.Include(p => p.Medicine);
            return View(purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // GET: Purchases/Create
        public ActionResult Create()
        {
            LoadMedicinesDDL();
            return View();
        }

        private void LoadMedicinesDDL()
        {
            Medicine emptyMedicines = new Medicine { Id = -1, Name = "Select any Medicine" };

            var allMedicines = db.Medicines.ToList();
            allMedicines.Insert(0, emptyMedicines);
            ViewBag.MedicineId = new SelectList(allMedicines, "Id", "Name");
        }

        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Purchase,MedicineName,ShelfLetter")] PurchaseView purchaseView)
        {

            LoadMedicinesDDL();
            Purchase purchase = purchaseView.Purchase;
            if (ModelState.IsValid)
            {
                Shelf shelf = db.Shelves
                    .Where(sh => sh.ShelfLetter == purchaseView.ShelfLetter)
                    .FirstOrDefault();

                if (shelf == null)
                {                
                    shelf = new Shelf
                    {
                        ShelfLetter = purchaseView.ShelfLetter
                    };
                    db.Shelves.Add(shelf);
                    db.SaveChanges();
                }

                if (!string.IsNullOrWhiteSpace(purchaseView.MedicineName))
                {
                    Medicine medicine = new Medicine
                    {
                        Name = purchaseView.MedicineName,
                        MedicineType = ""
                    };
                     
                    db.Medicines.Add(medicine);
                    db.SaveChanges();
                    purchase.MedicineId = medicine.Id;

                }


                db.Purchases.Add(purchase);
                db.SaveChanges(); 
                
                StockMedicine stockMedicine = db.StockMedicines.Find(purchase.MedicineId);
                
                if (stockMedicine != null)
                {
                    stockMedicine.Quantity = stockMedicine.Quantity + purchase.Quantity;
                    db.Entry(stockMedicine).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    Medicine Medicine = db.Medicines.Find(purchase.MedicineId);

                    StockMedicine sm = new StockMedicine 
                    { 
                        Id = purchase.MedicineId, 
                        Quantity = purchase.Quantity, 
                        ShelfId = shelf.Id 
                                    
                    };
                    db.StockMedicines.Add(sm);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","StockMedicines");
            }


            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", purchase.MedicineId);
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UnitPrice,ExpiryDate,PurchaseDate,Quantity,Reciept,SupplierName,SupplierContact,MedicineId")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MedicineId = new SelectList(db.Medicines, "Id", "Name", purchase.MedicineId);
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
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
