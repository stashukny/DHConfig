using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DHConfig
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    internal class SessionExpireFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;



            if ((ctx.Session["sClient"] == null) && (ctx.Request.UrlReferrer.AbsolutePath != "/") && (ctx.Request.UrlReferrer.AbsolutePath != "/Home/Index") && (ctx.Request.UrlReferrer.AbsolutePath != "/CONFIGs/Create"))
            {
                // check if a new session id was generated
                filterContext.Result = new RedirectResult("~/Home/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}