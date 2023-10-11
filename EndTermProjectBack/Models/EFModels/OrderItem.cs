namespace EndTermProjectBack.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        public int ProductPrice { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
