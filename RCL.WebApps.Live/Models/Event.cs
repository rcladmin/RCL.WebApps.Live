#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RCL.WebApps.Live.Models
{
    [Table(name: "rcl_app_live_event")]
    public class Event
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        [MaxLength(550)]
        public string description { get; set; }

        [Required]
        [Display(Name = "Sorting")]
        public int sorting { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.DateTime)]
        public DateTime start { get; set; }

        [Required]
        [Display(Name = "Duration (Hrs)")]
        public int duration { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string status { get; set; }

        [Required]
        [Display(Name = "Presenter Email")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string presenterEmail { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Precision(18,2)]
        public decimal price { get; set; }

        [Required]
        [Display(Name = "Currency")]
        [DataType(DataType.Text)]
        [MaxLength(5)]
        public string currency { get; set; }

        [Required]
        [Display(Name = "Group Id")]
        public int groupId { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string image { get; set; }

        [Display(Name = "Live Url")]
        [DataType(DataType.Text)]
        [MaxLength(350)]
        public string liveUrl { get; set; }

    }
}
