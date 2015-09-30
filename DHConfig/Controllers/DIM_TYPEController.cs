using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class DIM_TYPEController : Controller
    {
        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        // GET: DIM_TYPE
        [SessionExpireFilterAttribute]
        public ActionResult Index()
        {
            return View(db.DIM_TYPE.ToList());
        }

        // GET: DIM_TYPE/Details/5
        [SessionExpireFilterAttribute]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM_TYPE dIM_TYPE = db.DIM_TYPE.Find(id);
            if (dIM_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dIM_TYPE);
        }

        // GET: DIM_TYPE/Create
        [SessionExpireFilterAttribute]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DIM_TYPE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Create([Bind(Include = "DIM_TYPE_GUID,DIM_TYPE_NAME,DIM_TYPE_INCLUDE_DATA_SOURCE,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY")] DIM_TYPE dIM_TYPE)
        {
            if (ModelState.IsValid)
            {
                dIM_TYPE.DIM_TYPE_GUID = Guid.NewGuid();
                db.DIM_TYPE.Add(dIM_TYPE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dIM_TYPE);
        }

        // GET: DIM_TYPE/Edit/5
        [SessionExpireFilterAttribute]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM_TYPE dIM_TYPE = db.DIM_TYPE.Find(id);
            if (dIM_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dIM_TYPE);
        }

        // POST: DIM_TYPE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult Edit([Bind(Include = "DIM_TYPE_GUID,DIM_TYPE_NAME,DIM_TYPE_INCLUDE_DATA_SOURCE,CREATE_DATE_UTC,MODIFIED_DATE_UTC,MODIFIED_BY")] DIM_TYPE dIM_TYPE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dIM_TYPE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dIM_TYPE);
        }

        // GET: DIM_TYPE/Delete/5
        [SessionExpireFilterAttribute]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DIM_TYPE dIM_TYPE = db.DIM_TYPE.Find(id);
            if (dIM_TYPE == null)
            {
                return HttpNotFound();
            }
            return View(dIM_TYPE);
        }

        // POST: DIM_TYPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [SessionExpireFilterAttribute]
        public ActionResult DeleteConfirmed(Guid id)
        {
            DIM_TYPE dIM_TYPE = db.DIM_TYPE.Find(id);
            db.DIM_TYPE.Remove(dIM_TYPE);
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