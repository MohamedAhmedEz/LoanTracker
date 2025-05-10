using Hangfire;
using Hangfire.PostgreSql;
using LoanTracker.Application.Interface;
using LoanTracker.Application.Service;
using LoanTracker.Infrastructure.DBContext;
using LoanTracker.Infrastructure.Repository;
using LoanTracker.Infrastructure.Sender;
using Microsoft.EntityFrameworkCore;

namespace LoanTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy
                        .AllowAnyOrigin() // You can restrict this to your frontend origin
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddHangfire(config =>
                config.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddHangfireServer();


            builder.Services.AddScoped<ILoanRepository, LoanRepository>();
            builder.Services.AddScoped<IContactRepository, ContactRepository>();
            builder.Services.AddSingleton<INotificationService, ConsoleNotificationService>();
            builder.Services.AddScoped<ContactService>();
            builder.Services.AddScoped<LoanService>();

            var app = builder.Build();
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.UseCors();
            

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<LoanService>(
                "SendLoanReminders",
                    service => service.SendUpcomingLoanRemindersAsync(1),
                        Cron.Minutely);

            app.Run();
        }
    }
}
