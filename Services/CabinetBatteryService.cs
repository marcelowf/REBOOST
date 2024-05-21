namespace Reboost.Services
{
    public class CabinetBatteryService
    {
        private readonly ReboostDbContext _context;

        public CabinetBatteryService(ReboostDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CabinetBattery> GetAllCabinetBattery()
        {
            return _context.CabinetBatteries.ToList();
        }

        public CabinetBattery GetByIdCabinetBattery(int id)
        {
            var cabinetBattery = _context.CabinetBatteries.Find(id);
            if (cabinetBattery == null)
            {
                throw new ArgumentException("Cabinet battery not found.");
            }
            return cabinetBattery;
        }

        public CabinetBattery CreateCabinetBattery(CabinetBattery cabinetBattery)
        {
            if (cabinetBattery == null)
            {
                throw new ArgumentNullException(nameof(cabinetBattery), "Cabinet battery object cannot be null.");
            }

            if (cabinetBattery.Id != 0)
            {
                throw new ArgumentException("New cabinet battery must have Id set to 0.");
            }

            _context.CabinetBatteries.Add(cabinetBattery);
            _context.SaveChanges();
            return cabinetBattery;
        }

        public void UpdateCabinetBattery(CabinetBattery cabinetBattery)
        {
            if (cabinetBattery == null)
            {
                throw new ArgumentNullException(nameof(cabinetBattery), "Cabinet battery object cannot be null.");
            }

            if (cabinetBattery.Id <= 0)
            {
                throw new ArgumentException("Cabinet battery Id must be greater than 0.");
            }

            var existingCabinetBattery = _context.CabinetBatteries.Find(cabinetBattery.Id);
            if (existingCabinetBattery == null)
            {
                throw new ArgumentException("Cabinet battery not found.");
            }

            _context.CabinetBatteries.Update(cabinetBattery);
            _context.SaveChanges();
        }

        public void DeleteCabinetBattery(int id)
        {
            var cabinetBatteryToDelete = _context.CabinetBatteries.Find(id);
            if (cabinetBatteryToDelete == null)
            {
                throw new ArgumentException("Cabinet battery not found.");
            }

            _context.CabinetBatteries.Remove(cabinetBatteryToDelete);
            _context.SaveChanges();
        }

        public IEnumerable<CabinetBattery> GetFilteredCabinetBatteries(
            int? id,
            int? order,
            int? fkCabinetId,
            int? fkBatteryId)
        {
            var query = _context.CabinetBatteries.AsQueryable();

            if (id.HasValue)
            {
                query = query.Where(cb => cb.Id == id.Value);
            }
            if (order.HasValue)
            {
                query = query.Where(cb => cb.Order == order.Value);
            }
            if (fkCabinetId.HasValue)
            {
                query = query.Where(cb => cb.FkCabinetId == fkCabinetId.Value);
            }
            if (fkBatteryId.HasValue)
            {
                query = query.Where(cb => cb.FkBatteryId == fkBatteryId.Value);
            }

            return query.ToList();
        }
    }
}