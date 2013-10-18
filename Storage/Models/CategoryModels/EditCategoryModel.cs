namespace Storage.Models.CategoryModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class EditCategoryModel : LayoutModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [HiddenInput]
        public int Id { get; set; }
    }
}