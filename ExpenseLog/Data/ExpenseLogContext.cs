using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ExpenseLog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ExpenseLog.Data
{
    public class ExpenseLogContext : IdentityDbContext<IdentityUser>
    {
        public ExpenseLogContext(DbContextOptions<ExpenseLogContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expense { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Expense>()
                .HasOne<IdentityUser>(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserID)
                .HasPrincipalKey(u => u.Id) // ensures correct PK
                .OnDelete(DeleteBehavior.SetNull); // optional
        }
    }
}
