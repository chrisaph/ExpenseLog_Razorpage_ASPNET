using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseLog.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public string? Category { get; set; }
        public string? Product { get; set; }
        public string? Description { get; set; }
        public string? Shop { get; set; }
        public string? Mall { get; set; }
        public string? Contact { get; set; }
        public DateTime? Date { get; set; }

        // Foreign key
        public string? UserID { get; set; }

        // Navigation property (no need for ForeignKey attribute here)
        public IdentityUser? User { get; set; }
    }
}