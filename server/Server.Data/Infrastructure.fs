module Infrastructure
open DomainModel

open Server.WebHost.Data.Controllers

open FSharp.Reactive
open System.Reactive
open System
open System.Net.Http
open System.Web.Http
open System.Net.Http.Headers

open System.Web.Http.Dispatcher
open System.Web.Http.Controllers

type Agent<'T> = Microsoft.FSharp.Control.MailboxProcessor<'T>

type CompositionRoot(tasks: System.Collections.Concurrent.ConcurrentBag<AnalysisTask>, tasksRequestObserver) = 


    interface IHttpControllerActivator with
        member this.Create(request, controllerDescriptor, controllerType) = 
            if (controllerType = typeof<AnalysisController>) then
                let c = new AnalysisController()
                c 
                |> Observable.subscribeObserver tasksRequestObserver
                |> request.RegisterForDispose
                c :> IHttpController
            else
                raise <| ArgumentException(sprintf "Unkown controller type requested: %O" controllerType, "controllerType")
        
type HttpRoute = {controller: string; id: RouteParameter}

let ConfigureRoutes(config: HttpConfiguration) = 
    //config.MapHttpAttributeRoutes()
    config.Routes.MapHttpRoute(
        "DefaultApi", // Route name
        "api/{controller}/{id}", // URL with parameters
        {controller = "{controller}"; id = RouteParameter.Optional} // Parameter defaults
    ) |> ignore

let ConfigureServices tasks tasksRequestObserver (config: HttpConfiguration) =
     config.Services.Replace(typeof<IHttpControllerActivator>, CompositionRoot(tasks, tasksRequestObserver))

let ConfigureFormatters(config: HttpConfiguration) =
     #if DEBUG
     config.Formatters.JsonFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue("text/html") );
     #endif

     config.Formatters.XmlFormatter.UseXmlSerializer <- false
     config.Formatters.JsonFormatter.UseDataContractJsonSerializer <- true
     config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()

let Configure tasks tasksRequestObserver config =
    ConfigureRoutes config
    ConfigureServices tasks tasksRequestObserver config
    ConfigureFormatters config

/////////////////



