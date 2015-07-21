using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DHConfig;

namespace DHConfig.Controllers
{
    public class DIM_FIELDController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DIM_FIELD
        public ActionResult Index(string SelectedClient)
        {

            var Clients = db.CONFIGs.OrderBy(q => q.CONFIG_COMMON_NAME).Distinct().ToList();
           
            IQueryable<DIM_FIELD> fields = db.DIM_FIELD
            .Where(c => SelectedClient == null || SelectedClient == "" || c.CONFIG_COMMON_NAME == SelectedClient);

            SelectList clients = new SelectList(Clients, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", SelectedClient);            
            ViewBag.SelectedClient = clients;
            ViewBag.sClient = clients.SelectedValue;
            
            var sql = fields.ToString();

            return View(fields.ToList());
        }

        // GET: DIM_FIELD/Details/5
        public ActionResult Details(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string DIM_FIELD_NAME, string sClient)
        {

            DIM_FIELD dIM_FIELD = db.DIM_FIELD.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME, DIM_FIELD_NAME);
            if (dIM_FIELD == null)
            {
                return HttpNotFound();
            }
            ViewBag.sClient = sClient;
            return View(dIM_FIELD);
        }

        // GET: DIM_FIELD/Create
        public ActionResult Create(string sClient)
        {

            var features = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DIM_FIELDS")
            .ToList()
            .Select(c => new
            {
                DIM_FIELD_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
            });


            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", sClient);
            ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs.OrderBy(x => x.DIM_COMMON_NAME), "DIM_COMMON_NAME", "DIM_COMMON_NAME");                    
            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FIELD_FEATURE", "DESCR");

            return View();
        }

        // POST: DIM_FIELD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CONFIG_COMMON_NAME,DIM_COMMON_NAME,DIM_FIELD_NAME,DIM_FIELD_NAME_CLEAN,DIM_DATA_TYPE,DIM_FIELD_FEATURE,DERIVED_CONFIGURATION")] DIM_FIELD dIM_FIELD, string[] SelectedItems, string sClient)
        {

            if (SelectedItems != null)
            {

                if (SelectedItems.Count() > 1)
                {
                    dIM_FIELD.DIM_FIELD_FEATURE = String.Join(",", SelectedItems);
                }
                else
                {
                    dIM_FIELD.DIM_FIELD_FEATURE = SelectedItems[0];
                }
            }

            int Total = 0;            
            var settings = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DIM_FIELDS" && SelectedItems.Contains(f.BITWISE_KEY))
            .Sum(x => x.BITWISE_VALUE);
            Total = (int)settings;



            //validate sum           
            bool exists = db.BITWISE_DICTIONARY_VALID_VALUES.Any(a => a.BITWISE_VALUE == Total && a.BITWISE_GROUP == "DIM_FIELDS");
            if (!exists)
            {              
                var features = db.BITWISE_DICTIONARY
                    .Where(f => f.BITWISE_GROUP == "DIM_FIELDS")
                    .ToList()
                    .Select(c => new
                    {
                        DIM_FIELD_FEATURE = c.BITWISE_KEY,
                        DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                    });

                ViewBag.listFeatures = new MultiSelectList(features, "DIM_FIELD_FEATURE", "DESCR", dIM_FIELD.SelectedItems);

                TempData["FeaturesInvalid"] = true;


                ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs, "DIM_COMMON_NAME", "DIM_COMMON_NAME", dIM_FIELD.DIM_COMMON_NAME);
                ViewBag.CONFIG_COMMON_NAME = new SelectList(db.DIMs, "CONFIG_COMMON_NAME", "DIM_TABLE_SCHEMA", dIM_FIELD.CONFIG_COMMON_NAME);            
                return View(dIM_FIELD);

            }
            
            if (ModelState.IsValid)
            {
                db.DIM_FIELD.Add(dIM_FIELD);
                db.SaveChanges();
                return RedirectToAction("Index", new { SelectedClient = sClient });
            }
            
            return View(dIM_FIELD);
        }

        // GET: DIM_FIELD/Edit/5
        public ActionResult Edit(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string DIM_FIELD_NAME, string DIM_FIELD_FEATURE, string sClient)
        {

            DIM_FIELD dIM_FIELD = db.DIM_FIELD.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME, DIM_FIELD_NAME);
            if (dIM_FIELD == null)
            {
                return HttpNotFound();
            }

            var features = db.BITWISE_DICTIONARY
                .Where(f => f.BITWISE_GROUP == "DIM_FIELDS")
                .ToList()
                .Select(c => new
            {
                DIM_FIELD_FEATURE = c.BITWISE_KEY,
                DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR) 
            });


            
            ViewBag.CONFIG_COMMON_NAME = new SelectList(db.CONFIGs, "CONFIG_COMMON_NAME", "CONFIG_DATA_PROCESS_PROC_SCHEMA", dIM_FIELD.CONFIG_COMMON_NAME);
            ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs, "DIM_COMMON_NAME", "DIM_COMMON_NAME", dIM_FIELD.DIM_COMMON_NAME);
            ViewBag.listFeatures = new MultiSelectList(features, "DIM_FIELD_FEATURE", "DESCR", dIM_FIELD.SelectedItems);

            return View(dIM_FIELD);
        }

        // POST: DIM_FIELD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CONFIG_COMMON_NAME,DIM_COMMON_NAME,DIM_FIELD_NAME,DIM_FIELD_NAME_CLEAN,DIM_DATA_TYPE,DIM_FIELD_FEATURE,DERIVED_CONFIGURATION")] DIM_FIELD dIM_FIELD, string[] SelectedItems, string sClient)
        {            

            if (SelectedItems != null)
            {
                                
                if (SelectedItems.Count() > 1)
                {
                    dIM_FIELD.DIM_FIELD_FEATURE = String.Join(",", SelectedItems);
                }
                else
                {
                    dIM_FIELD.DIM_FIELD_FEATURE = SelectedItems[0];
                }
            }

            int Total = 0;
            
            //var settings = db.BITWISE_DICTIONARY.Where(a => SelectedItems.Contains(a.BITWISE_KEY) && a.BITWISE_GROUP == "DIM_FIELDS");
            var settings = db.BITWISE_DICTIONARY
            .Where(f => f.BITWISE_GROUP == "DIM_FIELDS" && SelectedItems.Contains(f.BITWISE_KEY))            
            .Sum(x => x.BITWISE_VALUE);

            Total = (int)settings;

            //validate sum           
            bool exists = db.BITWISE_DICTIONARY_VALID_VALUES.Any(a => a.BITWISE_VALUE == Total && a.BITWISE_GROUP == "DIM_FIELDS");
            if (!exists)
            {
                
                
                
                var features = db.BITWISE_DICTIONARY
                    .Where(f => f.BITWISE_GROUP == "DIM_FIELDS")
                    .ToList()
                    .Select(c => new
                    {
                        DIM_FIELD_FEATURE = c.BITWISE_KEY,
                        DESCR = string.Format("{0} -- {1}", c.BITWISE_KEY, c.DESCR)
                    });

                ViewBag.DIM_COMMON_NAME = new SelectList(db.DIMs, "DIM_COMMON_NAME", "DIM_COMMON_NAME", dIM_FIELD.DIM_COMMON_NAME);
                ViewBag.listFeatures = new MultiSelectList(features, "DIM_FIELD_FEATURE", "DESCR", dIM_FIELD.SelectedItems);
                //throw error                
                TempData["FeaturesInvalid"] = true;                

                return View(dIM_FIELD);

            }
            

            if (ModelState.IsValid)
            {
                
                string DIM_COMMON_NAME = Request["DIM_COMMON_NAME"].ToString();
                string CONFIG_COMMON_NAME = Request["CONFIG_COMMON_NAME"].ToString();
                string DIM_FIELD_NAME = Request["DIM_FIELD_NAME"].ToString();

                if (DIM_COMMON_NAME != dIM_FIELD.DIM_COMMON_NAME || CONFIG_COMMON_NAME != dIM_FIELD.CONFIG_COMMON_NAME || DIM_FIELD_NAME != dIM_FIELD.DIM_FIELD_NAME)
                { 

                    var fields = db.DIM_FIELD.Where(a => a.DIM_COMMON_NAME == DIM_COMMON_NAME && a.CONFIG_COMMON_NAME == CONFIG_COMMON_NAME && a.DIM_FIELD_NAME == DIM_FIELD_NAME);

                    foreach (var f in fields)
                    {
                        db.DIM_FIELD.Remove(f);
                    }

                    db.DIM_FIELD.Add(dIM_FIELD);
                }
                else
                {
                    db.Entry(dIM_FIELD).State = EntityState.Modified;                       
                }
                                         
                try
                {
                    db.SaveChanges();
                }
                catch
                {
                    //TODO: error handling
                    return RedirectToAction("Index", new { SelectedClient = sClient });
                }
                
                return RedirectToAction("Index", new { SelectedClient = sClient });
            }

            return View(dIM_FIELD);
        }

        // GET: DIM_FIELD/Delete/5
        public ActionResult Delete(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string DIM_FIELD_NAME, string sClient)
        {

            DIM_FIELD dIM_FIELD = db.DIM_FIELD.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME, DIM_FIELD_NAME);
            if (dIM_FIELD == null)
            {
                return HttpNotFound();
            }
            return View(dIM_FIELD);
        }

        // POST: DIM_FIELD/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string CONFIG_COMMON_NAME, string DIM_COMMON_NAME, string DIM_FIELD_NAME, string sClient)
        {
            DIM_FIELD dIM_FIELD = db.DIM_FIELD.Find(CONFIG_COMMON_NAME, DIM_COMMON_NAME, DIM_FIELD_NAME);
            db.DIM_FIELD.Remove(dIM_FIELD);
            db.SaveChanges();
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
