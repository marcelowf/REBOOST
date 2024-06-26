using System.Security.Cryptography;
using System.Text;
using Reboost;
using Reboost.Models;

public class UserService
{
    private readonly ReboostDbContext _context;

    public UserService(ReboostDbContext context)
    {
        _context = context;
    }
    
    public User? PostUser(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
        if (existingUser != null)
        {
            return null;
        }

        user.Id = 0;
        user.IsActive = true;
        user.Billing = 0;
        user.LastLogin = DateTime.UtcNow;
        user.CreatedAt = DateTime.UtcNow;

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }

    public User? ValidateUser(string email, string password)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);

        if (user == null)
        {
            return null;
        }

        return user;
    }

    public TokenLogin GenerateToken(User user)
    {
        string combinedString = $"{user.Id}-{user.Email}";

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedString));

            StringBuilder hexString = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hexString.Append(b.ToString("x2"));
            }

            string tokenValue = hexString.ToString().Substring(0, 8);

            var tokenLogin = new TokenLogin
            {
                Value = tokenValue,
                Email = user.Email,
                FkUserId = user.Id
            };

            _context.TokenLogins.Add(tokenLogin);
            _context.SaveChanges();

            return tokenLogin;
        }
    }

    public (string? Email, int UserId)? DecodeToken(string token)
    {
        var tokenEntity = _context.TokenLogins.FirstOrDefault(t => t.Value == token);

        if (tokenEntity == null)
        {
            return null;
        }

        var user = _context.Users.FirstOrDefault(u => u.Id == tokenEntity.FkUserId);

        if (user == null)
        {
            return null;
        }

        return (user.Email, user.Id);
    }

    public User? GetUserById(int id)
    {
        try
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error retrieving user by ID", ex);
        }
    }

    public IEnumerable<User> GetFilteredUsers(
        int? id,
        bool? isAdmin,
        bool? isActive,
        float? billing,
        string? name,
        string? email,
        string? password,
        DateTime? lastLogin,
        DateTime? createdAt)
    {
        var query = _context.Users.AsQueryable();

        if (id.HasValue)
        {
            query = query.Where(u => u.Id == id.Value);
        }
        if (isAdmin.HasValue)
        {
            query = query.Where(u => u.IsAdmin == isAdmin.Value);
        }
        if (isActive.HasValue)
        {
            query = query.Where(u => u.IsActive == isActive.Value);
        }
        if (billing.HasValue)
        {
            query = query.Where(u => u.Billing == billing.Value);
        }
        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(u => u.Name.Contains(name));
        }
        if (!string.IsNullOrEmpty(email))
        {
            query = query.Where(u => u.Email.Contains(email));
        }
        if (!string.IsNullOrEmpty(password))
        {
            query = query.Where(u => u.Password.Contains(password));
        }
        if (lastLogin.HasValue)
        {
            query = query.Where(u => u.LastLogin >= lastLogin.Value);
        }
        if (createdAt.HasValue)
        {
            query = query.Where(u => u.CreatedAt >= createdAt.Value);
        }

        return query.ToList();
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
