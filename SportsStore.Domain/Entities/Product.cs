using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SportsStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue = false)]
        public int ProductID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        [Display(Name="Product")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString="{0:c}")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
    public enum Category
    {
        Chess,
        Soccer,
        Watersports,
        Basketball,
        Baseball
    }
}
