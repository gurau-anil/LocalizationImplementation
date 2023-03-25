using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

List<CultureInfo> supportedCultures = new List<CultureInfo> {
                    new CultureInfo("en"),
                    new CultureInfo("ne")
                };
// Add services to the container.

//Localization Support

//Adding Localization service
//ResourcesPath sets the Directory where Resource Files are located for localization
builder.Services.AddLocalization(option =>
{
    option.ResourcesPath = "Resources";
});

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    //setting default culture
    opt.DefaultRequestCulture = new RequestCulture("en");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
});


//AddViewLocalization() Adds MVC view localization services to the application.
//AddDataAnnotationsLocalization() Adds MVC data annotations localization to the application.
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// sets culture information for requests based on information provided by the client.
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
