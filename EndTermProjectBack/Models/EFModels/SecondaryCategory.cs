namespace EndTermProjectBack.Models.EFModels
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SecondaryCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SecondaryCategory()
        {
            FAQs = new HashSet<FAQ>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public int CategoryId { get; set; }

        [Required]
		[DisplayName("«~Ãþ")]
		[StringLength(30)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool Enabled { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FAQ> FAQs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
