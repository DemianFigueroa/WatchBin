using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WatchBin.Domain.Repositories;
using WatchBin.Domain.Respositories;
using WatchBin.Domain.UseCases;
using WatchBin.Infrastructure.Repositories;
using WatchBin.Infrastructure.UseCases;
using WatchBin.Mappers;
using WatchBin.Services;
using WatchBin.TokenService;
using WatchBin.Users;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder
    .Services.AddIdentity<AppUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 12;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    builder.Configuration["JWT:SigningKey"]
                        ?? throw new ArgumentNullException("JWT:SigningKey")
                )
            ),
        };
    });

builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<
    IAddMediaRequestViewModelToModelMapper,
    AddMediaRequestViewModelMapper
>();
builder.Services.AddScoped<IMediaModelToViewModelMapper, MediaModelToViewModelMapper>();
builder.Services.AddScoped<
    IGetMediaRequestViewModelToModelMapper,
    GetMediaRequestViewModelToModelMapper
>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Add CORS policy (before adding middleware)
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowLocalhost",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200") // Angular app URL
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }
    );
});

// Mappers
builder.Services.AddScoped<IMediaModelToEntityMapper, MediaModelToEntityMapper>();
builder.Services.AddScoped<IMediaEntityToModelMapper, MediaEntityToModelMapper>();

// UseCases
builder.Services.AddScoped<IAddMediaUseCase, AddMediaUseCase>();
builder.Services.AddScoped<IGetMediaUseCase, GetMediaUseCase>();
builder.Services.AddScoped<IDeleteMediaUseCase, DeleteMediaUseCase>();
builder.Services.AddScoped<IDeleteMediaRepository, DeleteMediaRepository>();

// Repositories
builder.Services.AddScoped<IAddMediaRepository, AddMediaRepository>();
builder.Services.AddScoped<IGetMediaRepository, GetMediaRepository>();

// Controllers
builder.Services.AddControllers();

// Swagger Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
        }
    );
    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});

var app = builder.Build();

// Apply CORS middleware before Authentication and Authorization
app.UseCors("AllowLocalhost");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated(); // Creates the database if it does not exist
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name");
    });
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Authentication and Authorization should come after UseCors


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
