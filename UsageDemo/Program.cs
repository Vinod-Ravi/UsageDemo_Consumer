using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using UsageDemo.DataAcessLayer;
using UsageDemo.Services.Class;
using UsageDemo.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsageDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("UsageDevPostgresConnectionString")));

builder.Services.AddCors((Setup) =>
{
    Setup.AddPolicy("default", (options) =>
    {
        options.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});//this is used for allowing request from any service,method and any header

builder.Services.AddSingleton<IHostedService, ApacheKafkaConsumerService>();
builder.Services.AddSingleton<IRedisService, RedisService>();
builder.Services.AddSingleton<IClickHoueDataManager, ClickHouseDataManager>();
builder.Services.AddSingleton<IPostgresDataManager, PostgresDataManager>();

var multiplexer = ConnectionMultiplexer.Connect("127.0.0.1:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

var consumerConfiguration = new ConsumerConfig();
builder.Configuration.Bind("consumerConfiguration", consumerConfiguration);
builder.Services.AddSingleton<ConsumerConfig>(consumerConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
