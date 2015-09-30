using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHConfig;
using System.Data.Entity.Validation;

namespace DHConfig.Controllers
{
    public class FACTsController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: FACTs
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            string sClient = Session["sClient"].ToString();
            var fACTs = db.FACTs
                .Where(w => w.CONFIG_COMMON_NAME == sClient)
                .Include(f => f.CONFIG)
                .Include(f => f.DATA_SOURCE);            
            return View(fACTs.ToList());

            
        }

        // GET: FACTs/Details/5
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            string sClient = Session["sClient"].ToString();
            
            //FACT fact = new FACT();
            //fact.CONFIG_COMMON_NAME = sClient;

            //ViewBag.CONFIG_COMMON_NAME = sClient;
            var datasources = db.DATA_SOURCE
            .Where(f => f.CONFIG_COMMON_NAME == sClient)
            .ToList();

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "FACT")
            .ToList()
            .Select(c => new
            {
                FACT_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            ViewBag.listFeatures = new MultiSelectList(features, "FACT_FEATURE", "DESCR");

            ViewBag.listDataSources = new SelectList(datasources, "DATA_SOURCE_NAME", "DATA_SOURCE_NAME");
            ViewBag.FACT_TABLE_SCHEMA = new SelectList(db.vSCHEMAS, "name", "name");
            return View();
        }

        // POST: FACTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,FACT_COMMON_NAME,FACT_TABLE_SCHEMA,FACT_TABLE_NAME,FACT_FEATURE,DISTINCT_TABLE_KEY_SCHEMA,DISTINCT_TABLE_KEY_NAME,DISTINCT_TABLE_VALUE_SCHEMA,DISTINCT_TABLE_VALUE_NAME,DISTINCT_VALUE_PROCEDURE_SCHEMA,DISTINCT_VALUE_PROCEDURE_NAME,DISTINCT_KEY_PROCEDURE_SCHEMA,DISTINCT_KEY_PROCEDURE_NAME,FACT_LOAD_PROCEDURE_SCHEMA,FACT_LOAD_PROCEDURE_NAME,FACT_PRE_EXEC_SPROC_SCHEMA,FACT_PRE_EXEC_SPROC_NAME,FACT_POST_EXEC_SPROC_SCHEMA,FACT_POST_EXEC_SPROC_NAME,IS_AUTO_GENERATED_FACT_TABLE,IS_AUTO_GENERATED_LOAD_PROCEDURE")] FACT fACT, string[] SelectedItems, string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {
            fACT.CONFIG_COMMON_NAME = Session["sClient"].ToString();
            if (SelectedItems != null)
            {
                string feature = fACT.FACT_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "FACT", db);
                fACT.FACT_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot create due to selection of invalid features.");
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.FACTs.Add(fACT);
                    db.SaveChanges();
                }

                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME, FACT_COMMON_NAME });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(fACT);
        }

        // GET: FACTs/Edit/5
        [ImportModelStateFromTempData]
        [SessionExpireFilterAttribute]        
        public ActionResult Edit(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {

            FACT fACT = db.FACTs.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME);
            if (fACT == null)
            {
                return HttpNotFound();
            }

            var datasources = db.DATA_SOURCE
            .Where(f => f.CONFIG_COMMON_NAME == CONFIG_COMMON_NAME)
            .ToList();

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "FACT")
            .ToList()
            .Select(c => new
            {
                FACT_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            ViewBag.listFeatures = new MultiSelectList(features, "FACT_FEATURE", "DESCR");
            ViewBag.listDataSources = new SelectList(datasources, "DATA_SOURCE_NAME", "DATA_SOURCE_NAME");       

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", fACT.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", fACT.CONFIG_COMMON_NAME);
            ViewBag.FACT_TABLE_SCHEMA = new SelectList(db.vSCHEMAS, "name", "name");
            return View(fACT);
        }

        // POST: FACTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ExportModelStateToTempData]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]        
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,FACT_COMMON_NAME,FACT_TABLE_SCHEMA,FACT_TABLE_NAME,FACT_FEATURE,DISTINCT_TABLE_KEY_SCHEMA,DISTINCT_TABLE_KEY_NAME,DISTINCT_TABLE_VALUE_SCHEMA,DISTINCT_TABLE_VALUE_NAME,DISTINCT_VALUE_PROCEDURE_SCHEMA,DISTINCT_VALUE_PROCEDURE_NAME,DISTINCT_KEY_PROCEDURE_SCHEMA,DISTINCT_KEY_PROCEDURE_NAME,FACT_LOAD_PROCEDURE_SCHEMA,FACT_LOAD_PROCEDURE_NAME,FACT_PRE_EXEC_SPROC_SCHEMA,FACT_PRE_EXEC_SPROC_NAME,FACT_POST_EXEC_SPROC_SCHEMA,FACT_POST_EXEC_SPROC_NAME,IS_AUTO_GENERATED_FACT_TABLE,IS_AUTO_GENERATED_LOAD_PROCEDURE")] FACT fACT, string[] SelectedItems, string CONFIG_COMMON_NAME, string FACT_COMMON_NAME)
        {
            if (SelectedItems != null)
            {
                string feature = fACT.FACT_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "FACT", db);
                fACT.FACT_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot Edit due to invalid Features.");
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString() });
                }             
            }

            if (ModelState.IsValid)
            {
                //TODO: come back here
                string oldFACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString();
                string oldCONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();


                if (oldFACT_COMMON_NAME != fACT.FACT_COMMON_NAME || oldCONFIG_COMMON_NAME != fACT.CONFIG_COMMON_NAME)
                {

                    var facts = db.FACTs.Where(a => a.FACT_COMMON_NAME == oldFACT_COMMON_NAME && a.CONFIG_COMMON_NAME == oldCONFIG_COMMON_NAME);

                    foreach (var f in facts)
                    {
                        db.FACTs.Remove(f);
                    }

                    db.FACTs.Add(fACT);
                }
                else
                {
                    db.Entry(fACT).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw e;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString() });
                }

                return RedirectToAction("Index");
            }
            
            return View(fACT);
        }

        // GET: FACTs/Delete/5
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
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
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
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
