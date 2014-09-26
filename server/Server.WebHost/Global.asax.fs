namespace Server.WebHost
open DomainModel
open Infrastructure
open Filters

open System.Web.Http
open System.Collections.Concurrent
open FSharp.Reactive
open System.Reactive
open Dsl.Operators
open System

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        let tasks = ConcurrentDictionary<Guid, Analysis.Task>()

        let swearTermsFilter = SwearTermsFilter.Create()
        let googleFilter = GoogleFilter.Create()
        let bingFilter = BingFilter.Create()

        swearTermsFilter 
        |~< [| googleFilter; bingFilter |]
        |> ignore

        //[| googleFilter; bingFilter |] |~> 

        Infrastructure.Configure 
            tasks
            (Observer.Create( swearTermsFilter.Send ))
            GlobalConfiguration.Configuration

        GlobalConfiguration.Configuration.EnsureInitialized();
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
