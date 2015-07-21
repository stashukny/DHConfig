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
        public ActionResult Index(string SelectedClient)
        {
            
            var Clients = db.CONFIGs.OrderBy(q => q.CONFIG_COMMON_NAME).Distinct().ToList();

            IQueryable<DIM> dims = db.DIMs
            .Where(c => SelectedClient == null || SelectedClient == "" || c.CONFIG_COMMON_NAME == SelectedClient)
            .Include(t => t.DIM_TYPE);

            SelectList clients = new SelectList(Clients, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", SelectedClient);

            ViewBag.SelectedClient = clients;
            ViewBag.sClient = clients.SelectedValue;

            var sql = dims.ToString();

            return View(dims.ToList());

        }

        // GET: DIMs/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string sClient)
        {
            DIM dIM = db.DIMs.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME);
            if (dIM == null)
            {
                return HttpNotFound();
            }
            return View(dIM);
        }

        // GET: DIMs/Create
        [ImportModelStateFromTempData]
        public ActionResult Create(string sClient)
        {

            var features = db.BITWISE_DICTIONARY
                        .Where(f => f.BITWISE_GROUP == "DIM")
                        .ToList()
                        .Select(c => new
                        {
                            DIM_FEATURE = c.BITWISE_KEY,
                            DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                        });

            SelectList listSchemas = new SelectList(db.vSCHEMAS, "name", "name");
            SelectList listTypes = new SelectList(db.DIM_TYPE, "DIM_TYPE_GUID", "DIM_TYPE_NAME");
            SelectList listClients = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME");

            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", sClient ?? listClients.ElementAt(0).Value);
            ViewBag.DIM_TYPE_GUID = new SelectList(db.DIM_TYPE, "DIM_TYPE_GUID", "DIM_TYPE_NAME");
            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR");

            ViewBag.listTypes = listTypes;
            ViewBag.DIM_TABLE_SCHEMA = listSchemas;
            ViewBag.DIM_LOAD_PROC_SCHEMA_RAW = listSchemas;
            ViewBag.DIM_TABLE_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_PROC_UI_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_LOAD_PROC_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_WHITELIST_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_RAW_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA = listSchemas;

            return View();
        }

        // POST: DIMs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ExportModelStateToTempData]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DIM_TYPE_GUID,DIM_COMMON_NAME,DIM_TABLE_SCHEMA,DIM_TABLE_NAME,DIM_LOAD_PROC_SCHEMA_RAW,DIM_LOAD_PROC_NAME_RAW,DIM_TABLE_CLEAN_SCHEMA,DIM_TABLE_CLEAN_NAME,DIM_PROC_UI_CLEAN_SCHEMA,DIM_PROC_UI_CLEAN_NAME,DIM_LOAD_PROC_CLEAN_SCHEMA,DIM_LOAD_PROC_CLEAN_NAME,DIM_VIEW_WHITELIST_SCHEMA,DIM_VIEW_WHITELIST_NAME,DIM_VIEW_RAW_SCHEMA,DIM_VIEW_RAW_NAME,DIM_VIEW_CLEAN_SCHEMA,DIM_VIEW_CLEAN_NAME,DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA,DIM_PROC_RAW_TABLE_CLEAN_ID_NAME,DIM_FEATURE,DIM_TAXONOMY_PROC_SCHEMA,DIM_TAXONOMY_PROC_NAME,DIM_LOAD_PRE_PROC_SPROC_SCHEMA,DIM_LOAD_PRE_PROC_SPROC_NAME,DIM_LOAD_POST_PROC_SCHEMA,DIM_LOAD_POST_PROC_NAME,DIM_LOAD_PRE_PROC_CLEAN_SCHEMA,DIM_LOAD_PRE_PROC_CLEAN_NAME,DIM_LOAD_POST_PROC_CLEAN_SCHEMA,DIM_LOAD_POST_PROC_CLEAN_NAME,IS_STATIC,IS_AUTO_GENERATED")] string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, DIM dIM, string[] SelectedItems, string sClient)
        {

            if (SelectedItems != null)
            {

                if (SelectedItems.Count() > 1)
                {
                    dIM.DIM_FEATURE = String.Join(",", SelectedItems);
                }
                else
                {
                    dIM.DIM_FEATURE = SelectedItems[0];
                }



                int Total = 0;
                var settings = db.BITWISE_DICTIONARY
                .Where(f => f.BITWISE_GROUP == "DIM" && SelectedItems.Contains(f.BITWISE_KEY))
                .Sum(x => x.BITWISE_VALUE);
                Total = (int)settings;

                //validate sum           
                bool exists = db.BITWISE_DICTIONARY_VALID_VALUES.Any(a => a.BITWISE_VALUE == Total && a.BITWISE_GROUP == "DIM");
                if (!exists)
                {
                    var features = db.BITWISE_DICTIONARY
                        .Where(f => f.BITWISE_GROUP == "DIM")
                        .ToList()
                        .Select(c => new
                        {
                            DIM_FEATURE = c.BITWISE_KEY,
                            DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                        });

                    
                    ModelState.AddModelError(String.Empty, "Cannot create due to selection of invalid features.");
                    return RedirectToAction("Edit", new { sClient, CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DIM_COMMON_NAME = Request["DIM_COMMON_NAME"].ToString() });

                }
            }

            if (ModelState.IsValid)
            {

                try
                {
                    db.DIMs.Add(dIM);
                    db.SaveChanges();
                
                }
                
                
                catch (Exception ex)
                {
                    if (ex.HResult.ToString() == "-2146233087")

                        ModelState.AddModelError(String.Empty, "Cannot Create due to Key violation.");
                    return RedirectToAction("Create", new { sClient, CONFIG_COMMON_NAME, DIM_COMMON_NAME});
                }
                db.SaveChanges();
                return RedirectToAction("Index", new { SelectedClient = sClient });
            }


            return View(dIM);
        }

        // GET: DIMs/Edit/5
        [ImportModelStateFromTempData]
        public ActionResult Edit(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string sClient)
        {

            DIM dIM = db.DIMs.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME);
            if (dIM == null)
            {
                return HttpNotFound();
            }

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DIM")
            .ToList()
            .Select(c => new
            {
                DIM_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });

            
            SelectList listSchemas = new SelectList(db.vSCHEMAS, "name", "name");
            SelectList listTypes = new SelectList(db.DIM_TYPE, "DIM_TYPE_GUID", "DIM_TYPE_NAME");
            MultiSelectList listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR", dIM.SelectedItems);
            
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", dIM.CONFIG_COMMON_NAME);            
            ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs, "DIM_COMMON_NAME", "DIM_COMMON_NAME", dIM.DIM_COMMON_NAME);
            ViewBag.DIM_FEATURE = listFeatures;
            ViewBag.DIM_TYPE_GUID = listTypes;           
            ViewBag.DIM_TABLE_SCHEMA = listSchemas;
            ViewBag.DIM_LOAD_PROC_SCHEMA_RAW = listSchemas;
            ViewBag.DIM_TABLE_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_PROC_UI_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_LOAD_PROC_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_WHITELIST_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_RAW_SCHEMA = listSchemas;
            ViewBag.DIM_VIEW_CLEAN_SCHEMA = listSchemas;
            ViewBag.DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA = listSchemas;

            return View(dIM);
        }

        // POST: DIMs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ExportModelStateToTempData]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DIM_TYPE_GUID,DIM_COMMON_NAME,DIM_TABLE_SCHEMA,DIM_TABLE_NAME,DIM_LOAD_PROC_SCHEMA_RAW,DIM_LOAD_PROC_NAME_RAW,DIM_TABLE_CLEAN_SCHEMA,DIM_TABLE_CLEAN_NAME,DIM_PROC_UI_CLEAN_SCHEMA,DIM_PROC_UI_CLEAN_NAME,DIM_LOAD_PROC_CLEAN_SCHEMA,DIM_LOAD_PROC_CLEAN_NAME,DIM_VIEW_WHITELIST_SCHEMA,DIM_VIEW_WHITELIST_NAME,DIM_VIEW_RAW_SCHEMA,DIM_VIEW_RAW_NAME,DIM_VIEW_CLEAN_SCHEMA,DIM_VIEW_CLEAN_NAME,DIM_PROC_RAW_TABLE_CLEAN_ID_SCHEMA,DIM_PROC_RAW_TABLE_CLEAN_ID_NAME,DIM_FEATURE,DIM_TAXONOMY_PROC_SCHEMA,DIM_TAXONOMY_PROC_NAME,DIM_LOAD_PRE_PROC_SPROC_SCHEMA,DIM_LOAD_PRE_PROC_SPROC_NAME,DIM_LOAD_POST_PROC_SCHEMA,DIM_LOAD_POST_PROC_NAME,DIM_LOAD_PRE_PROC_CLEAN_SCHEMA,DIM_LOAD_PRE_PROC_CLEAN_NAME,DIM_LOAD_POST_PROC_CLEAN_SCHEMA,DIM_LOAD_POST_PROC_CLEAN_NAME,IS_STATIC,IS_AUTO_GENERATED")] DIM dIM, string[] SelectedItems, string sClient, string CONFIG_COMMON_NAME, string DIM_COMMON_NAME)
        {
            if (SelectedItems != null)
            {

                if (SelectedItems.Count() > 1)
                {
                    dIM.DIM_FEATURE = String.Join(",", SelectedItems);
                }
                else
                {
                    dIM.DIM_FEATURE = SelectedItems[0];
                }
            

                int Total = 0;
            
                var settings = db.BITWISE_DICTIONARY
                .Where(f => f.BITWISE_GROUP == "DIM" && SelectedItems.Contains(f.BITWISE_KEY))
                .Sum(x => x.BITWISE_VALUE);

                Total = (int)settings;

                //validate sum           
                bool exists = db.BITWISE_DICTIONARY_VALID_VALUES.Any(a => a.BITWISE_VALUE == Total && a.BITWISE_GROUP == "DIM");
                if (!exists)
                {

                    var features = db.BITWISE_DICTIONARY
                        .Where(f => f.BITWISE_GROUP == "DIM")
                        .ToList()
                        .Select(c => new
                        {
                            DIM_FEATURE = c.BITWISE_KEY,
                            DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                        });

                    ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs, "DIM_COMMON_NAME", "DIM_COMMON_NAME", dIM.DIM_COMMON_NAME);
                    ViewBag.listFeatures = new MultiSelectList(features, "DIM_FEATURE", "DESCR", dIM.SelectedItems);
                    ViewBag.DIM_TYPE_GUID = new SelectList(db.DIM_TYPE, "DIM_TYPE_GUID", "DIM_TYPE_NAME");           
                    //throw error                


                    ModelState.AddModelError(String.Empty, "Cannot Edit due to invalid Features.");
                    return RedirectToAction("Edit", new { sClient, CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DIM_COMMON_NAME = Request["DIM_COMMON_NAME"].ToString() });                    

                }
            }

            if (ModelState.IsValid)
            {
                //TODO: come back here
                string oldDIM_COMMON_NAME = Request["DIM_COMMON_NAME"].ToString();
                string oldCONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();

                if (oldDIM_COMMON_NAME != dIM.DIM_COMMON_NAME || oldCONFIG_COMMON_NAME != dIM.CONFIG_COMMON_NAME)
                {

                    var dims = db.DIMs.Where(a => a.DIM_COMMON_NAME == oldDIM_COMMON_NAME && a.CONFIG_COMMON_NAME == oldCONFIG_COMMON_NAME);

                    foreach (var f in dims)
                    {
                        db.DIMs.Remove(f);
                    }

                    db.DIMs.Add(dIM);
                }
                else
                {
                    db.Entry(dIM).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    if (ex.HResult.ToString() == "-2146233087")
                        
                        ModelState.AddModelError(String.Empty, "Cannot Edit due to Key violation.");
                        return RedirectToAction("Edit", new { sClient, CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString(), DIM_COMMON_NAME = Request["DIM_COMMON_NAME"].ToString() });
                }

                return RedirectToAction("Index", new { SelectedClient = sClient});
            }

            return View(dIM);
        }

        // GET: DIMs/Delete/5
        public ActionResult Delete(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string sClient)
        {

            DIM dIM = db.DIMs.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME);
            if (dIM == null)
            {
                return HttpNotFound();
            }
            return View(dIM);
        }

        // POST: DIMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string sClient)
        {
            DIM dIM = db.DIMs.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME);
            db.DIMs.Remove(dIM);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.HResult.ToString() == "-2146233087")
                    ViewBag.Error = "Cannot perform delete due to foreign key constraint";
                return View();
            }
            
            return RedirectToAction("Index", new { SelectedClient = sClient });
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
