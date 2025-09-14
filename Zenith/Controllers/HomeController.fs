namespace Zenith.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Zenith.Models

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    // Página inicial (Index.cshtml)
    member this.Index () =
        this.View()

    // Página Home principal (home.cshtml)
    member this.Home () =
        this.View()
        
    // Login (GET)
    member this.Login () =
        this.View()
    
    // Login (POST) → validación de usuario fijo
    [<HttpPost>]
    member this.Login(email: string, password: string) : IActionResult =
        if email = "admin@zenith.com" && password = "Zenith123" then
            this.RedirectToAction("Home") :> IActionResult
        else
            this.ViewData.["Error"] <- "Correo o contraseña incorrectos"
            this.View() :> IActionResult
        
    // Registro.cshtml
    member this.Registro () =
        this.View()

    // Rutinas.cshtml
    member this.Rutinas () =
        this.View()

    // Nutricion.cshtml
    member this.Nutricion () =
        this.View()

    // Progreso.cshtml
    member this.Progreso () =
        this.View()

    // Informacion.cshtml
    member this.Informacion () =
        this.View()

        // Comunidad.cshtml
    member this.Comunidad () =
         this.View()

    // Dataset.cshtml
    member this.Dataset () =
        this.View()

    // Nosotros.cshtml
    member this.Nosotros () =
        this.View()

    // Privacy.cshtml
    member this.Privacy () =
        this.View()

    // Error handler
    [<ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)>]
    member this.Error () =
        let reqId =
            if isNull System.Diagnostics.Activity.Current then
                this.HttpContext.TraceIdentifier
            else
                System.Diagnostics.Activity.Current.Id
        this.View({ RequestId = reqId })
