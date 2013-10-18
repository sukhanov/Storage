namespace Storage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using DomainObject;
    using Models.ProductModels;
    using Repository;
    using Shared.Pager;

    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ImageRepository _imageRepository;

        public ProductController(ProductRepository productRepository, CategoryRepository categoryRepository, ImageRepository imageRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _imageRepository = imageRepository;
        }

        public ActionResult Index(int page = 1, string sortColumn = "Title", SortOrder sortOrder = SortOrder.Ascending, int categoryFilter = 0)
        {
            var model = new ProductModel();
            model.Page = page;
            var filter = new Shared.Pager.Filter(model.Take, model.Skip);
            filter.Items = new List<ISortingItem> { new SortItem { Column = sortColumn, Order = sortOrder } };

            model.TotalRows = _productRepository.GetTotalCount(n => n.CategoryId == categoryFilter || categoryFilter <= 0);

            model.PageTitle = "Товары";
            model.Products = _productRepository.Query(n => n.CategoryId == categoryFilter || categoryFilter <= 0, filter);
            model.Categories = new List<SelectListItem> { new SelectListItem { Text = "Все категории", Value = "-1" } };
            model.Categories.AddRange(_categoryRepository.GetAllCategories()
                .Select(n => new SelectListItem { Text = n.Title, Value = n.Id.ToString(CultureInfo.InvariantCulture), Selected = Equals(n.Id, categoryFilter) }));
            model.EditingMode = Request.IsAuthenticated;
            model.SortColumn = sortColumn;
            model.SortOrder = sortOrder;

            return View("Index", model);
        }

        [Authorize]
        public ActionResult AddProduct()
        {
            var model = new EditProductModel
            {
                PageTitle = "Добавление нового товара",
                Categories = _categoryRepository.GetAllCategories()
                    .Select(n => new SelectListItem { Text = n.Title, Value = n.Id.ToString(CultureInfo.InvariantCulture) }),
            };
            return View("EditProduct", model);
        }

        [Authorize]
        public ActionResult EditProduct(int id)
        {
            var product = _productRepository.GetProductById(id);

            var model = Mapper.Map<Product, EditProductModel>(product);
            model.PageTitle = "Редактирование товара";
            model.Categories = _categoryRepository.GetAllCategories()
                .Select(n => new SelectListItem { Text = n.Title, Value = n.Id.ToString(CultureInfo.InvariantCulture) });

            return View("EditProduct", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProduct(EditProductModel model)
        {
            model.Categories = _categoryRepository.GetAllCategories()
                .Select(n => new SelectListItem { Text = n.Title, Value = n.Id.ToString(CultureInfo.InvariantCulture) });

            if (ModelState.IsValid)
            {
                if (_productRepository.ExistTitleProduct(model.Title, model.Id))
                {
                    ModelState.AddModelError("Title", "Данный продукт уже есть в БД");
                    return View("EditProduct", model);
                }

                var product = _productRepository.GetProductById(model.Id) ?? new Product();
                product = Mapper.Map(model, product);
                product.ImageId = product.ImageId == 0 ? null : product.ImageId;

                if (model.Image != null)
                {
                    using (var stream = new MemoryStream())
                    {
                        model.Image.InputStream.CopyTo(stream);
                        var image = _imageRepository.GetImageById(model.ImageId) ?? new Image();
                        image.Data = stream.ToArray();

                        if (_imageRepository.ExistImage(model.ImageId))
                        {
                            _imageRepository.EditImage(image);
                        }
                        else
                        {
                            product.ImageId = _imageRepository.AddImage(image);
                        }
                    }
                }

                if (_productRepository.ExistProduct(model.Id))
                {
                    product.ModificationDate = DateTime.Now;
                    _productRepository.EditProduct(product);
                }
                else
                {
                    product.CreationDate = DateTime.Now;
                    product.ModificationDate = DateTime.Now;
                    _productRepository.AddProduct(product);
                }

                return RedirectToAction("Index");
            }

            return View("EditProduct", model);
        }

        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            _productRepository.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
