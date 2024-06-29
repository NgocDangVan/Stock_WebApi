using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockWebApi.Models
{
    [Table("covered_warrants")]
    public class CoveredWarrant
    {
        [Key]
        [Column("varrant_id")]
        public int WarrantId { get; set; }

        [Column("name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [ForeignKey("UnderlyingAsset")]
        [Column("underlying_asset_id")]
        public int UnderlyingAssetId { get; set; }

        [Column("issue_date")]
        public DateTime IssueDate { get; set; }

        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Column("strike_price")]
        public decimal StrikePrice { get; set; }

        [Column("warrant_type")]
        [MaxLength(50)]
        public string WarrantType { get; set; }

        // Navigation property
        public virtual Stock UnderlyingAsset { get; set; }
    }
}
