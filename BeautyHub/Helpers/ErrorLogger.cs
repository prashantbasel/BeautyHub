using BeautyHub.Entities;
using BeautyHub.Models;
using System;

public class ErrorLogger
{
    private readonly ApplicationDbContext _context;

    public ErrorLogger(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Log(string error, string detail)
    {
        var log = new ErrorLog
        {
            Error = error,
            Detail = detail,
            Timestamp = DateTime.UtcNow
        };

        _context.ErrorLogs.Add(log);
        _context.SaveChanges();
    }
}
