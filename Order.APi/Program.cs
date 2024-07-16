using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Order.APi.Hubs;
using Order.APi.Seeds;
using Order.Repository.Data;
using Order.Repository.Entities;
using Order.Repository.Repositories.CustomerRepository;
using Order.Repository.Repositories.InvoiceRepository;
using Order.Repository.Repositories.OrderRepository;
using Order.Repository.Repositories.ProductRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderConnection"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);


}).AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSignalR();


#region InterfcaesServices

builder.Services.AddTransient<IOrderRepository,OrderServices >();
builder.Services.AddTransient<IProductRepo,ProductServices >();
builder.Services.AddTransient<IInvoiceRepo,InvoiceServices >();
builder.Services.AddTransient<ICustomerRepo,Customerservice >();



#endregion


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuar"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        IssuerSigningKey =
       new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddCors(corsOptions => {
    corsOptions.AddPolicy("MyPolicy", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();


builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation    
    

    // To Enable authorization using Swagger (JWT)    
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                    },
                    new string[] {}
                    }
                });
});





var app = builder.Build();




#region Seeding
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;


try
{
    var dbContext = services.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await DefaultRoles.SeedAsync(roleManager);
    await DefaultUsers.SeedAdminUserAsync(userManager, roleManager);
    await DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
   
}
catch (Exception ex)
{
    
}
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<OrderHub>("/orderHub");


app.MapControllers();


app.Run();
