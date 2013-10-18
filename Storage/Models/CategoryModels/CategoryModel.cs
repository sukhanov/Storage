namespace Storage.Models.CategoryModels
{
    using System.Collections.Generic;
    using DomainObject;

    public class CategoryModel : LayoutModel
    {
        public bool EditingMode { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}