namespace Ictshop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductID { get; set; }

        [StringLength(50)]
        [Required]
        public string ProductName { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public int? Quantity { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Description { get; set; }

        [Required]
        public int? Memory { get; set; }

        [Required]
        public bool? NewProduct { get; set; }

        [Required]
        public int? Ram { get; set; }

        [StringLength(10)]
        [Required]
        public string Image { get; set; }


        public int? BrandID { get; set; }

        public int? CateID { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
