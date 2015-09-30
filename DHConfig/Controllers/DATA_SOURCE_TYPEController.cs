using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class DATA_SOURCE_TYPEController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DATA_SOURCE_TYPE
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            return View(db.DATA_SOURCE_TYPE.ToList());
        }

        // GET: DATA_SOURCE_TYPE/Details/5
        [SessionExpireFilterAttribute]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATA_SOURCE_TYPE dATA_SOURCE_TYPE = db.DATA_SOURCE_TYPE.Find(id);
            if (dATA_SOURCE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE_TYPE);
        }

        // GET: DATA_SOURCE_TYPE/Create
        [SessionExpireFilterAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DATA_SOURCE_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Create([Bind(Include = "DATA_SOURCE_TYPE_GUID,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY,DATA_SOURCE_TYPE_HID_STRING,DATA_SOURCE_TYPE_HID_LEVEL,DATA_SOURCE_TYPE_NAME")] DATA_SOURCE_TYPE dATA_SOURCE_TYPE)
        {
            if (ModelState.IsValid)
            {
                dATA_SOURCE_TYPE.DATA_SOURCE_TYPE_GUID = Guid.NewGuid();
                db.DATA_SOURCE_TYPE.Add(dATA_SOURCE_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dATA_SOURCE_TYPE);
        }

        // GET: DATA_SOURCE_TYPE/Edit/5
        [SessionExpireFilterAttribute]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATA_SOURCE_TYPE dATA_SOURCE_TYPE = db.DATA_SOURCE_TYPE.Find(id);
            if (dATA_SOURCE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE_TYPE);
        }

        // POST: DATA_SOURCE_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Edit([Bind(Include = "DATA_SOURCE_TYPE_GUID,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY,DATA_SOURCE_TYPE_HID_STRING,DATA_SOURCE_TYPE_HID_LEVEL,DATA_SOURCE_TYPE_NAME")] DATA_SOURCE_TYPE dATA_SOURCE_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dATA_SOURCE_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dATA_SOURCE_TYPE);
        }

        // GET: DATA_SOURCE_TYPE/Delete/5
        [SessionExpireFilterAttribute]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATA_SOURCE_TYPE dATA_SOURCE_TYPE = db.DATA_SOURCE_TYPE.Find(id);
            if (dATA_SOURCE_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dATA_SOURCE_TYPE);
        }

        // POST: DATA_SOURCE_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DATA_SOURCE_TYPE dATA_SOURCE_TYPE = db.DATA_SOURCE_TYPE.Find(id);
            db.DATA_SOURCE_TYPE.Remove(dATA_SOURCE_TYPE);
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