using System;
using System.Linq;
using System.Threading;
using Reboost.Models;

namespace Reboost.Services
{
    public class TokenService
    {
        private readonly ReboostDbContext _context;
        private Timer _timer;

        public TokenService(ReboostDbContext context)
        {
            _context = context;
        }

        public string? GetTokenValue(int fkCabinetId, int fkBatteryId, int fkUserId)
        {
            var existingToken = _context.Tokens.FirstOrDefault(t =>
                t.FkCabinetId == fkCabinetId &&
                t.FkBatteryId == fkBatteryId &&
                t.FkUserId == fkUserId);

            if (existingToken != null)
            {
                return existingToken.Value;
            }
            else
            {
                var newValue = GenerateRandomToken();
                var newToken = new Token
                {
                    Value = newValue,
                    FkCabinetId = fkCabinetId,
                    FkBatteryId = fkBatteryId,
                    FkUserId = fkUserId,
                    CreatedAt = DateTime.Now
                };
                _context.Tokens.Add(newToken);
                _context.SaveChanges();
                return newValue;
            }
        }

        public string? ChangeTokenValue(int fkCabinetId, int fkBatteryId, int fkUserId)
        {
            var token = _context.Tokens.FirstOrDefault(t =>
                t.FkCabinetId == fkCabinetId &&
                t.FkBatteryId == fkBatteryId &&
                t.FkUserId == fkUserId);

            if (token != null)
            {
                var newValue = GenerateRandomToken();
                token.Value = newValue;
                token.CreatedAt = DateTime.Now;
                _context.SaveChanges();
                return newValue;
            }
            else
            {
                return null;
            }
        }

        private string GenerateRandomToken()
        {
            var random = new Random();
            int randomNumber = random.Next(100000000, 999999999);
            return randomNumber.ToString();
        }
    }
}
