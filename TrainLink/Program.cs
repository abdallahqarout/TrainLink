using DAL;
using DAL.Services;
using Microsoft.EntityFrameworkCore;
namespace TrainLink
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Database
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnection")));

            //Service 

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUniversityService, UniversityService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<ICompanySupervisorService, CompanySupervisorService>();
            builder.Services.AddScoped<ITrainingService, TrainingService>();
            builder.Services.AddScoped<IWeeklyReportService, WeeklyReportService>();
            builder.Services.AddScoped<IFinalReportService, FinalReportService>();
            builder.Services.AddScoped<IReportReviewService, ReportReviewService>();
            builder.Services.AddScoped<IFinalReportReferenceService, FinalReportReferenceService>();
            builder.Services.AddScoped<IFinalReportAppendixService, FinalReportAppendixService>();
            builder.Services.AddScoped<IFinalReportTaskService, FinalReportTaskService>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Report}/{action=CreateWeekly}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
