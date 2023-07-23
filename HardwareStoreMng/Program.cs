using HardwareStoreMng.CashServices;
using HardwareStoreMng.Context;
using HardwareStoreMng.Repositories.BarCodesRepository;
using HardwareStoreMng.Repositories.CustomerRepository;
using HardwareStoreMng.Repositories.EmployeeRepository;
using HardwareStoreMng.Repositories.InvoiceItemRepository;
using HardwareStoreMng.Repositories.InvoiceRepository;
using HardwareStoreMng.Repositories.PorductRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Hardware Store Mng",
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddDbContext<HardwareStoreMngContext>(cnn => cnn.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnect")));
builder.Services.AddScoped<ProductInterface, ProductImplemintation>();
builder.Services.AddScoped<CustomerInterface, CustomerImplemintation>();
builder.Services.AddScoped<EmployeeInterface, EmployeeImplemintation>();
builder.Services.AddScoped<InvoiceInterface, InvoiceImplemintation>();
builder.Services.AddScoped<InvoiceItemInterface, InvoiceItemImplemintation>();
builder.Services.AddScoped<BarCodesInterface, BarCodesImplemintation>();
builder.Services.AddScoped<IredisCashServices, RedisCashServices>();

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
string loggerPath = configuration.GetValue<string>("LoggerPath");
//use the above line to store file path in appsetings

Serilog.Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).
                WriteTo.File(loggerPath, rollingInterval: RollingInterval.Day).
                CreateLogger();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
