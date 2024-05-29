using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Model.DatabaseModel.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        
        public int VendorId { get; set; }

        public string ImageName { get; set; }

        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }
    }
}
