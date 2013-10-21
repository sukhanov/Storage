namespace Repository
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using DomainObject;

    public class ProductRepository:Repository<Product>
    {
        private readonly DataBase _db = new DataBase();

        public IEnumerable<Product> GetAllProducts()
        {
            return _db.Product;
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product == null) return;
            _db.Product.DeleteObject(product);
            _db.SaveChanges();
        }

        public bool ExistTitleProduct(string title, int id)
        {
            return _db.Product.Where(n => n.Id != id).Select(n => n.Title).Contains(title);
        }

        public bool ExistProduct(int id)
        {
            return _db.Product.Any(n => n.Id == id);
        }

        public Product GetProductById(int id)
        {
            return _db.Product.SingleOrDefault(n => n.Id == id);
        }

        public void EditProduct(Product product)
        {
            _db.ObjectStateManager.ChangeObjectState(product, EntityState.Modified);
            _db.SaveChanges();
        }

        public int AddProduct(Product product)
        {
            _db.Product.AddObject(product);
            _db.SaveChanges();

            return product.Id;
        }
    }
}