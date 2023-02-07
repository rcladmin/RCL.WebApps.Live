#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "Start")]
        [DataType(DataType.DateTime)]
        public DateTime start { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public int duration { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string status { get; set; }

        [Required]
        [Display(Name = "Presenter Id")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string presenterId { get; set; }

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
