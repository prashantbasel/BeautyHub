using BeautyHub.Models;
using System;

namespace BeautyHub.Entities
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserRole { get; set; }
        public string Action { get; set; }
        public string Resource { get; set; }
        public DateTime Timestamp { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
    }
}