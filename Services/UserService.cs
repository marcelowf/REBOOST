using System;
using System.Collections.Generic;
using System.Linq;
using Reboost;

public class UserService
{
    private readonly ReboostDbContext _context;

    public UserService(ReboostDbContext context)
    {
        _context = context;
    }

    public void PostUser(User user)
    {
        user.Id = 0;
        user.IsActive = true;
        user.LastLogin = DateTime.UtcNow;
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.Find(id);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public bool DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }

    public bool SoftDeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null) return false;

        user.IsActive = false;
        _context.Users.Update(user);
        _context.SaveChanges();
        return true;
    }
}
