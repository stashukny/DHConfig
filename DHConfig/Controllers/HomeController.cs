using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DHConfig.Controllers
{
    public class HomeController : Controller
    {

        private DataHammerConfigEntities db = new DataHammerConfigEntities();

        public ActionResult Index(string SelectedClient)
        {
            var Clients = db.CONFIGs.OrderBy(q => q.CONFIG_COMMON_NAME).Distinct().ToList();
            SelectList clients = new SelectList(Clients, "CONFIG_COMMON_NAME", "CONFIG_COMMON_NAME", SelectedClient);
            
            ViewBag.SelectedClient = clients;
            ViewBag.sClient = clients.SelectedValue;
            return View(db.CONFIGs.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}