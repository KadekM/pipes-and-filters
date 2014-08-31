namespace Server.WebHost
open DomainModel
open Infrastructure

open System.Web.Http
open System.Collections.Concurrent
open FSharp.Reactive
open System.Reactive
open System

// todo generic envelope with timestamp
// todo remove concurrent dictionary?

type Global() =
    inherit System.Web.HttpApplication() 

    member this.Application_Start() =
        let tasks = ConcurrentDictionary<Guid, Analysis.Task>()

        // todo MOVE away!
        let ProcessTask (task: Analysis.Task) =
            let x = async {
                tasks.TryAdd(task.Id, task) |> ignore
                let random = System.Random()

                let updateWorkflow (t: Analysis.Task) = async {
                    let data = Analysis.Data (random.Next(1,100)) (DateTimeOffset.Now.AddDays(float(random.Next(-10,10))))
                    let newTask = Analysis.AppendData [| data |] t
                    tasks.TryUpdate(t.Id, newTask, t) |> ignore
                   // tasks.Item(t.Id) = newTask |> ignore
                }

                let work t = async {
                    do! updateWorkflow t
                    do! Async.Sleep 5000
                }

                // todo CLEAN
                [1..10] |> List.map (fun _ -> Async.RunSynchronously ( work (tasks.Item(task.Id)) )) |> ignore
                ()
            }
            Async.RunSynchronously x



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
