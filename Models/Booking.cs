namespace TriCourier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Booking")]
    public partial class Booking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Booking ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name ="Pick-up address")]
        public string From_addr { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name ="Destination address")]
        public string To_Addr { get; set; }

        [Column(TypeName = "date")]
        [Display(Name ="Booking date")]
        public DateTime Booking_Date { get; set; }

        public int Weight { get; set; }

        public int Category_Id { get; set; }

        public int Delivery_Agent { get; set; }

        public int Customer_ID { get; set; }

        public virtual Category Category { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Delivery_Agent Delivery_Agent1 { get; set; }
    }
}
