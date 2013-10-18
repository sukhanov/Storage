namespace Storage.Controllers
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using Models.HomeModels;
    using Repository;

    public class HomeController : Controller
    {
        private readonly ImageRepository _imageRepository;

        public HomeController(ImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public ActionResult Index()
        {
            var model = new HomeModel()
            {
                PageTitle = "Склад",
                //CategoryCount = _repository.GetCategoryCount().ToString(CultureInfo.InvariantCulture),
                //ProductCount = _repository.GetProductCount().ToString(CultureInfo.InvariantCulture),
            };

            return View("Index", model);
        }

        public ActionResult GetImage(int id)
        {
            var imageData = _imageRepository.GetImageById(id).Data.ToArray();
            return new FileStreamResult(new MemoryStream(imageData), "image/jpeg");
        }
    }
}
