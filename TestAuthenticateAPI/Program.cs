using TestAuthenticateAPI.Data;
using ElCamino.AspNetCore.Identity.AzureTable;
using IdentityUser = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityUser;
using IdentityRole = ElCamino.AspNetCore.Identity.AzureTable.Model.IdentityRole;
using ElCamino.AspNetCore.Identity.AzureTable.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TestAuthenticateAPI.Models;
using Shared;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddIdentity<APIUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;
} )
    .AddAzureTableStores<ApplicationDbContext>(new Func<IdentityConfiguration>(() =>
    {
        IdentityConfiguration idconfig = new IdentityConfiguration();
        idconfig.TablePrefix = builder.Configuration.GetSection("IdentityAzureTable:IdentityConfiguration:TablePrefix").Value;
        idconfig.StorageConnectionString = builder.Configuration.GetSection("IdentityAzureTable:IdentityConfiguration:StorageConnectionString").Value;
        idconfig.IndexTableName = builder.Configuration.GetSection("IdentityAzureTable:IdentityConfiguration:IndexTableName").Value; // default: AspNetIndex
        idconfig.RoleTableName = builder.Configuration.GetSection("IdentityAzureTable:IdentityConfiguration:RoleTableName").Value;   // default: AspNetRoles
        idconfig.UserTableName = builder.Configuration.GetSection("IdentityAzureTable:IdentityConfiguration:UserTableName").Value;   // default: AspNetUsers
        return idconfig;
    })).CreateAzureTablesIfNotExists<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(options =>
    {
        ///TODO: If you plan to use both cookie and JWT auth on this API, you can use this attribute
        ///[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = config["JwtIssuer"],
                        ValidAudience = config["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtKey"])),
                        ClockSkew = TimeSpan.Zero ,// remove delay of token when expire
                        ValidateLifetime = true ,
                        ValidateIssuer = true,
                        ValidateAudience = true
                    };
                })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = builder.Configuration.GetValue<string>("GoogleClient");
                    googleOptions.ClientSecret = builder.Configuration.GetValue<string>("GoogleSecret");
                    googleOptions.Scope.Add("profile");
                    googleOptions.SaveTokens = true;
                    googleOptions.SignInScheme = IdentityConstants.ExternalScheme;
                    googleOptions.Events.OnRedirectToAuthorizationEndpoint = context =>
                    {
                        context.Response.Redirect(context.RedirectUri + "&prompt=consent");
                        return Task.CompletedTask;
                    };

                });

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
