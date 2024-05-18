using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost
{
    public class Battery
    {
        private int _Id;
        private bool _IsActive;
        private string? _ExternalCode;
        private string? _Model;
        private string? _Brand;
        private float? _Capacity;
        private float _PricePerHour;
        private float _TotalPrice;

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
        public string? ExternalCode
        {
            get { return _ExternalCode; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _ExternalCode = value;
                }
                else
                {
                    throw new ArgumentException("ExternalCode cannot be null or empty.");
                }
            }
        }

        [MaxLength(30)]
        public string? Model
        {
            get { return _Model; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Model = value;
                }
                else
                {
                    throw new ArgumentException("Model cannot be null or empty.");
                }
            }
        }

        [MaxLength(30)]
        public string? Brand
        {
            get { return _Brand; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _Brand = value;
                }
                else
                {
                    throw new ArgumentException("Brand cannot be null or empty.");
                }
            }
        }

        [Range(0, float.MaxValue, ErrorMessage = "PricePerHour must be a non-negative number.")]
        public float? Capacity
        {
            get { return _Capacity; }
            set { _Capacity = value; }
        }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "PricePerHour must be a non-negative number.")]
        public float PricePerHour
        {
            get { return _PricePerHour; }
            set { _PricePerHour = value; }
        }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "TotalPrice must be a non-negative number.")]
        public float TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
    }
}
