using elsaeedTea.data.Context;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.repository.Repositories;
using elsaeedTea.service.Services.teaImagesServices;
using elsaeedTea.service.Services.teaImagesServices.Dtos;
using elsaeedTea.service.Services.teaProductServices;
using elsaeedTea.service.Services.teaProductServices.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using elsaeedTea.service.Services.AuthenticationServices;
using elsaeedTea.service.Services.EmailServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ElsaeedTeaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true; // ??? ?? ????? ??? ???
    options.Password.RequiredLength = 6;  // ??? ???? ?????? (????? 6)
    options.Password.RequireLowercase = true; // ??? ?? ????? ??? ??? ????
    options.Password.RequireUppercase = false; // ?? ????? ??? ??? ????
    options.Password.RequireNonAlphanumeric = false; // ?? ???? ????? ????
    options.Password.RequiredUniqueChars = 1; // ??? ?? ???? ???? ??? ????? ??? ???? ?????
})
    .AddEntityFrameworkStores<ElsaeedTeaDbContext>()
    .AddDefaultTokenProviders();

// ????? JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "YourIssuer",
        ValidAudience = "YourAudience",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});





builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITeaServices, TeaServices>();
builder.Services.AddScoped<IImageServices, ImageServices>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();


builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(ImageProfile));

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

app.UseStaticFiles(); 

app.UseHttpsRedirection();

app.UseAuthentication(); // ?????? ???????
app.UseAuthorization();

app.MapControllers();

app.Run();
