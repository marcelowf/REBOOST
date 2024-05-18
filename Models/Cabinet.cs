using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost
{
    public class Cabinet
    {
        private int _Id;
        private string? _ExternalCode;
        private string? _AddressZipCode;
        private string? _AddressStreet;
        private string? _AddressNumber;
        private string? _AddressDistrict;
        private float? _AddressLatitude;
        private float? _AddressLongitude;
        private int? _DrawerNumber;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [Required]
        public string? ExternalCode
        {
            get { return _ExternalCode; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("ExternalCode cannot be null or empty.");
                }
                _ExternalCode = value;
            }
        }

        [MaxLength(30)]
        public string? AddressZipCode
        {
            get { return _AddressZipCode; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _AddressZipCode = value;
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("AddressZipCode cannot be longer than 30 characters.");
                }
            }
        }

        [MaxLength(30)]
        public string? AddressStreet
        {
            get { return _AddressStreet; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _AddressStreet = value;
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("AddressStreet cannot be longer than 30 characters.");
                }
            }
        }

        [MaxLength(10)]
        public string? AddressNumber
        {
            get { return _AddressNumber; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 10)
                {
                    _AddressNumber = value;
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("AddressNumber cannot be longer than 10 characters.");
                }
            }
        }

        [MaxLength(30)]
        public string? AddressDistrict
        {
            get { return _AddressDistrict; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length <= 30)
                {
                    _AddressDistrict = value;
                }
                else if (!string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("AddressDistrict cannot be longer than 30 characters.");
                }
            }
        }

        public float? AddressLatitude
        {
            get { return _AddressLatitude; }
            set
            {
                if (value >= -90 && value <= 90)
                {
                    _AddressLatitude = value;
                }
                else
                {
                    throw new ArgumentException("AddressLatitude must be between -90 and 90.");
                }
            }
        }

        public float? AddressLongitude
        {
            get { return _AddressLongitude; }
            set
            {
                if (value >= -180 && value <= 180)
                {
                    _AddressLongitude = value;
                }
                else
                {
                    throw new ArgumentException("AddressLongitude must be between -180 and 180.");
                }
            }
        }

        public int? DrawerNumber
        {
            get { return _DrawerNumber; }
            set
            {
                if (value >= 0)
                {
                    _DrawerNumber = value;
                }
                else
                {
                    throw new ArgumentException("DrawerNumber must be a non-negative integer.");
                }
            }
        }
    }
}
