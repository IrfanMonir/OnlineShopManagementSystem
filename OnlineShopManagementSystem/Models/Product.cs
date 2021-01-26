using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OnlineShopManagementSystem.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public Decimal Price { get; set; }
        public string Image { get; set; }
        [DisplayName("Color")]
        public string ProductColor { get; set; }
        [Required]
        [DisplayName("Available")]
        public bool IsAvailable { get; set; }
        [Required]
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes ProductTypes { get; set; }
        [Required]
        [DisplayName("Special Product Tag")]
        public int TagId { get; set; }
        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
    }
}
