using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reboost
{
    public class Rent
    {
        private int _Id;
        private bool _IsActive;
        private string? _BatteryId;
        private DateTime _BeginDate;
        private DateTime? _FinishDate;
        private int _FkCabinetFromId;
        private int _FkCabinetToId;
        private int _FkUserId;
        private int _FkBatteryId;

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

        public string? BatteryId
        {
            get { return _BatteryId; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _BatteryId = value;
                }
                else
                {
                    throw new ArgumentException("BatteryId cannot be null or empty.");
                }
            }
        }

        [Required]
        public DateTime BeginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }

        public DateTime? FinishDate
        {
            get { return _FinishDate; }
            set { _FinishDate = value; }
        }

        [Required]
        [ForeignKey("FK_Rent_CabinetFrom")]
        public int FkCabinetFromId
        {
            get { return _FkCabinetFromId; }
            set { _FkCabinetFromId = value; }
        }

        [ForeignKey("FK_Rent_CabinetTo")]
        public int FkCabinetToId
        {
            get { return _FkCabinetToId; }
            set { _FkCabinetToId = value; }
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
    }
}
