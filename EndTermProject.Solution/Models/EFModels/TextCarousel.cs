namespace EndTermProject.Solution.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TextCarousel")]
    public partial class TextCarousel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public DateTime CreatTime { get; set; }

        public bool Enabled { get; set; }
    }
}
