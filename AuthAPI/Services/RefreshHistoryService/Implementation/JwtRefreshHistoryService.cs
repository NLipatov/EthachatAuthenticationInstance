using AuthAPI.DB.DBContext;
using AuthAPI.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services.RefreshHistoryService.Implementation;

class JwtRefreshHistoryService : IJwtRefreshHistoryService
{
    private readonly AuthContext _context;

    public JwtRefreshHistoryService(AuthContext context)
    {
        _context = context;
    }
    public async Task<List<UserAccessRefreshEventLog>> GetUserHistory(string username)
    {
        var history = await _context.UserAccessRefreshEventLogs
            .Where(x => x.User.Username == username)
            .ToListAsync();

        return history;
    }
}