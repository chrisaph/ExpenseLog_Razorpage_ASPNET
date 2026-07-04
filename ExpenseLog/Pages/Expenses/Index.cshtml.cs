using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ExpenseLog.Data;
using ExpenseLog.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;


namespace ExpenseLog.Pages.Expenses
{
    public class IndexModel : PageModel
    {
        private readonly ExpenseLog.Data.ExpenseLogContext _context;

        public IndexModel(ExpenseLog.Data.ExpenseLogContext context)
        {
            _context = context;
        }

        public IList<Expense> Expense { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? toFind { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SelectedCategory { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? userID { get; set; }

        public SelectList CategoryOptions { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IQueryable<Expense> query = _context.Expense.Include(e => e.User); // Ensure User is included

            // Check if there's any search term
            if (!string.IsNullOrEmpty(toFind))
            {
                query = query.Where(x => x.Description.ToLower().Contains(toFind.ToLower()) ||
                                         x.Product.ToLower().Contains(toFind.ToLower()) ||
                                         x.Category.ToLower().Contains(toFind.ToLower()) ||
                                         x.Shop.ToLower().Contains(toFind.ToLower()) ||
                                         x.Mall.ToLower().Contains(toFind.ToLower()) ||
                                         x.Contact.ToLower().Contains(toFind.ToLower()) ||
                                         x.User.Email.ToLower().Contains(toFind.ToLower())); // Ensure to search in User.Email
            }

            // Apply category filter
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                query = query.Where(x => x.Category == SelectedCategory);
            }

            // Apply user ID filter
            if (!string.IsNullOrEmpty(userID))
            {
                query = query.Where(x => x.UserID == userID);
            }

            // Fetch the results
            Expense = await query.ToListAsync();

            // Fetch categories for the dropdown
            var categories = await _context.Expense
                .Select(e => e.Category)
                .Where(c => !string.IsNullOrEmpty(c))
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            CategoryOptions = new SelectList(categories);
        }
    }
}
