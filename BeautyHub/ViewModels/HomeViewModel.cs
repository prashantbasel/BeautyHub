using BeautyHub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeautyHub.ViewModels
{
    public class HomeViewModel
    {
        public List<Service> Services { get; set; }
        public List<OfficeLocation> Locations { get; set; }
        public Appointment Appointment { get; set; }
    }
}