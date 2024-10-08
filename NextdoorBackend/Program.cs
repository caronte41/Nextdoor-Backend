using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using NextDoorBackend.Business.Account;
using NextDoorBackend.Business.Employee;
using NextDoorBackend.Business.Event;
using NextDoorBackend.Business.Favorite;
using NextDoorBackend.Business.FriendshipConnection;
using NextDoorBackend.Business.GoogleMaps;
using NextDoorBackend.Business.MasterData;
using NextDoorBackend.Business.Notification;
using NextDoorBackend.Business.Post;
using NextDoorBackend.Business.Profile;
using NextDoorBackend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NextDoorBackend.Business.SystemJob;  // Adjust the namespace based on your project structure

var builder = WebApplication.CreateBuilder(args);
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])),
        ClockSkew = TimeSpan.Zero // Optional: to prevent token expiration issues due to time drift
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:3000", "https://localhost:3000") // Allow frontend URLs
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials(); // If you are using authentication cookies or tokens
    });
});

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
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAccountInteractions, AccountInteractions>();
builder.Services.AddScoped<IProfileInteractions, ProfileInteractions>();
builder.Services.AddScoped<IFavoritesInteractions, FavoritesInteractions>();
builder.Services.AddScoped<IPostsInteractions, PostsInteractions>();
builder.Services.AddScoped<IFriendshipConnectionInteractions, FriendshipConnectionInteractions>();
builder.Services.AddScoped<INotificaitonInteractions, NotificaitonInteractions>();
builder.Services.AddScoped<IEventInteractions, EventInteractions>();


builder.Services.AddQuartz(q =>
{
    // Use a scoped job factory
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Register the job and trigger
    var jobKey = new JobKey("UpdateEventStatusJob");

    q.AddJob<UpdateEventStatusJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("UpdateEventStatusTrigger")
        .WithCronSchedule("0 0 0 * * ?")); // Cron expression for daily execution at midnight
});

// Add Quartz hosted service to run the scheduler
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.AddAuthorization();

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

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Ensure API controllers are mapped
app.MapControllers();
app.MapRazorPages(); // Map Razor Pages if you have any

app.Run();
