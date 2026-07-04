using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseLog.Data;
using ExpenseLog.Models;
using Microsoft.AspNetCore.Identity;

namespace ExpenseLog.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ExpenseLog.Data.ExpenseLogContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ExpenseLog.Data.ExpenseLogContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Expense Expense { get; set; } = default!;

        // This handles POST requests for creating an expense
        public async Task<IActionResult> OnPostAsync()
        {
            // Debugging: Check if the user is authenticated and the user ID
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("User is authenticated.");
            }
            else
            {
                Console.WriteLine("User is NOT authenticated.");
            }

            var userId = _userManager.GetUserId(User);

            // Log the resolved user ID
            Console.WriteLine($"Resolved User ID: {userId}");

            // Check if userId is null
            if (userId == null)
            {
                TempData["debugMessage"] = "ERROR: User ID is null despite being logged in.";
                return RedirectToPage("/Account/Login");
            }

            // Assign the userId to the Expense.UserID
            Expense.UserID = userId;

            // Now check if the Expense.UserID is correctly assigned
            if (string.IsNullOrEmpty(Expense.UserID))
            {
                TempData["debugMessage"] = "ERROR: User ID is not assigned to the expense.";
                return Page();
            }

            try
            {
                // Save the expense to the database
                _context.Expense.Add(Expense);
                await _context.SaveChangesAsync();
                //TempData["debugMessage"] = "Expense successfully created.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Handle the exception and log the inner exception if available
                if (ex.InnerException != null)
                {
                    TempData["debugMessage"] = $"Error: {ex.Message}. Inner Exception: {ex.InnerException.Message}";
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                else
                {
                    TempData["debugMessage"] = $"Error: {ex.Message}";
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return Page();
            }
        }
    }
}
