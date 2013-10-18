namespace Storage.Controllers
{
    using System.Web.Mvc;
    using System.Web.Security;
    using Models.AccountModels;

    public class AccountController : Controller
    {
        public ActionResult LogOn()
        {
            var viewModel = new LogOnModel
            {
                PageTitle = "Авторизация"
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            if (Membership.ValidateUser(viewModel.UserName, viewModel.Password))
            {
                FormsAuthentication.SetAuthCookie(viewModel.UserName, viewModel.RememberMe);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(viewModel);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Account");
        }
    }
}
