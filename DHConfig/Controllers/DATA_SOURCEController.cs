using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class DATA_SOURCEController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();
        private string feature;

        // GET: DATA_SOURCE
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            string sClient = Session["sClient"].ToString();
            var dATA_SOURCE = db.DATA_SOURCE
                .Where(w => w.CONFIG_COMMON_NAME == sClient)
                .Include(d => d.CONFIG)
                .Include(d => d.DATA_SOURCE_TYPE)
                .Include(p => p.DATA_SOURCE_TYPE.vDATA_SOURCE_TYPE_WITH_PARENT);

            return View(dATA_SOURCE.ToList());
        }

        // GET: DATA_SOURCE/Details/5
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,DATA_SOURCE_TYPE_GUID,DATA_SOURCE_TABLE_SCHEMA,DATA_SOURCE_TABLE_NAME,DATA_SOURCE_RAW_VIEW_SCHEMA,DATA_SOURCE_RAW_VIEW_NAME,DATA_SOURCE_TABLE_PROC_UPDATE_SCHEMA,DATA_SOURCE_TABLE_PROC_UPDATE_NAME,DATA_SOURCE_TABLE_PROC_INSERT_SCHEMA,DATA_SOURCE_TABLE_PROC_INSERT_NAME,DATA_SOURCE_TABLE_PROC_DELETE_SCHEMA,DATA_SOURCE_TABLE_PROC_DELETE_NAME,DATA_SOURCE_TABLE_PROC_DDL_PARENT_SCHEMA,DATA_SOURCE_TABLE_PROC_DDL_PARENT_NAME,DATA_SOURCE_RAW_UI_VIEW_SCHEMA,DATA_SOURCE_RAW_UI_VIEW_NAME,DATA_SOURCE_FEATURE,DATA_SOURCE_TEST_DATA_PROC_SCHEMA,DATA_SOURCE_TEST_DATA_PROC_NAME")] DATA_SOURCE dATA_SOURCE, string[] SelectedItems, string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {
            dATA_SOURCE.CONFIG_COMMON_NAME = Session["sClient"].ToString();
            if (SelectedItems != null)
            {                
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "DATASOURCE_ATTRIBUTES", db);
                dATA_SOURCE.DATA_SOURCE_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot create due to selection of invalid features.");
                    return RedirectToAction("Create");
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.DATA_SOURCE.Add(dATA_SOURCE);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME, DATA_SOURCE_NAME });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dATA_SOURCE);
        }

        // GET: DATA_SOURCE/Edit/5
        [ImportModelStateFromTempData]
        [SessionExpireFilterAttribute]
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

            SelectList listTypes = new SelectList(db.vDATA_SOURCE_TYPE_WITH_PARENT, "DATA_SOURCE_TYPE_GUID", "DATA_SOURCE_TYPE_NAME_WITH_PARENT");

            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");
            ViewBag.DATA_SOURCE_TABLE_SCHEMA = new SelectList(db.vSCHEMAS, "name", "name");
            ViewBag.DATA_SOURCE_TYPE_GUID = listTypes;
            return View(dATA_SOURCE);
        }

        // POST: DATA_SOURCE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        [SessionExpireFilterAttribute]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DATA_SOURCE_NAME,DATA_SOURCE_TYPE_GUID,DATA_SOURCE_TABLE_SCHEMA,DATA_SOURCE_TABLE_NAME,DATA_SOURCE_RAW_VIEW_SCHEMA,DATA_SOURCE_RAW_VIEW_NAME,DATA_SOURCE_TABLE_PROC_UPDATE_SCHEMA,DATA_SOURCE_TABLE_PROC_UPDATE_NAME,DATA_SOURCE_TABLE_PROC_INSERT_SCHEMA,DATA_SOURCE_TABLE_PROC_INSERT_NAME,DATA_SOURCE_TABLE_PROC_DELETE_SCHEMA,DATA_SOURCE_TABLE_PROC_DELETE_NAME,DATA_SOURCE_TABLE_PROC_DDL_PARENT_SCHEMA,DATA_SOURCE_TABLE_PROC_DDL_PARENT_NAME,DATA_SOURCE_RAW_UI_VIEW_SCHEMA,DATA_SOURCE_RAW_UI_VIEW_NAME,DATA_SOURCE_FEATURE,DATA_SOURCE_TEST_DATA_PROC_SCHEMA,DATA_SOURCE_TEST_DATA_PROC_NAME")] DATA_SOURCE dATA_SOURCE, string[] SelectedItems, string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {
            if (SelectedItems != null)
            {                
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "DATASOURCE_ATTRIBUTES", db);
                dATA_SOURCE.DATA_SOURCE_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot Edit due to invalid Features.");
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                //TODO: come back here
                string oldCONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();
                string oldDATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString();

                if (oldDATA_SOURCE_NAME != dATA_SOURCE.DATA_SOURCE_NAME || oldCONFIG_COMMON_NAME != dATA_SOURCE.CONFIG_COMMON_NAME)
                {
                    var dATA_SOURCEs = db.DATA_SOURCE.Where(a => a.DATA_SOURCE_NAME == oldDATA_SOURCE_NAME && a.CONFIG_COMMON_NAME == oldCONFIG_COMMON_NAME);

                    foreach (var f in dATA_SOURCEs)
                    {
                        db.DATA_SOURCE.Remove(f);
                    }

                    db.DATA_SOURCE.Add(dATA_SOURCE);
                }
                else
                {
                    db.Entry(dATA_SOURCE).State = EntityState.Modified;
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
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DATA_SOURCE_NAME = Request["DATA_SOURCE_NAME"].ToString() });
                }

                return RedirectToAction("Index");
            }

            return View(dATA_SOURCE);
        }

        // GET: DATA_SOURCE/Delete/5
        [ImportModelStateFromTempData]
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string DATA_SOURCE_NAME)
        {
            DATA_SOURCE dATA_SOURCE = db.DATA_SOURCE.Find(CONFIG_COMMON_NAME, DATA_SOURCE_NAME);
            db.DATA_SOURCE.Remove(dATA_SOURCE);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                return RedirectToAction("Delete", new { CONFIG_COMMON_NAME, DATA_SOURCE_NAME });
            }

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