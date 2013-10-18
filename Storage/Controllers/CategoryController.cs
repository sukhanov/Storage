namespace Storage.Controllers
{
    using System;
    using System.Web.Mvc;
    using AutoMapper;
    using DomainObject;
    using Models.CategoryModels;
    using Repository;

    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var model = new CategoryModel
            {
                PageTitle = "Категории",
                Categories = _categoryRepository.GetAllCategories(),
                EditingMode = Request.IsAuthenticated
            };

            return View("Index", model);
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            var model = new EditCategoryModel
            {
                PageTitle = "Добавление новой категории",
            };
            return View("EditCategory", model);
        }

        [Authorize]
        public ActionResult EditCategory(int id)
        {
            var category = _categoryRepository.GetCategotyById(id);

            var model = Mapper.Map<Category, EditCategoryModel>(category);
            model.PageTitle = "Редактирование категории";

            return View("EditCategory", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditCategory(EditCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                if (_categoryRepository.ExistTitleCategory(model.Title, model.Id))
                {
                    ModelState.AddModelError("Title", "Данная категория уже есть в БД");
                    return View("EditCategory", model);
                }

                var category = _categoryRepository.GetCategotyById(model.Id) ?? new Category();
                category = Mapper.Map(model, category);

                if (_categoryRepository.ExistCategory(model.Id))
                {
                    category.ModificationDate = DateTime.Now;
                    _categoryRepository.EditCategory(category);
                }
                else
                {
                    category.CreationDate = DateTime.Now;
                    category.ModificationDate = DateTime.Now;
                    _categoryRepository.AddCategory(category);
                }

                return RedirectToAction("Index");
            }

            return View("EditCategory", model);
        }

        [Authorize]
        public ActionResult DeleteCategory(int id)
        {
            _categoryRepository.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
