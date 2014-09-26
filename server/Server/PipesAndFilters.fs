[<AutoOpen>]
module PipesAndFilters

type Agent<'a> = MailboxProcessor<'a>

open System
open System.Collections.Concurrent
open System.Reactive.Subjects

type ISinkable<'a> = 
    abstract Send : 'a -> unit

type Filter<'a, 'b>(filter : 'a -> 'b option) = 
    let subject = new Subject<'b>()
    
    let agent = 
        new Agent<'a>(fun inbox -> 
        let rec loop() = 
            async { 
                let! msg = inbox.Receive()
                match filter msg with
                | Some x -> subject.OnNext(x)
                | None -> ()
                return! loop()
            }
        loop())
    
    do agent.Start()
    
    interface IObservable<'b> with
        member x.Subscribe(observer : IObserver<'b>) : IDisposable = subject.Subscribe observer
    
    interface IDisposable with
        member x.Dispose() = subject.Dispose()
    
    interface ISinkable<'a> with
        member x.Send(input) = agent.Post input

//todo: remove dependancy on ConcurrentDictionary
type Aggregator<'TValue, 'TKey>(bag : ConcurrentDictionary<'TKey, 'TValue>, toIdtransformer: 'TValue -> 'TKey, mergeStrategy: 'TValue -> 'TValue -> 'TValue) = 
    let subject = new Subject<'TValue>()
    
    let agent = 
        new Agent<'TValue>(fun inbox -> 
        let rec loop() = 
            async { 
                let! msg = inbox.Receive()
                let id = toIdtransformer (msg)
                match bag.TryGetValue(id) with
                | (true, current) -> 
                    let merged = mergeStrategy msg current
                    match bag.TryUpdate(id, merged, current) with
                    | true -> subject.OnNext(merged)
                    | false -> ()
                | _ -> match bag.TryAdd(id, msg) with
                    | true -> subject.OnNext(msg)
                    | false -> ()
                return! loop()
            }
        loop())
    
    do agent.Start()
    
    interface IObservable<'TValue> with
        member x.Subscribe(observer : IObserver<'TValue>) : IDisposable = subject.Subscribe observer
    
    interface IDisposable with
        member x.Dispose() = subject.Dispose()
    
    interface ISinkable<'TValue> with
        member x.Send(input) = agent.Post input
