using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Common;
using Service.IRepository;
using Service.Repository;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



var connectionString = builder.Configuration.GetConnectionString("SampleDB");
builder.Services.AddDbContext<DatabaseContext>(option =>
option.UseSqlServer(connectionString)
);

builder.Services.AddScoped<IItemCategoryRepository, ItemcategoryRepository>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
