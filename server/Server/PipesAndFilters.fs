[<AutoOpen>]
module PipesAndFilters

type Agent<'a> = MailboxProcessor<'a>

open System.Collections
open System.Collections.Concurrent
open System.Reactive.Subjects
open System

type Filter<'a, 'b>(filter: 'a -> 'b option) =

     let subject = new Subject<'b>()

     let agent = new Agent<'a>(fun inbox ->
       let rec loop() = 
         async {
          let! msg = inbox.Receive()
          match filter msg with
          | Some x -> subject.OnNext(x) 
          | None -> ()
          return! loop()
         }
       loop()
     )
     do agent.Start()

     interface IObservable<'b> with
         member x.Subscribe(observer: IObserver<'b>): IDisposable = subject.Subscribe observer

     interface IDisposable with
         member x.Dispose() = 
             subject.Dispose()

     with member x.Send(input) = agent.Post input
    
