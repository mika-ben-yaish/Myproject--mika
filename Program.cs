using MyProject.Components;
using MyProject.Data;
using MyProject.Services;

namespace MyProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // רישום MyDBService כסינגלטון
            builder.Services.AddSingleton<MainDBService>();

            builder.Services.AddScoped<UserService>();


            // הוספת Razor Components במצב InteractiveServer
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            // הוספת UseRouting לפני ה-AntiForgery
            app.UseRouting();

            // הוספת ה-AntiForgery (לאחר ה-Routing, ולפני מיפוי הנתיבים)
            app.UseAntiforgery();

            // מיפוי רכיבי Razor במצב InteractiveServer
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}