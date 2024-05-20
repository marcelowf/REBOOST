using Reboost;

public class CabinetService
{
    private readonly ReboostDbContext _context;

    public CabinetService(ReboostDbContext context)
    {
        _context = context;
    }

    public void PostCabinet(Cabinet cabinet)
    {
        if (cabinet == null) throw new ArgumentNullException(nameof(cabinet));

        cabinet.Id = 0;
        _context.Cabinets.Add(cabinet);
        _context.SaveChanges();
    }

    public List<Battery> GetBatteriesByCabinetId(int cabinetId)
    {
        try
        {
            var batteryIds = _context.CabinetBatteries
                .Where(cb => cb.FkCabinetId == cabinetId)
                .Select(cb => cb.FkBatteryId)
                .ToList();

            var batteries = _context.Batteries
                .Where(b => batteryIds.Contains(b.Id))
                .ToList();

            return batteries;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error retrieving batteries by cabinet ID", ex);
        }
    }

    public Cabinet? GetCabinetById(int id)
    {
        return _context.Cabinets.FirstOrDefault(c => c.Id == id);
    }

    public List<Cabinet> GetAllCabinets()
    {
        return _context.Cabinets.ToList();
    }

    public List<Cabinet> GetFilteredCabinets(int? cabinetId, string? AddressZipCode)
    {
        var query = _context.Cabinets.AsQueryable();

        if (cabinetId.HasValue)
        {
            query = query.Where(c => c.Id == cabinetId.Value);
        }

        if (!string.IsNullOrEmpty(AddressZipCode))
        {
            query = query.Where(c => c.AddressZipCode == AddressZipCode);
        }

        return query.ToList();
    }

    public bool DeleteCabinet(int id)
    {
        var cabinet = _context.Cabinets.Find(id);
        if (cabinet == null) return false;

        _context.Cabinets.Remove(cabinet);
        _context.SaveChanges();
        return true;
    }

    public Cabinet? UpdateCabinet(int id, Cabinet cabinet)
    {
        var existingCabinet = _context.Cabinets.FirstOrDefault(c => c.Id == id);
        if (existingCabinet != null)
        {
            existingCabinet.IsActive = cabinet.IsActive;
            existingCabinet.ExternalCode = cabinet.ExternalCode;
            existingCabinet.AddressZipCode = cabinet.AddressZipCode;
            existingCabinet.AddressStreet = cabinet.AddressStreet;
            existingCabinet.AddressNumber = cabinet.AddressNumber;
            existingCabinet.AddressDistrict = cabinet.AddressDistrict;
            existingCabinet.AddressLatitude = cabinet.AddressLatitude;
            existingCabinet.AddressLongitude = cabinet.AddressLongitude;
            existingCabinet.DrawerNumber = cabinet.DrawerNumber;

            _context.SaveChanges();
            return existingCabinet;
        }
        return null;
    }

    public bool SoftDeleteCabinet(int id)
    {
        var cabinet = _context.Cabinets.Find(id);
        if (cabinet == null) return false;

        cabinet.IsActive = false;
        _context.Cabinets.Update(cabinet);
        _context.SaveChanges();
        return true;
    }
}
