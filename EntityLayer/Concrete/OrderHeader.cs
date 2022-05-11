using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class OrderHeader
    {
        public int OrderHeaderID { get; set; }
        public string ApplicationUserID { get; set; }
        [ValidateNever]
        [ForeignKey("ApplicationUserID")]
        public ApplicationUser applicationUser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }
        public DateTime Payment { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string? SessionID { get; set; }
        public string? PaymentIntendID { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAdress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
