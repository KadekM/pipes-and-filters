namespace Server.WebHost.Controllers

open Messages
open Infrastructure


open System.Web.Http
open System.Reactive.Subjects
open System

type AnalysisController() =
    inherit ApiController()

    let subject = new Subject<AnalysisTask>()
    interface IObservable<AnalysisTask> with
        member this.Subscribe observer = subject.Subscribe observer

    override this.Dispose disposing =
        if disposing then subject.Dispose()
        base.Dispose disposing

    member this.Post(r : AnalysisRequest) =
        let task = CreateTask r
        do subject.OnNext task
        AsTaskInfoResponse task
     
    
