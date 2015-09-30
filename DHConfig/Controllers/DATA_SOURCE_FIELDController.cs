using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class DATA_SOURCE_FIELDController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DATA_SOURCE_FIELD
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            string sClient = Session["sClient"].ToString();
            var dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD
                .Where(w => w.CONFIG_COMMON_NAME == sClient)
                .Include(d => d.CONFIG)
                .Include(d => d.DATA_SOURCE);
            return View(dATA_SOURCE_FIELD.ToList());
        }

        // GET: DATA_SOURCE_FIELD/Details/5
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
        public ActionResult Create()
        {
            string sClient = Session["sClient"].ToString();

            var datasources = db.DATA_SOURCE
            .Where(f => f.CONFIG_COMMON_NAME == sClient)
            .ToList();

            ViewBag.listDataSources = new SelectList(datasources, "DATA_SOURCE_NAME", "DATA_SOURCE_NAME");

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DATA_SOURCE_FIELDS")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            SelectList datatypes = new SelectList(db.vDATA_TYPES, "DIM_DATA_TYPE", "DIM_DATA_TYPE");
            ViewBag.SOURCE_COLUMN_DATA_TYPE = datatypes;
            ViewBag.RAW_VIEW_COLUMN_DATA_TYPE = datatypes;

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");

            //ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA");
            //ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA");
            return View();
        }

        // POST: DATA_SOURCE_FIELD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,SOURCE_COLUMN_NAME,SOURCE_COLUMN_DATA_TYPE,RAW_VIEW_COLUMN_NAME,RAW_VIEW_COLUMN_DATA_TYPE,IS_DIM_HASH,IS_IDENTITY,DERIVED_CONFIGURATION,COLUMN_NAME,DATA_SOURCE_FIELD_FEATURE")] DATA_SOURCE_FIELD dATA_SOURCE_FIELD, string[] SelectedItems, string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            dATA_SOURCE_FIELD.CONFIG_COMMON_NAME = Session["sClient"].ToString();
            if (SelectedItems != null)
            {
                string feature = dATA_SOURCE_FIELD.DATA_SOURCE_FIELD_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "DATA_SOURCE_FIELDS", db);
                dATA_SOURCE_FIELD.DATA_SOURCE_FIELD_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot create due to selection of invalid features.");
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString(), COLUMN_NAME = Request["COLUMN_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.DATA_SOURCE_FIELD.Add(dATA_SOURCE_FIELD);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dATA_SOURCE_FIELD);
        }

        // GET: DATA_SOURCE_FIELD/Edit/5
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
        public ActionResult Edit(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            //string COLUMN_NAME = !String.IsNullOrEmpty(SOURCE_COLUMN_NAME) ? SOURCE_COLUMN_NAME: RAW_VIEW_COLUMN_NAME;

            DATA_SOURCE_FIELD dATA_SOURCE_FIELD = db.DATA_SOURCE_FIELD.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME, COLUMN_NAME);
            if (dATA_SOURCE_FIELD == null)
            {
                return HttpNotFound();
            }

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DATA_SOURCE_FIELDS")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            SelectList rawdatatypes = new SelectList(db.vDATA_TYPES, "DIM_DATA_TYPE", "DIM_DATA_TYPE", dATA_SOURCE_FIELD.RAW_VIEW_COLUMN_DATA_TYPE);
            SelectList sourcedatatypes = new SelectList(db.vDATA_TYPES, "DIM_DATA_TYPE", "DIM_DATA_TYPE", dATA_SOURCE_FIELD.SOURCE_COLUMN_DATA_TYPE);

            ViewBag.RAW_VIEW_COLUMN_DATA_TYPE = rawdatatypes;
            ViewBag.SOURCE_COLUMN_DATA_TYPE = sourcedatatypes;

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DATA_SOURCE, "CONFIG_COMMON_NAME", "DATA_SOURCE_TABLE_SCHEMA", dATA_SOURCE_FIELD.CONFIG_COMMON_NAME);
            return View(dATA_SOURCE_FIELD);
        }

        // POST: DATA_SOURCE_FIELD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,SOURCE_COLUMN_NAME,SOURCE_COLUMN_DATA_TYPE,RAW_VIEW_COLUMN_NAME,RAW_VIEW_COLUMN_DATA_TYPE,IS_DIM_HASH,IS_IDENTITY,DERIVED_CONFIGURATION,COLUMN_NAME,DATA_SOURCE_FIELD_FEATURE")] DATA_SOURCE_FIELD dATA_SOURCE_FIELD, string[] SelectedItems, string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME, string COLUMN_NAME)
        {
            if (SelectedItems != null)
            {
                string feature = dATA_SOURCE_FIELD.DATA_SOURCE_FIELD_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "DATA_SOURCE_FIELDS", db);
                dATA_SOURCE_FIELD.DATA_SOURCE_FIELD_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot  due to invalid Features.");
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString(), COLUMN_NAME = Request["COLUMN_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                //TODO: come back here
                string oldDATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString();
                string oldCONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();
                string oldCOLUMN_NAME = Request["COLUMN_NAME"].ToString();

                if (oldDATA_SOURCE_NAME != dATA_SOURCE_FIELD.DATA_SOURCE_NAME || oldCONFIG_COMMON_NAME != dATA_SOURCE_FIELD.CONFIG_COMMON_NAME || oldCOLUMN_NAME != dATA_SOURCE_FIELD.COLUMN_NAME)
                {
                    var fact_fields = db.DATA_SOURCE_FIELD.Where(a => a.DATA_SOURCE_NAME == oldDATA_SOURCE_NAME && a.CONFIG_COMMON_NAME == oldCONFIG_COMMON_NAME && a.COLUMN_NAME == oldCOLUMN_NAME);

                    foreach (var f in fact_fields)
                    {
                        db.DATA_SOURCE_FIELD.Remove(f);
                    }

                    db.DATA_SOURCE_FIELD.Add(dATA_SOURCE_FIELD);
                }
                else
                {
                    db.Entry(dATA_SOURCE_FIELD).State = EntityState.Modified;
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
                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString(), COLUMN_NAME = Request["COLUMN_NAME"].ToString() });
                }

                return RedirectToAction("Index");
            }

            return View(dATA_SOURCE_FIELD);
        }

        // GET: DATA_SOURCE_FIELD/Delete/5
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
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
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
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