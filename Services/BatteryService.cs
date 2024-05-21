using Reboost;

public class BatteryService
{
    private readonly ReboostDbContext _context;

    public BatteryService(ReboostDbContext context)
    {
        _context = context;
    }

    public void PostBattery(Battery battery)
    {
        battery.Id = 0;
        _context.Batteries.Add(battery);
        _context.SaveChanges();
    }

    public Battery? GetBatteryById(int id)
    {
        return _context.Batteries.FirstOrDefault(b => b.Id == id);
    }

    public List<Battery> GetAllBatteries()
    {
        return _context.Batteries.ToList();
    }

    public IEnumerable<Battery> GetFilteredBatteries(
        int? id,
        bool? isActive,
        string? externalCode,
        string? model,
        string? brand,
        float? capacity,
        float? pricePerHour,
        float? totalPrice)
    {
        var query = _context.Batteries.AsQueryable();

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
            query = query.Where(b => b.ExternalCode.Contains(externalCode));
        }
        if (!string.IsNullOrEmpty(model))
        {
            query = query.Where(b => b.Model.Contains(model));
        }
        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(b => b.Brand.Contains(brand));
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

    public bool DeleteBattery(int id)
    {
        var battery = _context.Batteries.Find(id);
        if (battery == null) return false;
        _context.Batteries.Remove(battery);
        _context.SaveChanges();
        return true;
    }

    public Battery? UpdateBattery(int id, Battery battery)
    {
        var existingBattery = _context.Batteries.FirstOrDefault(b => b.Id == id);
        if (existingBattery != null)
        {
            existingBattery.ExternalCode = battery.ExternalCode;
            existingBattery.Model = battery.Model;
            existingBattery.Brand = battery.Brand;
            existingBattery.Capacity = battery.Capacity;
            existingBattery.PricePerHour = battery.PricePerHour;
            existingBattery.TotalPrice = battery.TotalPrice;

            _context.SaveChanges();
            return existingBattery;
        }
        return null;
    }

    public bool SoftDeleteBattery(int id)
    {
        var battery = _context.Batteries.Find(id);
        if (battery == null) return false;
        battery.IsActive = false;
        _context.SaveChanges();
        return true;
    }
}
