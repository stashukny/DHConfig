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
    public class DATA_SOURCEController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DATA_SOURCE
        public ActionResult Index()
        {
            var dATA_SOURCE = db.DATA_SOURCE.Include(d => d.CONFIG).Include(d => d.DATA_SOURCE_TYPE);   
            return View(dATA_SOURCE.ToList());
        }

        // GET: DATA_SOURCE/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {

            DATA_SOURCE dATA_SOURCE = db.DATA_SOURCE.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME);
            if (dATA_SOURCE == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE);
        }

        // GET: DATA_SOURCE/Create
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            var features = db.BITWISE_DICTIONARY
                        .Where(f => f.BITWISE_GROUP == "DATASOURCE_ATTRIBUTES")
                        .ToList()
                        .Select(c => new
                        {
                            DIM_FEATURE = c.BITWISE_KEY,
                            DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                        });

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");
            ViewBag.DATA_SOURCE_TABLE_SCHEMA = new SelectList(db.vSCHEMAS, "name", "name");
            ViewBag.DATA_SOURCE_TYPE_GUID = new SelectList(db.vDATA_SOURCE_TYPE_WITH_PARENT, "DATA_SOURCE_TYPE_GUID", "DATA_SOURCE_TYPE_NAME_WITH_PARENT");                        
            return View();
        }

        // POST: DATA_SOURCE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,DATA_SOURCE_TYPE_GUID,DATA_SOURCE_TABLE_SCHEMA,DATA_SOURCE_TABLE_NAME,DATA_SOURCE_RAW_VIEW_SCHEMA,DATA_SOURCE_RAW_VIEW_NAME,DATA_SOURCE_TABLE_PROC_UPDATE_SCHEMA,DATA_SOURCE_TABLE_PROC_UPDATE_NAME,DATA_SOURCE_TABLE_PROC_INSERT_SCHEMA,DATA_SOURCE_TABLE_PROC_INSERT_NAME,DATA_SOURCE_TABLE_PROC_DELETE_SCHEMA,DATA_SOURCE_TABLE_PROC_DELETE_NAME,DATA_SOURCE_TABLE_PROC_DDL_PARENT_SCHEMA,DATA_SOURCE_TABLE_PROC_DDL_PARENT_NAME,DATA_SOURCE_RAW_UI_VIEW_SCHEMA,DATA_SOURCE_RAW_UI_VIEW_NAME,DATA_SOURCE_FEATURE,DATA_SOURCE_TEST_DATA_PROC_SCHEMA,DATA_SOURCE_TEST_DATA_PROC_NAME")] DATA_SOURCE dATA_SOURCE)
        {
            if (ModelState.IsValid)
            {
                db.DATA_SOURCE.Add(dATA_SOURCE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }            
            
            return View(dATA_SOURCE);
        }

        // GET: DATA_SOURCE/Edit/5
        [ImportModelStateFromTempData]
        public ActionResult Edit(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {

            DATA_SOURCE dATA_SOURCE = db.DATA_SOURCE.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME);
            if (dATA_SOURCE == null)
            {
                return HttpNotFound();
            }

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DATASOURCE_ATTRIBUTES")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");            
            ViewBag.DATA_SOURCE_TABLE_SCHEMA = new SelectList(db.vSCHEMAS, "name", "name");
            ViewBag.DATA_SOURCE_TYPE_GUID = new SelectList(db.vDATA_SOURCE_TYPE_WITH_PARENT, "DATA_SOURCE_TYPE_GUID", "DATA_SOURCE_TYPE_NAME_WITH_PARENT");      
            return View(dATA_SOURCE);
        }

        // POST: DATA_SOURCE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,DATA_SOURCE_TYPE_GUID,DATA_SOURCE_TABLE_SCHEMA,DATA_SOURCE_TABLE_NAME,DATA_SOURCE_RAW_VIEW_SCHEMA,DATA_SOURCE_RAW_VIEW_NAME,DATA_SOURCE_TABLE_PROC_UPDATE_SCHEMA,DATA_SOURCE_TABLE_PROC_UPDATE_NAME,DATA_SOURCE_TABLE_PROC_INSERT_SCHEMA,DATA_SOURCE_TABLE_PROC_INSERT_NAME,DATA_SOURCE_TABLE_PROC_DELETE_SCHEMA,DATA_SOURCE_TABLE_PROC_DELETE_NAME,DATA_SOURCE_TABLE_PROC_DDL_PARENT_SCHEMA,DATA_SOURCE_TABLE_PROC_DDL_PARENT_NAME,DATA_SOURCE_RAW_UI_VIEW_SCHEMA,DATA_SOURCE_RAW_UI_VIEW_NAME,DATA_SOURCE_FEATURE,DATA_SOURCE_TEST_DATA_PROC_SCHEMA,DATA_SOURCE_TEST_DATA_PROC_NAME")] DATA_SOURCE dATA_SOURCE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dATA_SOURCE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dATA_SOURCE.CONFIG_COMMON_NAME);
            ViewBag.DATA_SOURCE_TYPE_GUID = new SelectList(db.DATA_SOURCE_TYPE, "DATA_SOURCE_TYPE_GUID", "MODIFIED_BY", dATA_SOURCE.DATA_SOURCE_TYPE_GUID);
            return View(dATA_SOURCE);
        }

        // GET: DATA_SOURCE/Delete/5
        [ImportModelStateFromTempData]
        public ActionResult Delete(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {

            DATA_SOURCE dATA_SOURCE = db.DATA_SOURCE.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME);
            if (dATA_SOURCE == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE);
        }

        // POST: DATA_SOURCE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {
            DATA_SOURCE dATA_SOURCE = db.DATA_SOURCE.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME);
            db.DATA_SOURCE.Remove(dATA_SOURCE);
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
