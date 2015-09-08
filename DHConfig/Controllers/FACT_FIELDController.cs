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
    public class FACT_FIELDController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: FACT_FIELD
        public ActionResult Index()
        {
            string sClient = Session["sClient"].ToString();
            var fACT_FIELD = db.FACT_FIELD
                .Where(w => w.CONFIG_COMMON_NAME == sClient)
                .Include(f => f.CONFIG)
                .Include(f => f.FACT);
            return View(fACT_FIELD.ToList());
        }

        // GET: FACT_FIELD/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {

            FACT_FIELD fACT_FIELD = db.FACT_FIELD.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME);
            if (fACT_FIELD == null)
            {
                return HttpNotFound();
            }
            return View(fACT_FIELD);
        }

        // GET: FACT_FIELD/Create
        public ActionResult Create()
        {
            string sClient = Session["sClient"].ToString();
            ViewBag.CONFIG_COMMON_NAME = sClient;            
            var factCommonNames = db.FACTs
            .Where(f => f.CONFIG_COMMON_NAME == sClient)
            .ToList();

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "FACT_FIELDS")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");

            ViewBag.listFactCommonName = new SelectList(factCommonNames, "FACT_COMMON_NAME", "FACT_COMMON_NAME");            

            return View();
        }

        // POST: FACT_FIELD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,FACT_COMMON_NAME,FACT_FIELD_NAME,OBJECT_TYPE_NAME,DIM_FIELD_NAME,FACT_FIELD_FEATURE")] FACT_FIELD fACT_FIELD)
        {
            if (ModelState.IsValid)
            {
                db.FACT_FIELD.Add(fACT_FIELD);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.FACTs, "CONFIG_COMMON_NAME", "DATA_SOURCE_NAME", fACT_FIELD.CONFIG_COMMON_NAME);
            return View(fACT_FIELD);
        }

        // GET: FACT_FIELD/Edit/5
        public ActionResult Edit(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {

            FACT_FIELD fACT_FIELD = db.FACT_FIELD.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME);
            if (fACT_FIELD == null)
            {
                return HttpNotFound();
            }

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "FACT_FIELDS")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.FACTs, "CONFIG_COMMON_NAME", "DATA_SOURCE_NAME", fACT_FIELD.CONFIG_COMMON_NAME);
            return View(fACT_FIELD);
        }

        // POST: FACT_FIELD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,FACT_COMMON_NAME,FACT_FIELD_NAME,OBJECT_TYPE_NAME,DIM_FIELD_NAME,FACT_FIELD_FEATURE")] FACT_FIELD fACT_FIELD)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fACT_FIELD).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.FACTs, "CONFIG_COMMON_NAME", "DATA_SOURCE_NAME", fACT_FIELD.CONFIG_COMMON_NAME);
            return View(fACT_FIELD);
        }

        // GET: FACT_FIELD/Delete/5
        public ActionResult Delete(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {

            FACT_FIELD fACT_FIELD = db.FACT_FIELD.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME);
            if (fACT_FIELD == null)
            {
                return HttpNotFound();
            }
            return View(fACT_FIELD);
        }

        // POST: FACT_FIELD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {
            FACT_FIELD fACT_FIELD = db.FACT_FIELD.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME);
            db.FACT_FIELD.Remove(fACT_FIELD);
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
