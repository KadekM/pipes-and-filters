namespace Server.WebHost
open Server.WebHost.Controllers
open Infrastructure

open System
open System.Net.Http
open System.Web.Http
open System.Net.Http.Headers

open System.Web.Http.Dispatcher
open System.Web.Http.Controllers

//todo move to Server data or somewhere upper
//todo generic envelope with timestamp

type Agent<'T> = Microsoft.FSharp.Control.MailboxProcessor<'T>

type CompositionRoot() = 
    let maxJobs = 5
    let tasks = System.Collections.Concurrent.ConcurrentBag<AnalysisTask>()

    let agent = new Agent<AnalysisTask>(fun inbox ->
        let rec loop() = 
            async {
                let! cmd = inbox.Receive()
              //  let handle = Handle maxJobs tasks

                return! loop()}
        loop())
    do agent.Start()

    interface IHttpControllerActivator with
        member x.Create(request, controllerDescriptor, controllerType) = 
            if (controllerType = typeof<AnalysisController>) then
                let c = new AnalysisController()
                let sub = c.Subscribe agent.Post
                request.RegisterForDispose sub
                c :> IHttpController
            else
                raise <| ArgumentException(sprintf "Unkown controller type requested: %O" controllerType, "controllerType")
        
        


type HttpRoute = {
    controller : string
    id : RouteParameter }

type Global() =
    inherit System.Web.HttpApplication() 

    static member RegisterWebApi(config: HttpConfiguration) =
        // Configure routing
        config.MapHttpAttributeRoutes()
        config.Routes.MapHttpRoute(
            "DefaultApi", // Route name
            "api/{controller}/{id}", // URL with parameters
            { controller = "{controller}"; id = RouteParameter.Optional } // Parameter defaults
        ) |> ignore

        // Configure serialization
        #if DEBUG
        config.Formatters.JsonFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue("text/html") );
        #endif

        config.Formatters.XmlFormatter.UseXmlSerializer <- false
        config.Formatters.JsonFormatter.UseDataContractJsonSerializer <- true
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()

        // Additional Web API settings

    member x.Application_Start() =
        GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
