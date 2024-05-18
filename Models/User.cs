using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost
{
    public class User
    {
        private int _Id;
        private bool _IsActive;
        private string? _Name;
        private string? _Email;
        private string? _Password;
        private DateTime? _LastLogin;
        private DateTime? _CreatedAt;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public bool IsActive 
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [Required]
        [MaxLength(30)]
        public string? Name
        {
            get { return _Name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Name = value;
                }
                else
                {
                    throw new ArgumentException("Name cannot be null or empty.");
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
        [MaxLength(30)]
        public string? Password
        {
            get { return _Password; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Password = value;
                }
                else
                {
                    throw new ArgumentException("Password cannot be null or empty.");
                }
            }
        }

        [DataType(DataType.DateTime)]
        public DateTime? LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt
        {
            get { return _CreatedAt; }
            set { _CreatedAt = value; }
        }
    }
}
