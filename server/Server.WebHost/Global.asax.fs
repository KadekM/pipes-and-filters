namespace Server.WebHost
open Infrastructure

open System.Web.Http

//todo generic envelope with timestamp

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        Infrastructure.Configure GlobalConfiguration.Configuration
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
