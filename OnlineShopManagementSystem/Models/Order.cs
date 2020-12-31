using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopManagementSystem.Models
{
    public class Order
    {
        public int Id { get; set; }


        [Display(Name="Order No.")]
        public string OrderNo { get; set; }


        [Required(ErrorMessage ="Please provide your name")]
        public string Name{ get; set; }


        [Required(ErrorMessage ="Please provide your phone number")]
        [Display(Name="Phone No.")]
        public string Phone { get; set; }

        [EmailAddress]
        [Required(ErrorMessage ="Please provide your email address")]
        public string Email { get; set; }

        
        [Required(ErrorMessage ="Please provide your address")]
        public string Address { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }
    }
}
