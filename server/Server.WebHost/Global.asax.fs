namespace Server.WebHost
open DomainModel
open Infrastructure

open System.Web.Http
open System.Collections.Concurrent
open FSharp.Reactive
open System.Reactive

//todo generic envelope with timestamp

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        let tasks = ConcurrentBag<Analysis.Task>()
        let maxJobs = 5

        let tasksSubject = new Subjects.Subject<Analysis.Task>()
        tasksSubject.Subscribe tasks.Add |> ignore

        let agent = new Agent<Analysis.Task>(fun inbox ->
            let rec loop() = 
                async {
                    let! task = inbox.Receive()
                    tasksSubject.OnNext task
                  //  let handle = Handle maxJobs tasks
                  //todo <--- --->
                    return! loop() }
            loop())
        do agent.Start()

        Infrastructure.Configure 
            tasks
            (Observer.Create agent.Post)
            GlobalConfiguration.Configuration

        GlobalConfiguration.Configuration.EnsureInitialized();
        //GlobalConfiguration.Configure(Action<_> Global.RegisterWebApi)
