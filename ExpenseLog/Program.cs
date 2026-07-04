using ExpenseLog.Services;
using ExpenseLog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ExpenseLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseLogContext") ?? throw new InvalidOperationException("Connection string 'ExpenseLogContext' not found."))
           .EnableSensitiveDataLogging()  // This enables detailed SQL logs
           .LogTo(Console.WriteLine, LogLevel.Information));  // This outputs SQL logs to the console
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ExpenseLogContext>();
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Expenses");
});

builder.Services.AddTransient<linkSender>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();

app.Run();
