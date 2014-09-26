module Dsl

module Operators = 
    open System
    
    let pipe (``begin`` : IObservable<_>) (sink : ISinkable<_>) = ``begin``.Subscribe(sink.Send)
    let pipeAllTo (``begin`` : IObservable<_>) (sinks : ISinkable<_> seq) = 
        sinks |> Seq.map (fun x -> ``begin``.Subscribe(x.Send))
    let pipeAllFrom (begins : IObservable<_> seq) (sink : ISinkable<_>) = 
        begins |> Seq.map (fun x -> x.Subscribe(sink.Send))
    let (|~) ``begin`` sink = pipe ``begin`` sink
    let (|~<) ``begin`` sinks = pipeAllTo ``begin`` sinks
    let (|~>) begins sink = pipeAllFrom begins sink
