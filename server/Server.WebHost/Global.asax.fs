namespace Server.WebHost

open DomainModel
open Dsl.Operators
open FSharp.Reactive
open Filters
open Infrastructure
open System
open System.Collections.Concurrent
open System.Reactive
open System.Web.Http

type Global() = 
    inherit System.Web.HttpApplication()
    member this.Application_Start() = 
        let tasks = ConcurrentDictionary<Guid, Analysis.Task>()


        let swearTermsFilter = SwearTermsFilter.Create()
        let googleFilter = GoogleFilter.Create()
        let bingFilter = BingFilter.Create()
        let googleBingAggregator = GoogleBingAggregator.Create(tasks)

        swearTermsFilter |~< [| googleFilter; bingFilter |]
        |> ignore

        [| googleFilter; bingFilter |] |~> googleBingAggregator
        |> ignore

        Infrastructure.Configure tasks (Observer.Create((swearTermsFilter :> ISinkable<_>).Send)) 
            GlobalConfiguration.Configuration
        GlobalConfiguration.Configuration.EnsureInitialized()
//GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
