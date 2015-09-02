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
    public class DATA_SOURCE_FIELDController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DATA_SOURCE_FIELD
        public ActionResult Index()
        {
            var dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Include(d => d.CONFIG).Include(d => d.DATA_SOURCE);            
            return View(dATA_SOURCE_FIELD.ToList());
        }

        // GET: DATA_SOURCE_FIELD/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {

            DATA_SOURCE_FIELD dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME);
            if (dATA_SOURCE_FIELD == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE_FIELD);
        }

        // GET: DATA_SOURCE_FIELD/Create
        public ActionResult Create()
        {
            string sClient = Session["sClient"].ToString();

            var datasources = db.DATA_SOURCE
            .Where(f => f.CONFIG_COMMON_NAME == sClient)
            .ToList();

            ViewBag.listDataSources = new SelectList(datasources, "DATA_SOURCE_NAME", "DATA_SOURCE_NAME");            

            //ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA");
            //ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA");
            return View();
        }

        // POST: DATA_SOURCE_FIELD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,SOURCE_COLUMN_NAME,SOURCE_COLUMN_DATA_TYPE,RAW_VIEW_COLUMN_NAME,RAW_VIEW_COLUMN_DATA_TYPE,IS_DIM_HASH,IS_IDENTITY,DERIVED_CONFIGURATION,COLUMN_NAME,DATA_SOURCE_FIELD_FEATURE")] DATA_SOURCE_FIELD dATA_SOURCE_FIELD)
        {
            if (ModelState.IsValid)
            {
                db.DATA_SOURCE_FIELD.Add(dATA_SOURCE_FIELD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            return View(dATA_SOURCE_FIELD);
        }

        // GET: DATA_SOURCE_FIELD/Edit/5
        public ActionResult Edit(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            
            //string COLUMN_NAME = !String.IsNullOrEmpty(SOURCE_COLUMN_NAME) ? SOURCE_COLUMN_NAME: RAW_VIEW_COLUMN_NAME;

            DATA_SOURCE_FIELD dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME);
            if (dATA_SOURCE_FIELD == null)
            {
                return HttpNotFound();
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            return View(dATA_SOURCE_FIELD);
        }

        // POST: DATA_SOURCE_FIELD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,SOURCE_COLUMN_NAME,SOURCE_COLUMN_DATA_TYPE,RAW_VIEW_COLUMN_NAME,RAW_VIEW_COLUMN_DATA_TYPE,IS_DIM_HASH,IS_IDENTITY,DERIVED_CONFIGURATION,COLUMN_NAME,DATA_SOURCE_FIELD_FEATURE")] DATA_SOURCE_FIELD dATA_SOURCE_FIELD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dATA_SOURCE_FIELD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            return View(dATA_SOURCE_FIELD);
        }

        // GET: DATA_SOURCE_FIELD/Delete/5
        public ActionResult Delete(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            DATA_SOURCE_FIELD dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME);
            if (dATA_SOURCE_FIELD == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE_FIELD);
        }

        // POST: DATA_SOURCE_FIELD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            DATA_SOURCE_FIELD dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME);
            db.DATA_SOURCE_FIELD.Remove(dATA_SOURCE_FIELD);
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
