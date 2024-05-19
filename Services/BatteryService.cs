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
        return _context.Batteries.Find(id);
    }

    public List<Battery> GetAllBatteries()
    {
        return _context.Batteries.ToList();
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
