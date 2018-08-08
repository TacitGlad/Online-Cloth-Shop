using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Obaju.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        
        public string BillingAddress { get; set; }

        public string BillingAddressLine2 { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }        
    }
}
