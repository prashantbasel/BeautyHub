using BeautyHub.Models;
using System;

namespace BeautyHub.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Error { get; set; }
        public string Detail { get; set; }
        public DateTime Timestamp { get; set; }
    }
}