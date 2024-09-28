using DotNetsTasks.Web.LIBS;
using DotNetsTasks.Web.MappingProfile;
using DotNetsTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using DotNetsTask.Data.Models;
using DotNetsTask.Repo;
using NToastNotify;
using DotNetstask.Service;
using DotNetsTask.Service.DropDown;
using DotNetsTask.Service.BookServices;
using DotNetsTask.Service.DashboardServices;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Set up configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Configure session
builder.Services.AddSession(options =>
{
	options.Cookie.IsEssential = true;
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.IdleTimeout = TimeSpan.FromMinutes(30); // Reasonable session timeout
});

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
	opt.LoginPath = "/Admin/Account/Login"; // Specify the complete path
});

// Configure NToastNotify
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
	ProgressBar = true,
	PositionClass = ToastPositions.TopRight
});

// Configure cookie policy
builder.Services.Configure<CookiePolicyOptions>(options =>
{
	options.CheckConsentNeeded = context => true;
	options.MinimumSameSitePolicy = SameSiteMode.None;
	options.ConsentCookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Configure form options
builder.Services.Configure<FormOptions>(x => x.ValueCountLimit = 1048576);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configure CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", builder =>
	{
		builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); // Review this for production use
	});
});

// Set connection string
builder.Services.AddDbContext<DotNetsTasksDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("UsersConnection")));

// Dependency Injection
InitServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CorsPolicy"); // Use CORS policy if needed
app.UseAuthentication(); // Configure Authentication
app.UseAuthorization();
app.UseSession(); // Use Session middleware

app.UseNToastNotify(); // Add NToastNotify middleware

app.UseEndpoints(endpoints =>
{
	// Route for Admin Login
	endpoints.MapAreaControllerRoute(
		name: "AdminArea",
		areaName: "Admin",
		pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

	// Redirect to Admin Dashboard by default
	endpoints.MapGet("/", async context =>
	{
		context.Response.Redirect("/Admin/Dashboard/Index");
	});
});



// Context provider setup
ContextProvider.Configure(app.Services.GetRequiredService<IHttpContextAccessor>(), app.Services.GetRequiredService<IWebHostEnvironment>());

app.Run();

void InitServices(IServiceCollection services)
{
	services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
	services.AddScoped<IUsersService, UsersService>();
	services.AddScoped<IBookService, BookService>();
	services.AddScoped<IDropdownHelper, DropdownHelper>();
	services.AddScoped<IBookIssueService, BookIssueService>();
	services.AddScoped<IDashboardCountServices, DashboardCountServices>();
}
