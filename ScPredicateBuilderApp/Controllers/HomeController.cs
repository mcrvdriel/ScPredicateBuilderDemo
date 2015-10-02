using System.Web.Mvc;
using ScPredicateBuilderApp.Models;
using Sitecore.Mvc.Controllers;

namespace ScPredicateBuilderApp.Controllers
{
    public class HomeController : SitecoreController
    {
        // GET: Home
	    public ActionResult Default()
	    {
		    var model = new HomeModel();
			model.GetProducts();
			model.GetServices();
			return View("Default", model);
	    }
    }
}