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
    public class METRIC_TYPEController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: METRIC_TYPE
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            return View(db.METRIC_TYPE.ToList());
        }

        // GET: METRIC_TYPE/Details/5
        [SessionExpireFilterAttribute]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METRIC_TYPE mETRIC_TYPE = db.METRIC_TYPE.Find(id);
            if (mETRIC_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mETRIC_TYPE);
        }

        // GET: METRIC_TYPE/Create
        [SessionExpireFilterAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: METRIC_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Create([Bind(Include = "METRIC_TYPE_GUID,METRIC_TYPE_NAME,METRIC_DATA_TYPE,METRIC_CALCULATION,METRIC_ROLLUP,METRIC_PRORATE,IS_INTERNAL_AUDIT,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY")] METRIC_TYPE mETRIC_TYPE)
        {
            if (ModelState.IsValid)
            {
                mETRIC_TYPE.METRIC_TYPE_GUID = Guid.NewGuid();
                db.METRIC_TYPE.Add(mETRIC_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mETRIC_TYPE);
        }

        // GET: METRIC_TYPE/Edit/5
        [SessionExpireFilterAttribute]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METRIC_TYPE mETRIC_TYPE = db.METRIC_TYPE.Find(id);
            if (mETRIC_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mETRIC_TYPE);
        }

        // POST: METRIC_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Edit([Bind(Include = "METRIC_TYPE_GUID,METRIC_TYPE_NAME,METRIC_DATA_TYPE,METRIC_CALCULATION,METRIC_ROLLUP,METRIC_PRORATE,IS_INTERNAL_AUDIT,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY")] METRIC_TYPE mETRIC_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mETRIC_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mETRIC_TYPE);
        }

        // GET: METRIC_TYPE/Delete/5
        [SessionExpireFilterAttribute]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            METRIC_TYPE mETRIC_TYPE = db.METRIC_TYPE.Find(id);
            if (mETRIC_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(mETRIC_TYPE);
        }

        // POST: METRIC_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult DeleteConfirmed(Guid id)
        {
            METRIC_TYPE mETRIC_TYPE = db.METRIC_TYPE.Find(id);
            db.METRIC_TYPE.Remove(mETRIC_TYPE);
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
