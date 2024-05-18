// Services/BatteryService.cs
using System;
using System.Collections.Generic;
using System.Linq;
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

    public bool SoftDeleteBattery(int id)
    {
        var battery = _context.Batteries.Find(id);
        if (battery == null) return false;
        battery.IsActive = false;
        _context.SaveChanges();
        return true;
    }
}
