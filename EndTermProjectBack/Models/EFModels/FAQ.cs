namespace EndTermProjectBack.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FAQ
    {
        public int Id { get; set; }

        public int SecondaryCategoriesId { get; set; }

        [Required]
        [StringLength(3000)]
        public string Question { get; set; }

        [Required]
        [StringLength(3000)]
        public string Answer { get; set; }

        public virtual SecondaryCategory SecondaryCategory { get; set; }
    }
}
