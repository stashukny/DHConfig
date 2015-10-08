using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class FACT_FIELDController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: FACT_FIELD
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
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
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
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
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,FACT_COMMON_NAME,FACT_FIELD_NAME,OBJECT_TYPE_NAME,DIM_FIELD_NAME,FACT_FIELD_FEATURE")] FACT_FIELD fACT_FIELD, string[] SelectedItems, string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {
            fACT_FIELD.CONFIG_COMMON_NAME = Session["sClient"].ToString();
            if (SelectedItems != null)
            {
                string feature = fACT_FIELD.FACT_FIELD_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "FACT_FIELDS", db);
                fACT_FIELD.FACT_FIELD_FEATURE = feature;
                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot create due to selection of invalid features.");
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString(), FACT_FIELD_NAME = Request["FACT_FIELD_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.FACT_FIELD.Add(fACT_FIELD);
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
                    return RedirectToAction("Create", new { CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fACT_FIELD);
        }

        // GET: FACT_FIELD/Edit/5
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
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
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,FACT_COMMON_NAME,FACT_FIELD_NAME,OBJECT_TYPE_NAME,DIM_FIELD_NAME,FACT_FIELD_FEATURE")] FACT_FIELD fACT_FIELD, string[] SelectedItems, string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {
            if (SelectedItems != null)
            {
                string feature = fACT_FIELD.FACT_FIELD_FEATURE;
                bool exists = BitwiseDictionaryChecker.IsExists(ref feature, SelectedItems, "FACT_FIELDS", db);
                fACT_FIELD.FACT_FIELD_FEATURE = feature;

                if (!exists)
                {
                    ModelState.AddModelError(String.Empty, "Cannot select due to invalid Features.");
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString(), FACT_FIELD_NAME = Request["FACT_FIELD_NAME"].ToString() });
                }
            }

            if (ModelState.IsValid)
            {
                //TODO: come back here
                string oldFACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString();
                string oldCONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();
                string oldFACT_FIELD_NAME = Request["FACT_FIELD_NAME"].ToString();

                if (oldFACT_COMMON_NAME != fACT_FIELD.FACT_COMMON_NAME || oldCONFIG_COMMON_NAME != fACT_FIELD.CONFIG_COMMON_NAME || oldFACT_FIELD_NAME != fACT_FIELD.FACT_FIELD_NAME)
                {
                    var fact_fields = db.FACT_FIELD.Where(a => a.FACT_COMMON_NAME == oldFACT_COMMON_NAME && a.CONFIG_COMMON_NAME == oldCONFIG_COMMON_NAME && a.FACT_FIELD_NAME == oldFACT_FIELD_NAME);

                    foreach (var f in fact_fields)
                    {
                        db.FACT_FIELD.Remove(f);
                    }

                    db.FACT_FIELD.Add(fACT_FIELD);
                }
                else
                {
                    db.Entry(fACT_FIELD).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                    return RedirectToAction("Edit", new { CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), FACT_COMMON_NAME = Request["FACT_COMMON_NAME"].ToString(), FACT_FIELD_NAME = Request["FACT_FIELD_NAME"].ToString() });
                }

                return RedirectToAction("Index");
            }

            return View(fACT_FIELD);
        }

        // GET: FACT_FIELD/Delete/5
        [SessionExpireFilterAttribute]
        [ImportModelStateFromTempData]
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
        [SessionExpireFilterAttribute]
        [ExportModelStateToTempData]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string FACT_COMMON_NAME, string FACT_FIELD_NAME)
        {
            FACT_FIELD fACT_FIELD = db.FACT_FIELD.Find(CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME);
            db.FACT_FIELD.Remove(fACT_FIELD);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.InnerException.InnerException.Message);
                return RedirectToAction("Delete", new { CONFIG_COMMON_NAME, FACT_COMMON_NAME, FACT_FIELD_NAME });
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