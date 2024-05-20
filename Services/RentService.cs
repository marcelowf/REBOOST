namespace Reboost
{
    public class RentService
    {
        private readonly ReboostDbContext _context;

        public RentService(ReboostDbContext context)
        {
            _context = context;
        }

        public void RentBattery(Rent rent)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == rent.FkUserId);
            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            var battery = _context.Batteries.FirstOrDefault(b => b.Id == rent.FkBatteryId);
            if (battery == null)
            {
                throw new ArgumentException("Bateria não encontrada.");
            }

            var cabinetFrom = _context.Cabinets.FirstOrDefault(c => c.Id == rent.FkCabinetFromId);
            if (cabinetFrom == null)
            {
                throw new ArgumentException("Gabinete de origem não encontrado.");
            }

            var cabinetTo = _context.Cabinets.FirstOrDefault(c => c.Id == rent.FkCabinetToId);
            if (cabinetTo == null)
            {
                throw new ArgumentException("Gabinete de destino não encontrado.");
            }

            var rentedBattery = _context.Rents.FirstOrDefault(r => r.FkBatteryId == rent.FkBatteryId && r.IsActive);
            if (rentedBattery != null)
            {
                throw new ArgumentException("Bateria já está alugada.");
            }

            rent.IsActive = true;
            // Demanda para a retirada da funcionalidade    
            // rent.BeginDate = DateTime.UtcNow;
            _context.Rents.Add(rent);
            _context.SaveChanges();
        }

        public Rent? GetRentById(int id)
        {
            return _context.Rents.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Rent> GetAllRents()
        {
            return _context.Rents.ToList();
        }

        public IEnumerable<Rent> GetRentsByUserId(int userId)
        {
            return _context.Rents.Where(r => r.FkUserId == userId).ToList();
        }

        public bool DeleteRent(int id)
        {
            var rent = _context.Rents.FirstOrDefault(r => r.Id == id);
            if (rent == null)
            {
                return false;
            }

            _context.Rents.Remove(rent);
            _context.SaveChanges();
            return true;
        }
        
        public Rent? UpdateRent(int id, Rent rent)
        {
            var existingRent = _context.Rents.FirstOrDefault(r => r.Id == id);
            if (existingRent != null)
            {
                existingRent.BeginDate = rent.BeginDate;
                existingRent.FinishDate = rent.FinishDate;
                existingRent.FkCabinetFromId = rent.FkCabinetFromId;
                existingRent.FkCabinetToId = rent.FkCabinetToId;
                existingRent.FkUserId = rent.FkUserId;
                existingRent.FkBatteryId = rent.FkBatteryId;

                _context.SaveChanges();
                return existingRent;
            }
            return null;
        }

        public bool SoftDeleteRent(int id)
        {
            var rent = _context.Rents.FirstOrDefault(r => r.Id == id);
            if (rent == null)
            {
                return false;
            }

            rent.IsActive = false;
            _context.SaveChanges();
            return true;
        }

        public List<Rent> GetRents(int? userId)
        {
            try
            {
                var query = _context.Rents.AsQueryable();

                if (userId.HasValue)
                {
                    query = query.Where(r => r.FkUserId == userId.Value);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving rents", ex);
            }
        }
    }
}
