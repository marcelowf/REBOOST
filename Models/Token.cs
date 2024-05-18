using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost.Models
{
    public class Token
    {
        private int _Id;
        private string? _Value;
        private int _FkCabinetId;
        private int _FkBatteryId;
        private int _FkUserId;
        private DateTime? _CreatedAt;

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
        [ForeignKey("FK_Rent_Cabinet")]
        public int FkCabinetId
        {
            get { return _FkCabinetId; }
            set { _FkCabinetId = value; }
        }

        [Required]
        [ForeignKey("FK_Rent_User")]
        public int FkUserId
        {
            get { return _FkUserId; }
            set { _FkUserId = value; }
        }

        [Required]
        [ForeignKey("FK_Rent_Battery")]
        public int FkBatteryId
        {
            get { return _FkBatteryId; }
            set { _FkBatteryId = value; }
        }
        
        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt
        {
            get { return _CreatedAt; }
            set { _CreatedAt = value; }
        }
    }
}