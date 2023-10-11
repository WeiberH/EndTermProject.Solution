namespace EndTermProject.Solution.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            SecondaryCategories = new HashSet<SecondaryCategory>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool Enabled { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecondaryCategory> SecondaryCategories { get; set; }
    }
}
