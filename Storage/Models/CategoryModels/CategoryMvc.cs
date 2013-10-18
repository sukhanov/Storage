namespace Storage.Models.CategoryModels
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class CategoryMvc : LayoutModel
    {
        public int? Id { get; set; }

        [NotNullValidator(MessageTemplate = "Обязательное поле")]
        public string Title { get; set; }


        public string Description { get; set; }


        public DateTime CreationDate { get; set; }


        public DateTime ModificationDate { get; set; }


        public int ProductsCount { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}