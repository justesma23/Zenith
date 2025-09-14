namespace Zenith.Controllers

open System
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Zenith.Models
open System.Collections.Generic
open Microsoft.AspNetCore.Http

type HomeController (logger : ILogger<HomeController>) =
    inherit Controller()

    // Lista estática para simular almacenamiento de progreso
    static let progresos = new List<string>()

    // Página inicial (Index.cshtml)
    member this.Index () =
        this.View()

    // Home principal (requiere sesión)
    member this.Home () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            this.View()

    // Login (GET)
    member this.Login () =
        this.View()

    // Login (POST)
    [<HttpPost>]
    member this.Login(email: string, password: string) : IActionResult =
        if email = "admin@zenith.com" && password = "Zenith123" then
            this.HttpContext.Session.SetString("Usuario", email)
            this.RedirectToAction("Home") :> IActionResult
        else
            this.ViewData.["Error"] <- "Correo o contraseña incorrectos"
            this.View() :> IActionResult

    // Logout
    member this.Logout () =
        this.HttpContext.Session.Clear()
        this.RedirectToAction("Index") :> IActionResult

    // Registro.cshtml
    member this.Registro () =
        this.View()

    // Rutinas.cshtml (requiere sesión)
    member this.Rutinas () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            this.View()

    // Nutricion.cshtml (requiere sesión)
    member this.Nutricion () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            this.View()

    // Progreso.cshtml (GET)
    member this.Progreso () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            this.View(progresos)

    // Progreso.cshtml (POST)
    [<HttpPost>]
    member this.Progreso(peso: float, fuerza: float) : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            let registro = sprintf "📅 %s - Peso: %.1f kg | Bench Press: %.1f kg" (DateTime.Now.ToShortDateString()) peso fuerza
            progresos.Add(registro)
            this.ViewData.["Mensaje"] <- "✅ Progreso registrado correctamente."
            this.View(progresos) :> IActionResult

    // Informacion.cshtml
    member this.Informacion () =
        this.View()

    // Comunidad.cshtml (requiere sesión)
    member this.Comunidad () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
            this.View()

    // Dataset.cshtml (requiere sesión)
    member this.Dataset () : IActionResult =
        if String.IsNullOrEmpty(this.HttpContext.Session.GetString("Usuario")) then
            this.RedirectToAction("Login") :> IActionResult
        else
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
