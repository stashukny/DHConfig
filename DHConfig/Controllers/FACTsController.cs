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
    public class FACTsController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: FACTs
        public ActionResult Index()
        {
            var fACTs = db.FACTs.Include(f => f.CONFIG).Include(f => f.DATA_SOURCE);            
            return View(fACTs.ToList());

            
        }

        // GET: FACTs/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {

            FACT fACT = db.FACTs.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME);
            if (fACT == null)
            {
                return HttpNotFound();
            }
            return View(fACT);
        }

        // GET: FACTs/Create
        public ActionResult Create()
        {
            string sClient = Session["sClient"].ToString();
            
            FACT fact = new FACT();
            fact.CONFIG_COMMON_NAME = sClient;

            ViewBag.CONFIG_COMMON_NAME = sClient;
            var datasources = db.DATA_SOURCE
            .Where(f => f.CONFIG_COMMON_NAME == sClient)
            .ToList();

            ViewBag.listDataSources = new SelectList(datasources, "DATA_SOURCE_NAME", "DATA_SOURCE_NAME");            

            return View(fact);
        }

        // POST: FACTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,FACT_COMMON_NAME,FACT_TABLE_SCHEMA,FACT_TABLE_NAME,FACT_FEATURE,DISTINCT_TABLE_KEY_SCHEMA,DISTINCT_TABLE_KEY_NAME,DISTINCT_TABLE_VALUE_SCHEMA,DISTINCT_TABLE_VALUE_NAME,DISTINCT_VALUE_PROCEDURE_SCHEMA,DISTINCT_VALUE_PROCEDURE_NAME,DISTINCT_KEY_PROCEDURE_SCHEMA,DISTINCT_KEY_PROCEDURE_NAME,FACT_LOAD_PROCEDURE_SCHEMA,FACT_LOAD_PROCEDURE_NAME,FACT_PRE_EXEC_SPROC_SCHEMA,FACT_PRE_EXEC_SPROC_NAME,FACT_POST_EXEC_SPROC_SCHEMA,FACT_POST_EXEC_SPROC_NAME,IS_AUTO_GENERATED_FACT_TABLE,IS_AUTO_GENERATED_LOAD_PROCEDURE")] FACT fACT)
        {
            if (ModelState.IsValid)
            {
                db.FACTs.Add(fACT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", fACT.CONFIG_COMMON_NAME);
            return View(fACT);
        }

        // GET: FACTs/Edit/5
        public ActionResult Edit(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {

            FACT fACT = db.FACTs.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME);
            if (fACT == null)
            {
                return HttpNotFound();
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", fACT.CONFIG_COMMON_NAME);
            return View(fACT);
        }

        // POST: FACTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,FACT_COMMON_NAME,FACT_TABLE_SCHEMA,FACT_TABLE_NAME,FACT_FEATURE,DISTINCT_TABLE_KEY_SCHEMA,DISTINCT_TABLE_KEY_NAME,DISTINCT_TABLE_VALUE_SCHEMA,DISTINCT_TABLE_VALUE_NAME,DISTINCT_VALUE_PROCEDURE_SCHEMA,DISTINCT_VALUE_PROCEDURE_NAME,DISTINCT_KEY_PROCEDURE_SCHEMA,DISTINCT_KEY_PROCEDURE_NAME,FACT_LOAD_PROCEDURE_SCHEMA,FACT_LOAD_PROCEDURE_NAME,FACT_PRE_EXEC_SPROC_SCHEMA,FACT_PRE_EXEC_SPROC_NAME,FACT_POST_EXEC_SPROC_SCHEMA,FACT_POST_EXEC_SPROC_NAME,IS_AUTO_GENERATED_FACT_TABLE,IS_AUTO_GENERATED_LOAD_PROCEDURE")] FACT fACT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fACT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", fACT.CONFIG_COMMON_NAME);
            return View(fACT);
        }

        // GET: FACTs/Delete/5
        public ActionResult Delete(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {
            FACT fACT = db.FACTs.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME);
            if (fACT == null)
            {
                return HttpNotFound();
            }
            return View(fACT);
        }

        // POST: FACTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {
            FACT fACT = db.FACTs.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME);
            db.FACTs.Remove(fACT);
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
