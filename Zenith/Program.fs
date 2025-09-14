namespace Zenith

#nowarn "20"

open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection

module Program =
    let exitCode = 0

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        builder
            .Services
            .AddControllersWithViews()
            .AddRazorRuntimeCompilation()

        builder.Services.AddRazorPages()

        // ✅ Agregar soporte para sesiones
        builder.Services.AddSession(fun options ->
            options.IdleTimeout <- TimeSpan.FromMinutes(30.0)
            options.Cookie.HttpOnly <- true
            options.Cookie.IsEssential <- true
        ) |> ignore

        let app = builder.Build()

        if not (builder.Environment.IsDevelopment()) then
            app.UseExceptionHandler("/Home/Error")
            app.UseHsts() |> ignore

        app.UseHttpsRedirection()
        app.UseStaticFiles()
        app.UseRouting()

        // ✅ Activar sesiones
        app.UseSession()

        app.UseAuthorization()

        app.MapControllerRoute(
            name = "default",
            pattern = "{controller=Home}/{action=Index}/{id?}"
        )

        app.MapRazorPages()
        app.Run()

        exitCode
