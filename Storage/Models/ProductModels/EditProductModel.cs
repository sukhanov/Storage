namespace Storage.Models.ProductModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    public class EditProductModel : LayoutModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Цена")]
        [Range(typeof(double), "0", "100000", ErrorMessage = ">0")]
        public double Price { get; set; }

        [Display(Name = "Количество")]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Inclusive, MessageTemplate = "Значение должно быть положительное")]
        public int Count { get; set; }

        [HiddenInput]
        public int Id { get; set; }

        [HiddenInput]
        public int ImageId { get; set; }

        public HttpPostedFileBase Image { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}