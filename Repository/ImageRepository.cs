namespace Repository
{
    using System.Data;
    using System.Linq;
    using DomainObject;

    public class ImageRepository
    {
        private readonly DataBase _db = new DataBase();

        public Image GetImageById(int id)
        {
            return _db.Image.SingleOrDefault(n => n.Id == id);
        }

        public bool ExistImage(int id)
        {
            return _db.Image.Any(n => n.Id == id);
        }

        public void EditImage(Image image)
        {
            _db.ObjectStateManager.ChangeObjectState(image, EntityState.Modified);
            _db.SaveChanges();
        }

        public int AddImage(Image image)
        {
            _db.Image.AddObject(image);
            _db.SaveChanges();

            return image.Id;
        }
    }
}