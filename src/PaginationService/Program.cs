using Microsoft.EntityFrameworkCore;
using PaginationService.App.Adapters;
using PaginationService.App.Adapters.DataContracts;
using PaginationService.App.Application;
using PaginationService.App.Application.DataContracts;
using PaginationService.Domain.DataContracts;
using PaginationService.Infra.EF;
using PaginationService.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PaginationContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerAdapter, CustomerAdapter>();

builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<IOrderApplication, OrderApplication>();
builder.Services.AddScoped<IOrderDetailApplication, OrderDetailApplication>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

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
