#nullable disable

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RCL.WebApps.Live.Models
{
    [Table(name: "rcl_app_live_transaction")]
    public class Transaction
    {
        #region Transaction

        [Key]
        [Display(Name = "Id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "External Id")]
        public int? externalId { get; set; }

        [Required]
        [Display(Name = "Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime? transactionDate { get; set; }

        [Required]
        [Display(Name = "Type")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string transactionType { get; set; }

        [Required]
        [Display(Name = "Order Id")]
        [DataType(DataType.Text)]
        [MaxLength(150)]
        public string orderId { get; set; }

        [Required]
        [Display(Name = "Status")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string status { get; set; }

        #endregion

        #region Charge

        [Display(Name = "Charge Id")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string chargeId { get; set; }

        [Display(Name = "Charge Date")]
        [DataType(DataType.Date)]
        public DateTime? chargeDate { get; set; }

        [Display(Name = "Charge Amount")]
        public int? chargeAmount { get; set; }

        [Display(Name = "Charge Amount Currency")]
        [DataType(DataType.Text)]
        [MaxLength(5)]
        public string chargeAmountCurrency { get; set; }

        [Display(Name = "Charge Name")]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string chargeName { get; set; }

        [Display(Name = "Charge Email")]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string chargeEmail { get; set; }

        [Display(Name = "Charge Country")]
        [DataType(DataType.Text)]
        [MaxLength(5)]
        public string chargeCountry { get; set; }

        #endregion

        #region Refund

        [Display(Name = "Refund Id")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string refundId { get; set; }

        [Display(Name = "Refund Date")]
        [DataType(DataType.Date)]
        public DateTime? refundDate { get; set; }

        [Display(Name = "Refund Amount")]
        public int? refundAmount { get; set; }

        [Display(Name = "Refund Amount Currency")]
        [DataType(DataType.Text)]
        [MaxLength(5)]
        public string refundAmountCurrency { get; set; }

        [Display(Name = "Refund Reason")]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string refundReason { get; set; }

        #endregion

        #region Dispute

        [Display(Name = "Dispute Id")]
        [DataType(DataType.Text)]
        [MaxLength(50)]
        public string disputeId { get; set; }

        [Display(Name = "Dispute Date")]
        [DataType(DataType.Date)]
        public DateTime? disputeDate { get; set; }

        [Display(Name = "Dispute Amount")]
        public int? disputeAmount { get; set; }

        [Display(Name = "Dispute Amount Currency")]
        [DataType(DataType.Text)]
        [MaxLength(5)]
        public string disputeAmountCurrency { get; set; }

        [Display(Name = "Dispute Reason")]
        [DataType(DataType.Text)]
        [MaxLength(250)]
        public string disputeReason { get; set; }

        #endregion
    }
}
