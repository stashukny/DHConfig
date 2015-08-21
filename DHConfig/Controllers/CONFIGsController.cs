using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHConfig;

namespace DHConfig.Controllers
{
    public class CONFIGsController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: CONFIGs
        public ActionResult Index()
        {
            return View(db.CONFIGs.ToList());
        }

        // GET: CONFIGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFIG cONFIG = db.CONFIGs.Find(id);
            if (cONFIG == null)
            {
                return HttpNotFound();
            }
            return View(cONFIG);
        }

        // GET: CONFIGs/Create
        [HttpGet]
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            CONFIG cONFIG = new CONFIG();
            cONFIG.CONFIG_DATA_PROCESS_PROC_NAME = "EASY_BUTTON_PROCESS";
            cONFIG.CONFIG_DATA_PROCESS_PROC_SCHEMA = "DBO";
            return View(cONFIG);
        }

        // POST: CONFIGs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,CONFIG_DATA_PROCESS_PROC_SCHEMA,CONFIG_DATA_PROCESS_PROC_NAME,CONFIG_DATA_SYNC_PROC_SCHEMA,CONFIG_DATA_SYNC_PROC_NAME")] CONFIG cONFIG)
        {
                if (ModelState.IsValid)
                {
                    db.CONFIGs.Add(cONFIG);
                    try
                    {
                    db.SaveChanges();
                    }

                    catch (Exception ex)
                    {
                        ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                        return RedirectToAction("Create", "CONFIGs");
                    }
                }

                return RedirectToAction("Index", "Home", new { SelectedClient = cONFIG.CONFIG_COMMON_NAME });
            
        }

        // GET: CONFIGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFIG cONFIG = db.CONFIGs.Find(id);
            if (cONFIG == null)
            {
                return HttpNotFound();
            }
            return View(cONFIG);
        }

        // POST: CONFIGs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,CONFIG_DATA_PROCESS_PROC_SCHEMA,CONFIG_DATA_PROCESS_PROC_NAME,CONFIG_DATA_SYNC_PROC_SCHEMA,CONFIG_DATA_SYNC_PROC_NAME")] CONFIG cONFIG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cONFIG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(cONFIG);
        }

        // GET: CONFIGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CONFIG cONFIG = db.CONFIGs.Find(id);
            if (cONFIG == null)
            {
                return HttpNotFound();
            }
            return View(cONFIG);
        }

        // POST: CONFIGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CONFIG cONFIG = db.CONFIGs.Find(id);
            db.CONFIGs.Remove(cONFIG);
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
