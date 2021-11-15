using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;
//var builder = WebApplication.CreateBuilder(args);

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("AppConfig");
builder.Configuration.AddAzureAppConfiguration(options =>
                    options.Connect(connection).UseFeatureFlags(
                        featureFlagsOptions => 
                            {
                                featureFlagsOptions.CacheExpirationInterval = 
                                    TimeSpan.FromSeconds(5);
                            }
));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddFeatureManagement();
builder.Services.AddAzureAppConfiguration();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAzureAppConfiguration();    

app.Run();
