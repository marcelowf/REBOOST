using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost
{
    public class CabinetBattery
    {
        private int _Id;
        private int _Order;
        private int _FkCabinetId;
        private int _FkBatteryId;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [Required]
        public int Order
        {
            get { return _Order; }
            set
            {
                if (value >= 0)
                {
                    _Order = value;
                }
                else
                {
                    throw new ArgumentException("Order must be a positive integer.");
                }
            }
        }

        [Required]
        [ForeignKey("FK_CabinetBattery_Cabinet")]
        public int FkCabinetId
        {
            get { return _FkCabinetId; }
            set
            {
                if (value > 0)
                {
                    _FkCabinetId = value;
                }
                else
                {
                    throw new ArgumentException("FkCabinetId must be a positive integer.");
                }
            }
        }

        [Required]
        [ForeignKey("FK_CabinetBattery_Battery")]
        public int FkBatteryId
        {
            get { return _FkBatteryId; }
            set
            {
                if (value > 0)
                {
                    _FkBatteryId = value;
                }
                else
                {
                    throw new ArgumentException("FkBatteryId must be a positive integer.");
                }
            }
        }
    }
}
