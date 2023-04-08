using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PaginationService.App.Adapters;
using PaginationService.App.Adapters.DataContracts;
using PaginationService.App.Application;
using PaginationService.App.Application.DataContracts;
using PaginationService.Domain.DataContracts;
using PaginationService.Domain.DataContracts.Cache;
using PaginationService.Infra.EF;
using PaginationService.Infra.Repository;
using PaginationService.Infra.Repository.Cache;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext configuration
builder.Services.AddDbContext<PaginationContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Redis configuration
var redisConnection = ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton(redisConnection);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = new ConfigurationOptions
    {
        EndPoints = { redisConnection.GetEndPoints()[0] },
        AbortOnConnectFail = false
    };
});

//Adapter
builder.Services.AddScoped<ICustomerAdapter, CustomerAdapter>();

//Application
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<IOrderApplication, OrderApplication>();
builder.Services.AddScoped<IOrderDetailApplication, OrderDetailApplication>();

//Repository
builder.Services.AddScoped(typeof(ICacheService<>), typeof(RedisCacheService<>));
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
