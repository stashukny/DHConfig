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
    public class DIMsController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DIMs
        public ActionResult Index()
        {
            var dIMs = db.DIMs.Include(d => d.CONFIG);
            return View(dIMs.ToList());
        }

        // GET: DIMs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM dIM = db.DIMs.Find(id);
            if (dIM == null)
            {
                return HttpNotFound();
            }
            return View(dIM);
        }

        // GET: DIMs/Create
        public ActionResult Create()
        {
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA");
            return View();
        }

        // POST: DIMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DIM_TYPE_GUID,DIM_COMMON_NAME,DIM_TABLE_SCHEMA,DIM_TABLE_NAME,DIM_LOAD_PROC_SCHEMA_RAW,DIM_LOAD_PROC_NAME_RAW,DIM_TABLE_CLEAN_SCHEMA,DIM_TABLE_CLEAN_NAME,DIM_PROC_UI_CLEAN_SCHEMA,DIM_PROC_UI_CLEAN_NAME,DIM_LOAD_PROC_CLEAN_SCHEMA,DIM_LOAD_PROC_CLEAN_NAME,DIM_VIEW_WHITELIST_SCHEMA,DIM_VIEW_WHITELIST_NAME,DIM_VIEW_RAW_SCHEMA,DIM_VIEW_RAW_NAME,DIM_VIEW_CLEAN_SCHEMA,DIM_VIEW_CLEAN_NAME,DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA,DIM_PROC_RAW_TABLE_CLEAN_ID_NAME,DIM_FEATURE,DIM_TAXONOMY_PROC_SCHEMA,DIM_TAXONOMY_PROC_NAME,DIM_LOAD_PRE_PROC_SPROC_SCHEMA,DIM_LOAD_PRE_PROC_SPROC_NAME,DIM_LOAD_POST_PROC_SCHEMA,DIM_LOAD_POST_PROC_NAME,DIM_LOAD_PRE_PROC_CLEAN_SCHEMA,DIM_LOAD_PRE_PROC_CLEAN_NAME,DIM_LOAD_POST_PROC_CLEAN_SCHEMA,DIM_LOAD_POST_PROC_CLEAN_NAME,IS_STATIC,IS_AUTO_GENERATED")] DIM dIM)
        {
            if (ModelState.IsValid)
            {
                db.DIMs.Add(dIM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dIM.CONFIG_COMMON_NAME);
            return View(dIM);
        }

        // GET: DIMs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM dIM = db.DIMs.Find(id);
            if (dIM == null)
            {
                return HttpNotFound();
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dIM.CONFIG_COMMON_NAME);
            return View(dIM);
        }

        // POST: DIMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DIM_TYPE_GUID,DIM_COMMON_NAME,DIM_TABLE_SCHEMA,DIM_TABLE_NAME,DIM_LOAD_PROC_SCHEMA_RAW,DIM_LOAD_PROC_NAME_RAW,DIM_TABLE_CLEAN_SCHEMA,DIM_TABLE_CLEAN_NAME,DIM_PROC_UI_CLEAN_SCHEMA,DIM_PROC_UI_CLEAN_NAME,DIM_LOAD_PROC_CLEAN_SCHEMA,DIM_LOAD_PROC_CLEAN_NAME,DIM_VIEW_WHITELIST_SCHEMA,DIM_VIEW_WHITELIST_NAME,DIM_VIEW_RAW_SCHEMA,DIM_VIEW_RAW_NAME,DIM_VIEW_CLEAN_SCHEMA,DIM_VIEW_CLEAN_NAME,DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA,DIM_PROC_RAW_TABLE_CLEAN_ID_NAME,DIM_FEATURE,DIM_TAXONOMY_PROC_SCHEMA,DIM_TAXONOMY_PROC_NAME,DIM_LOAD_PRE_PROC_SPROC_SCHEMA,DIM_LOAD_PRE_PROC_SPROC_NAME,DIM_LOAD_POST_PROC_SCHEMA,DIM_LOAD_POST_PROC_NAME,DIM_LOAD_PRE_PROC_CLEAN_SCHEMA,DIM_LOAD_PRE_PROC_CLEAN_NAME,DIM_LOAD_POST_PROC_CLEAN_SCHEMA,DIM_LOAD_POST_PROC_CLEAN_NAME,IS_STATIC,IS_AUTO_GENERATED")] DIM dIM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dIM.CONFIG_COMMON_NAME);
            return View(dIM);
        }

        // GET: DIMs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM dIM = db.DIMs.Find(id);
            if (dIM == null)
            {
                return HttpNotFound();
            }
            return View(dIM);
        }

        // POST: DIMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DIM dIM = db.DIMs.Find(id);
            db.DIMs.Remove(dIM);
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
