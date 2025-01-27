using BeautyHub.Entities;
using BeautyHub.Models;
using System.Threading.Tasks;
using System;

public class ActivityLogger
{
    private readonly ApplicationDbContext _context;

    public ActivityLogger(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Log(string userId, string userRole, string action, string resource, string details, string status)
    {
        var log = new ActivityLog
        {
            UserId = userId,
            UserRole = userRole,
            Action = action,
            Resource = resource,
            Timestamp = DateTime.UtcNow,
            Details = details,
            Status = status
        };

        _context.ActivityLogs.Add(log);
        _context.SaveChanges();
    }
}
