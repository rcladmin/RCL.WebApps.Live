#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RCL.WebApps.Live.Models
{
    [Table(name: "rcl_app_live_group")]
    public class Group
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
        [DataType(DataType.MultilineText)]
        [MaxLength(550)]
        public string description { get; set; }

        [Required]
        [Display(Name = "Ranking")]
        public int ranking { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string status { get; set; }

        [Display(Name = "Cover Image")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string image { get; set; }
    }
}
