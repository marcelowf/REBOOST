using System;
using System.Linq;
using System.Threading;
using Reboost.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Reboost.Services
{
    public class TokenCleanupService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public TokenCleanupService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _timer = new Timer(CleanupTokens, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private void CleanupTokens(object? state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ReboostDbContext>();
                var cutoffTime = DateTime.Now.AddMinutes(-10);
                var oldTokens = context.Tokens.Where(t => t.CreatedAt < cutoffTime).ToList();

                if (oldTokens.Any())
                {
                    context.Tokens.RemoveRange(oldTokens);
                    context.SaveChanges();
                }
            }
        }
    }
}
