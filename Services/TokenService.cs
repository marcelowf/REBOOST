using Reboost.Models;

namespace Reboost.Services
{
    public class TokenService
    {
        private readonly ReboostDbContext _context;
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

        private string GenerateRandomToken()
        {
            var random = new Random();
            int randomNumber = random.Next(100000000, 999999999);
            return randomNumber.ToString();
        }
    }
}
