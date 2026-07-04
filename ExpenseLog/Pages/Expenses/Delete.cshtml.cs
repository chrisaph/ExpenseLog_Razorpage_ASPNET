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
    public class DeleteModel : PageModel
    {
        private readonly ExpenseLog.Data.ExpenseLogContext _context;

        public DeleteModel(ExpenseLog.Data.ExpenseLogContext context)
        {
            _context = context;
        }

        [BindProperty]
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


            //expense = await _context.expense
            //    .include(e => e.user) // include the related user
            //    .firstordefaultasync(m => m.id == id);

            //if (expense == null)
            //{
            //    return notfound();
            //}

            //return page();

        }




        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expense = await _context.Expense.FindAsync(id);
            if (expense != null)
            {
                Expense = expense;
                _context.Expense.Remove(Expense);
                await _context.SaveChangesAsync();
            }


            return RedirectToPage("./Index");
        }
    }
}
