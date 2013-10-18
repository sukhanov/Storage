namespace Repository
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DomainObject;

    public class CategoryRepository
    {
        private readonly DataBase _db = new DataBase();

        public IEnumerable<Category> GetAllCategories()
        {
            return _db.Category;
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategotyById(id);
            if (category == null) return;
            _db.Category.DeleteObject(category);
            _db.SaveChanges();
        }

        public bool ExistTitleCategory(string title, int id)
        {
            return _db.Category.Where(n => n.Id != id).Select(n => n.Title).Contains(title);
        }

        public bool ExistCategory(int id)
        {
            return _db.Category.Any(n => n.Id == id);
        }

        public Category GetCategotyById(int id)
        {
            return _db.Category.SingleOrDefault(n => n.Id == id);
        }

        public void EditCategory(Category category)
        {
           // _db.Category.Attach(category);
            _db.ObjectStateManager.ChangeObjectState(category, EntityState.Modified);
            _db.SaveChanges();
        }

        public int AddCategory(Category category)
        {
            _db.Category.AddObject(category);
            _db.SaveChanges();

            return category.Id;
        }
    }
}
