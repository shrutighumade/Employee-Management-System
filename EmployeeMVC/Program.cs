var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// ✅ register EmployeeService + HttpClient
builder.Services.AddHttpClient<EmployeeMVC.Services.EmployeeService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
