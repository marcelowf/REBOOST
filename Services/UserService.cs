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
        user.Billing = 0;
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

    public User? UpdateUser(int id, User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);
        if (existingUser != null)
        {
            existingUser.IsActive = user.IsActive;
            existingUser.IsAdmin = user.IsAdmin;
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Billing = user.Billing;
            existingUser.LastLogin = user.LastLogin;

            _context.SaveChanges();
            return existingUser;
        }
        return null;
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
