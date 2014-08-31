namespace Server.WebHost
open Infrastructure

open System
open System.Net.Http
open System.Web.Http
open System.Web.Http.WebHost

open System.Web.Http.Dispatcher
open System.Web.Http.Controllers

//todo generic envelope with timestamp

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        Infrastructure.Configure GlobalConfiguration.Configuration
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
