using Microsoft.EntityFrameworkCore;
using NextDoorBackend.Business.Account;
using NextDoorBackend.Business.Employee;
using NextDoorBackend.Business.Favorite;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.Data;  // Adjust the namespace based on your project structure

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Ensure this is present for API controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages(); // Add this if you have Razor Pages

// Register DbContext with dependency injection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WebApiDatabase")));

// Build the app
builder.Services.AddScoped<IEmployeeInteractions, EmployeeInteractions>();
builder.Services.AddScoped<IMasterDataInteractions, MasterDataInteractions>();
builder.Services.AddHttpClient<IGoogleMapsInteractions, GoogleMapsInteractions>();
builder.Services.AddScoped<IAccountInteractions, AccountInteractions>();
builder.Services.AddScoped<IProfileInteractions, ProfileInteractions>();
builder.Services.AddScoped<IFavoritesInteractions, FavoritesInteractions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;  // Serve the Swagger UI at the app's root
    });
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ensure API controllers are mapped
app.MapControllers();
app.MapRazorPages(); // Map Razor Pages if you have any

app.Run();
