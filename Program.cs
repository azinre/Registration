using Microsoft.EntityFrameworkCore;
using Lab4.DataAccess;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

string dbConnStr = builder.Configuration.GetConnectionString("StudentRecord");
builder.Services.AddDbContext<StudentRecordContext>(options => options.UseSqlServer(dbConnStr));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
