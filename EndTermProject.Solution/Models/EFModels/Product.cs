namespace EndTermProject.Solution.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            Favorites = new HashSet<Favorite>();
            OrderItems = new HashSet<OrderItem>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int BrandId { get; set; }

        public int CapacityId { get; set; }

        public int SecondaryCategoryId { get; set; }

        public int NoteId { get; set; }

        public int Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(500)]
        public string Method { get; set; }

        [StringLength(500)]
        public string Ingredient { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public int Stock { get; set; }

        public bool Enabled { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Capacity Capacity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartItem> CartItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual Note Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual SecondaryCategory SecondaryCategory { get; set; }
    }
}
