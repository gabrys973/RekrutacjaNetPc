using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Rekrutacja.Api.Services;
using Rekrutacja.Application.Requests.Contacts;
using Rekrutacja.Application.Validators.Contacts;
using Rekrutacja.Infrastructure.DataAccess;
using Rekrutacja.Infrastructure.Repositories;
using Rekrutacja.Infrastructure.Repositories.Contacts;
using System.Globalization;

namespace Rekrutacja.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var defaultConnestionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddControllers();

        // ustawiam na sta³e zwrot komunikatów z walidacji na angielski
        builder.Services.AddFluentValidation(options =>
            {
                options.ValidatorOptions.LanguageManager.Culture = new CultureInfo("en-US", false);
                options.AutomaticValidationEnabled = true;
            });
        builder.Services.AddScoped<IValidator<ContactRequest>, ContactRequestValidator>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        builder.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication("Bearer", options =>
            {
                options.Authority = "https://localhost:5443";
                options.ApiName = "RekrutacjaAPI";
            });

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(defaultConnestionString));

        // zarejestrowanie generycznego repozytorium
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IContactRepository, ContactRepository>();

        builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

        var app = builder.Build();

        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}