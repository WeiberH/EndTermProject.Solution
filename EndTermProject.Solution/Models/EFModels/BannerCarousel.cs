namespace EndTermProject.Solution.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BannerCarousel")]
    public partial class BannerCarousel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Headline { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public DateTime CreatTime { get; set; }

        [StringLength(50)]
        public string Image { get; set; }

        public bool Enabled { get; set; }
    }
}
