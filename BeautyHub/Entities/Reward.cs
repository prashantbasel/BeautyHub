using BeautyHub.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeautyHub.Entities
{
    public class Reward
    {
        public int Id { get; set; }
        public int Star { get; set; }
        public double Expense { get; set; }
        public double TotalExpense { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

    }
}