using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeApi.Request
{
    public class InventoryAddRequest
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public string CreatedBy { get; set; }
    }
}
