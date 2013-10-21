using System.Web.Mvc;

namespace Storage.Controllers
{
    using Models.Error;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Error404()
        {
            var model = new ErrorModel
                        {
                            PageTitle = "Ошибка 404",
                            Error = "Ошибка 404"
                        };
            return View(model);
        }
    }
}
