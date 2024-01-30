using LfragmentApi.Data;
using LfragmentApi.Entities;
using LfragmentApi.RequestHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiler));
builder.Services.AddDbContext<FragmentDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDbContext<UserDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<User>().AddEntityFrameworkStores<UserDbContext>().AddDefaultTokenProviders();

var app = builder.Build();
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Configure the HTTP request pipeline.
app.MapIdentityApi<User>();
app.UseAuthorization();

app.MapControllers();

app.Run();