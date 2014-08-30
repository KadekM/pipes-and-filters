namespace Server.WebHost.Controllers
open System.Web.Http
open Infrastructure

type AnalysisController() =
    inherit ApiController()

    member this.Post(r : AnalysisRequest) =
        let task = CreateTask r
        AsTaskInfoResponse task
     
    
