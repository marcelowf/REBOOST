using System;
using System.Collections.Generic;
using System.Linq;
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

        cabinet.Id = 0; // Ensure the ID is reset to prevent updates
        _context.Cabinets.Add(cabinet);
        _context.SaveChanges();
    }

    public Cabinet GetCabinetById(int id)
    {
        var cabinet = _context.Cabinets.Find(id);
        if (cabinet == null)
        {
            throw new KeyNotFoundException("Cabinet not found.");
        }
        return cabinet;
    }

    public List<Cabinet> GetAllCabinets()
    {
        return _context.Cabinets.ToList();
    }

    public bool DeleteCabinet(int id)
    {
        var cabinet = _context.Cabinets.Find(id);
        if (cabinet == null) return false;

        _context.Cabinets.Remove(cabinet);
        _context.SaveChanges();
        return true;
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
