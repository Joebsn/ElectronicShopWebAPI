using ElectronicShop.Interfaces;
using ElectronicShop.Pagination;
using ElectronicShop.Repository;
using ElectronicShop.ShopModels;
using ElectronicShop.Controllers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShopDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ElectronicShopConnection")));

//builder.Services.AddScoped<IAdminShop, ShopRepository>();
builder.Services.AddSingleton<IAdminShop, ShopRepository>();

var ShopOptionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
ShopOptionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("ElectronicShopConnection"));
builder.Services.AddSingleton(new ShopDbContext(ShopOptionsBuilder.Options));

builder.Services.AddSingleton<IUserShop, UserRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IUriService>(o =>
{
    var accessor = o.GetRequiredService<IHttpContextAccessor>();
    var request = accessor?.HttpContext?.Request;
    var uri = string.Concat(request?.Scheme, "://", request?.Host.ToUriComponent());
    return new UriService(uri);
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
