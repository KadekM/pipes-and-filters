namespace Server.WebHost
open DomainModel
open Infrastructure

open System.Web.Http
open System.Collections.Concurrent
open FSharp.Reactive
open System.Reactive
open System

//todo generic envelope with timestamp

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        let tasks = ConcurrentDictionary<Guid, Analysis.Task>()

        // todo MOVE away!
        let ProcessTask (task:Analysis.Task) =
            tasks.TryAdd(task.Id, task) |> ignore
           (* let work = async {
                let random = System.Random()
                do! Async.Sleep 2000
                let poppedTask = tasks.
                return! ()
            }*)
            ()


        let tasksSubject = new Subjects.Subject<Analysis.Task>()
        tasksSubject.Subscribe ProcessTask |> ignore

        let agent = new Agent<Analysis.Task>(fun inbox ->
            let rec loop() = 
                async {
                    let! task = inbox.Receive()
                    tasksSubject.OnNext task
                    return! loop() }
            loop())
        do agent.Start()

        Infrastructure.Configure 
            tasks
            (Observer.Create agent.Post)
            GlobalConfiguration.Configuration

        GlobalConfiguration.Configuration.EnsureInitialized();
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
