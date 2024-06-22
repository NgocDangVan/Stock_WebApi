using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StockWebApi.Models
{
    [Table("stocks")]
    public class Stock
    {
        [Key]
        [Column("stock_id")]
        public int StockId { get; set; }

        [Required]
        [StringLength(10)]
        [Column("symbol")]
        public string Symbol { get; set; } = "";

        [Required]
        [StringLength(255)]
        [Column("company_name")]
        public string CompanyName { get; set; } = "";

        [Column("market_cap")]
        [Range(0, (double)decimal.MaxValue, ErrorMessage = "Market cap must be a positive value.")]
        public decimal? MarketCap { get; set; }

        [StringLength(200)]
        [Column("sector")]
        public string Sector { get; set; }

        [StringLength(200)]
        [Column("industry")]
        public string Industry { get; set; } = "";

        [StringLength(200)]
        [Column("sector_en")]
        public string SectorEn { get; set; } = "";

        [StringLength(200)]
        [Column("industry_en")]
        public string IndustryEn { get; set; }

        [StringLength(50)]
        [Column("stock_type")]
        public string StockType { get; set; } = "";

        [Column("rank")]
        [Range(0, int.MaxValue, ErrorMessage = "Rank must be a non-negative integer.")]
        public int Rank { get; set; } = 0;

        [StringLength(200)]
        [Column("rank_source")]
        public string RankSource { get; set; } = "";

        [StringLength(255)]
        [Column("reason")]
        public string Reason { get; set; } = "";
        public ICollection<Watchlist>? Watchlists { get; set; }
    }
}
