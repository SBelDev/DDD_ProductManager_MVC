using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Web.Attributes;

namespace Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Name of Product")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Quantity of Product")]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please enter Price of Product")]
        [RegularExpression(@"^\$?([1-9]{1}[0-9]{0,2}(\,[0-9]{3})*(\.[0-9]{0,2})?|[1-9]{1}[0-9]{0,}(\.[0-9]{0,2})?|0(\.[0-9]{0,2})?|(\.[0-9]{1,2})?)$", ErrorMessage = "{0} must be a Number.")]
        [Range(typeof(Decimal), "0.01", "9999999999999999.99", ErrorMessage = "{0} must be between {1} and {2}.")]
        [DisplayName("Price (€)")]
        public decimal Price { get; set; }

        [ValidateAtLeastOneBoxChecked(ErrorMessage = "Please select at least one Tag")]
        public List<SelectedTag> Tags { get; set; }

        [ValidateImageFile(ErrorMessage = "Please select a Image smaller than 1MB")]
        public HttpPostedFileBase File { get; set; }

        public byte[] FileInBytes { get; set; } 

        //[Required(ErrorMessage = "Please Select whether to show Image in List of Products")]
        public bool? ShowInList { get; set; }

        [Required(ErrorMessage = "Please select Category of Product")]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; }
    }
}


