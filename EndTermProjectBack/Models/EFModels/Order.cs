namespace EndTermProjectBack.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public DateTime OrderTime { get; set; }

        public int PaytypeId { get; set; }

        public int StatusId { get; set; }

        public int DistrictsId { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Receiver { get; set; }

        [StringLength(50)]
        public string TelePhone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public virtual District District { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual Paytype Paytype { get; set; }

        public virtual Status Status { get; set; }
    }
}
