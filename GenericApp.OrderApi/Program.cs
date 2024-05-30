using System.IdentityModel.Tokens.Jwt;
using GenericApp.DataMan;
using GenericApp.Domain.Repositories;
using GenericApp.DataMan.Repositories.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers(

options =>
    {
        var policy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim("email")
            .Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    }
);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://advinculaorderapi.azurewebsites.net",
                                              "http://localhost:4200");
                      });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c =>
    {
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                Implicit = new OpenApiOAuthFlow()
                {
                    AuthorizationUrl = new Uri("https://login.microsoftonline.com/33444707-653e-4367-bb8b-1ed2685b6b7c/oauth2/v2.0/authorize"),
                    TokenUrl = new Uri("https://login.microsoftonline.com/33444707-653e-4367-bb8b-1ed2685b6b7c/oauth2/v2.0/token"),
                    Scopes = new Dictionary<string, string> {
                        {
                            "api://287789c8-78d4-43e6-8b76-8f2d2f206961/Order.Management",
                            "Orders management"
                        }
                    }
                }
            }
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                },
                Scheme = "oauth2",
                Name = "oauth2",
                In = ParameterLocation.Header
        },
        new List < string > ()
        }
});
    }
);
builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        // The claim in the Jwt token where App roles are available.
        options.TokenValidationParameters.RoleClaimType = "roles";
    });
IdentityModelEventSource.ShowPII = true; //Para mostrar informaciÃ³n contenida en el jwt
builder.Services.AddDbContext <GenericAppDBContext> (options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddTransient < IUnitOfWork, UnitOfWork > ();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderApi v1");
        c.OAuthClientId(app.Configuration["AzureAd:ClientId"]);  
        c.OAuthClientSecret("Client Secret Key");  
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();        
    });    
    app.UseSwagger();
} else {
        app.Use((context, next) => { context.Request.Scheme = "https"; return next(); });
        app.UsePathBase("/orderapi");
        app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "OrderApi v1");
        c.OAuthClientId(app.Configuration["AzureAd:ClientId"]);  
        c.OAuthClientSecret("Client Secret Key");  
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();        
    });    
    app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    var scheme = "https";
                    swagger.Servers = new List<OpenApiServer>() {new OpenApiServer() {Url = $"{scheme}://advinculaorderapi.azurewebsites.net{httpReq.PathBase}"}};
                });
            }); 
    app.UseHttpsRedirection();
    app.UseCors(MyAllowSpecificOrigins);
    app.UseRouting();   
}



app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();