using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseLog.Data;
using ExpenseLog.Models;

namespace ExpenseLog.Pages.Expenses
{
    public class DetailsModel : PageModel
    {
        private readonly ExpenseLog.Data.ExpenseLogContext _context;

        public DetailsModel(ExpenseLog.Data.ExpenseLogContext context)
        {
            _context = context;
        }

        public Expense Expense { get; set; } = default!;



        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Expense = await _context.Expense
                .Include(e => e.User) // Include the related User
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Expense == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
