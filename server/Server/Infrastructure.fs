module Infrastructure

open DomainModel
open FSharp.Reactive
open Server.WebHost.Data.Controllers
open System
open System.Collections
open System.Collections.Concurrent
open System.Net.Http
open System.Net.Http.Headers
open System.Reactive
open System.Web.Http
open System.Web.Http.Controllers
open System.Web.Http.Dispatcher

type Agent<'T> = Microsoft.FSharp.Control.MailboxProcessor<'T>

// todo: AnalysisTask seq to domain object
type CompositionRoot(tasks : ConcurrentDictionary<Guid, Analysis.Task>, tasksRequestObserver) = 
    interface IHttpControllerActivator with
        member this.Create(request, controllerDescriptor, controllerType) = 
            if (controllerType = typeof<AnalysisController>) then 
                let c = new AnalysisController(tasks)
                c
                |> Observable.subscribeObserver tasksRequestObserver
                |> request.RegisterForDispose
                c :> IHttpController
            else 
                raise 
                <| ArgumentException(sprintf "Unkown controller type requested: %O" controllerType, "controllerType")

type HttpRoute = 
    { controller : string
      id : RouteParameter }

let ConfigureRoutes(config : HttpConfiguration) = 
    config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", 
                               { controller = "{controller}"
                                 id = RouteParameter.Optional })
    |> ignore

let ConfigureServices tasks tasksRequestObserver (config : HttpConfiguration) = 
    config.Services.Replace(typeof<IHttpControllerActivator>, CompositionRoot(tasks, tasksRequestObserver))

let ConfigureFormatters(config : HttpConfiguration) =
#if DEBUG 
    config.Formatters.JsonFormatter.SupportedMediaTypes.Add(MediaTypeHeaderValue("text/html"))
    
#endif
    config.Formatters.XmlFormatter.UseXmlSerializer <- false
    config.Formatters.JsonFormatter.UseDataContractJsonSerializer <- true
    config.Formatters.JsonFormatter.SerializerSettings.ContractResolver <- Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver
                                                                               ()

let Configure tasks tasksRequestObserver (config : HttpConfiguration) = 
    ConfigureRoutes config
    ConfigureServices tasks tasksRequestObserver config
    ConfigureFormatters config
    config.MapHttpAttributeRoutes()
/////////////////
