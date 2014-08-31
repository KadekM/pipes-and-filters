namespace Server.WebHost
open DomainModel
open Infrastructure

open System.Web.Http
open System.Collections.Concurrent

//todo generic envelope with timestamp

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        Infrastructure.Configure (ConcurrentBag<AnalysisTask>()) GlobalConfiguration.Configuration
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
