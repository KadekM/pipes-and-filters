namespace Server.WebHost.Data.Controllers

open DomainModel
open Messages
open System
open System.Collections.Concurrent
open System.Reactive.Subjects
open System.Web.Http

// todo make readonly
type AnalysisController(analysisTasks : ConcurrentDictionary<Guid, Analysis.Task>) = 
    inherit ApiController()
    let subject = new Subject<Analysis.Task>()
    
    interface IObservable<Analysis.Task> with
        member this.Subscribe observer = subject.Subscribe observer
    
    override this.Dispose disposing = 
        if disposing then subject.Dispose()
        base.Dispose disposing
    
    // todo NULL checking
    member this.Post(r : AnalysisRequest) = 
        let task = Analysis.CreateTask r
        do subject.OnNext task
        Analysis.AsTaskInfoResponse task
    
    // todo key not found checking
    member this.GetAnalysis(id : string) = analysisTasks.Item(Guid.Parse(id))
