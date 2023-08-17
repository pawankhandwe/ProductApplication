using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApplication.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Column("productName",TypeName ="varchar(200)")]
        public string Name { get; set; }
        [Column("productPrice", TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        [Column("productName", TypeName = "varchar(500)")]
        public Category Category { get; set; }
    }
}
