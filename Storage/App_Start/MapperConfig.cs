namespace Storage
{
    using AutoMapper;
    using DomainObject;
    using Models;
    using Models.CategoryModels;
    using Models.ProductModels;

    public static class MapperConfig
    {
        public static void RegisterMappers()
        {
            Mapper.CreateMap<EditCategoryModel, Category>();
            Mapper.CreateMap<Category, EditCategoryModel>();
            Mapper.CreateMap<EditProductModel, Product>().ForMember(n => n.Image, m => m.Ignore());
            Mapper.CreateMap<Product, EditProductModel>().ForMember(n => n.Image, m => m.Ignore());
        }
    }
}
