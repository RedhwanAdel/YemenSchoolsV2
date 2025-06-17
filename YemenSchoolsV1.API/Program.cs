using FinalProject.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Globalization;
using YemenSchoolsV1.Application;
using YemenSchoolsV1.Application.MiddleWare;
using YemenSchoolsV1.Persistence;
using YemenSchoolsV1.Persistence.Data;
using YemenSchoolsV1.Persistence.Identity;

var builder = WebApplication.CreateBuilder(args);

//Connection SQL
builder.Services.AddDbContext<YemenShoolsDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("dbcontext"));
});

builder.Services.AddConfigureServices()
.AddConfigureApplicationServices()
.AddConfigurePersistenceServices()
.AddIdentityServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = ": Bearer {token}"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement {
	{
		new OpenApiSecurityScheme {
			Reference = new OpenApiReference {
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			}
		},
		new string[] {}
	}});
});




#region Localization
//builder.Services.AddControllersWithViews();

builder.Services.AddLocalization(opt =>
{
	opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
	List<CultureInfo> supportedCultures = new List<CultureInfo>
	{
			new CultureInfo("en-US"),
			new CultureInfo("ar-YE")
	};

	options.DefaultRequestCulture = new RequestCulture("en-US");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

#endregion
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll",
		policy => policy.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader());
});

var app = builder.Build();
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<YemenShoolsDbContext>();

	await context.Database.MigrateAsync();
	//await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
	await DataSeeder.SeedAsync(context);
}
catch (Exception ex)
{
	var logger = services.GetRequiredService<ILogger<Program>>();
	logger.LogError(ex, "An error occurred during migration");
}
app.Run();
