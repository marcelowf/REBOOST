using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost.Models
{
    public class TokenLogin
    {
        private int _Id;
        private string? _Value;
        private string? _Email;
        private int _FkUserId;
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        
        [Required]
        [MaxLength(200)]
        public string? Value
        {
            get { return _Value; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Value = value;
                }
                else
                {
                    throw new ArgumentException("Value cannot be null or empty.");
                }
            }
        }

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string? Email
        {
            get { return _Email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Email = value;
                }
                else
                {
                    throw new ArgumentException("Email cannot be null or empty.");
                }
            }
        }

        [Required]
        [ForeignKey("FK_Rent_User")]
        public int FkUserId
        {
            get { return _FkUserId; }
            set { _FkUserId = value; }
        }
    }
}