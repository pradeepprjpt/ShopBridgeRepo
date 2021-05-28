using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridgeApi.Request
{
    public class InventoryUpdateRequest
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ModifiedBy { get; set; }
    }
}
