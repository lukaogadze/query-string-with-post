namespace QueryStringWithPostMethod.Controllers

open System
open System.Collections.Generic
open System.IO
open System.Threading.Tasks
open QueryStringWithPostMethod.Models
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open QueryStringWithPostMethod

[<ApiController>]
[<Route("[controller]")>]
type WeatherForecastController (logger : ILogger<WeatherForecastController>) =
    inherit ControllerBase()

    let summaries = [| "Freezing"; "Bracing"; "Chilly"; "Cool"; "Mild"; "Warm"; "Balmy"; "Hot"; "Sweltering"; "Scorching" |]

    [<HttpGet>]
    member __.Get() : WeatherForecast[] =
        let rng = System.Random()       
        [|
            for index in 0..4 ->
                { Date = DateTime.Now.AddDays(float index)
                  TemperatureC = rng.Next(-20,55)
                  Summary = summaries.[rng.Next(summaries.Length)] }
        |]

    // http://localhost:5000/weatherforecast/create?nickName=mdaaa
    [<HttpPost("create")>]
    member __.Create(nickName: string, person: Person): IActionResult =
        File.WriteAllText("nickName", nickName)
        File.WriteAllText("FirstName", person.FirstName)
        File.WriteAllText("LastName", person.LastName)
        __.Ok() :> IActionResult