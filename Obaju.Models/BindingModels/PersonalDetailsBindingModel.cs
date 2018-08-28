using Obaju.Constants;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models.BindingModels
{
    public class PersonalDetailsBindingModel
    {
        [Display(Name = FormLabel.FirstName)]
        [Required(ErrorMessage = ErrorMessage.FirstNameRequired)]
        public string FirstName { get; set; }

        [Display(Name = FormLabel.LastName)]
        [Required(ErrorMessage = ErrorMessage.LastNameRequired)]
        public string LastName { get; set; }

        [Display(Name = FormLabel.BillingAddress)]
        [Required(ErrorMessage = ErrorMessage.BillingAddressRequired)]
        public string BillingAddress { get; set; }

        [Display(Name = FormLabel.BillingAddressLine2)]
        public string BillingAddressLine2 { get; set; }

        [Required(ErrorMessage = ErrorMessage.CountryRequired)]
        public string Country { get; set; }

        [Required(ErrorMessage = ErrorMessage.CityRequired)]
        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        [Display(Name = FormLabel.PostalCode)]
        public string PostalCode { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = FormLabel.PhoneNumber)]
        [Required(ErrorMessage = ErrorMessage.PhoneNumberRequired)]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = ErrorMessage.EmailRequired)]
        public string Email { get; set; }
    }
}
