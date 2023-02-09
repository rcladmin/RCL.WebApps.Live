#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCL.WebApps.Live.Models
{
    [Table(name: "rcl_app_live_event_attendee")]
    public class EventAttendee
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public string userId { get; set; }

        [Required]
        [Display(Name = "Event Id")]
        public int eventId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Order Id")]
        [MaxLength(150)]
        public string orderId { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string status { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Charge Date")]
        public DateTime chargeDate { get; set; }

        [Required]
        [Display(Name = "Transaction Id")]
        public int transactionId { get; set; }
    }
}
