namespace EndTermProjectBack.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        public virtual Product Product { get; set; }
    }
}
