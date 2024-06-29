namespace StockWebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        [Column("username")]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(200)]
        [Column("hashed_password")]
        public string HashedPassword { get; set; } = "";

        [Required]
        [EmailAddress]
        [StringLength(255)]
        [Column("email")]
        public string Email { get; set; } = "";

        [Required]
        [Phone]
        [StringLength(20)]
        [Column("phone")]
        public string? Phone { get; set; }

        [StringLength(255)]
        [Column("full_name")]
        public string? FullName { get; set; }
        [Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(200)]
        [Column("country")]
        public string? Country { get; set; }

        public ICollection<Watchlist>? Watchlists { get; set; }
    }

}
