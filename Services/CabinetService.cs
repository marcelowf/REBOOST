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

    public List<Battery> GetBatteriesByCabinetId(
        int cabinetId,
        int? id,
        bool? isActive,
        string externalCode,
        string model,
        string brand,
        float? capacity,
        float? pricePerHour,
        float? totalPrice)
    {
        try
        {
            var batteryIds = _context.CabinetBatteries
                .Where(cb => cb.FkCabinetId == cabinetId)
                .Select(cb => cb.FkBatteryId)
                .ToList();

            var query = _context.Batteries.AsQueryable();

            if (batteryIds.Any())
            {
                query = query.Where(b => batteryIds.Contains(b.Id));
            }

            if (id.HasValue)
            {
                query = query.Where(b => b.Id == id.Value);
            }

            if (isActive.HasValue)
            {
                query = query.Where(b => b.IsActive == isActive.Value);
            }

            if (!string.IsNullOrEmpty(externalCode))
            {
                query = query.Where(b => b.ExternalCode == externalCode);
            }

            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(b => b.Model == model);
            }

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(b => b.Brand == brand);
            }

            if (capacity.HasValue)
            {
                query = query.Where(b => b.Capacity == capacity.Value);
            }

            if (pricePerHour.HasValue)
            {
                query = query.Where(b => b.PricePerHour == pricePerHour.Value);
            }

            if (totalPrice.HasValue)
            {
                query = query.Where(b => b.TotalPrice == totalPrice.Value);
            }

            return query.ToList();
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

    public IEnumerable<Cabinet> GetFilteredCabinets(
        int? id,
        bool? isActive,
        string? externalCode,
        string? addressZipCode,
        string? addressStreet,
        string? addressNumber,
        string? addressDistrict,
        float? addressLatitude,
        float? addressLongitude,
        int? drawerNumber)
    {
        var query = _context.Cabinets.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(c => c.Id == id.Value);
        }
        if (isActive.HasValue)
        {
            query = query.Where(c => c.IsActive == isActive.Value);
        }
        if (!string.IsNullOrEmpty(externalCode))
        {
            query = query.Where(c => c.ExternalCode.Contains(externalCode));
        }
        if (!string.IsNullOrEmpty(addressZipCode))
        {
            query = query.Where(c => c.AddressZipCode.Contains(addressZipCode));
        }
        if (!string.IsNullOrEmpty(addressStreet))
        {
            query = query.Where(c => c.AddressStreet.Contains(addressStreet));
        }
        if (!string.IsNullOrEmpty(addressNumber))
        {
            query = query.Where(c => c.AddressNumber.Contains(addressNumber));
        }
        if (!string.IsNullOrEmpty(addressDistrict))
        {
            query = query.Where(c => c.AddressDistrict.Contains(addressDistrict));
        }
        if (addressLatitude.HasValue)
        {
            query = query.Where(c => c.AddressLatitude == addressLatitude.Value);
        }
        if (addressLongitude.HasValue)
        {
            query = query.Where(c => c.AddressLongitude == addressLongitude.Value);
        }
        if (drawerNumber.HasValue)
        {
            query = query.Where(c => c.DrawerNumber == drawerNumber.Value);
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
