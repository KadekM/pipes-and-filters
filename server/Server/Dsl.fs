module Dsl

module Operators = 
    open System
    
    let pipe (``begin`` : IObservable<_>) (sink : ISinkable<_>) = ``begin``.Subscribe(sink.Send)
    let (|~) ``begin`` sink = pipe ``begin`` sink

    let pipeAllTo (``begin`` : IObservable<_>) (sinks : ISinkable<_> seq) = 
        sinks |> Seq.map (fun x -> ``begin`` |~ x) |> Array.ofSeq
    let (|~<) ``begin`` sinks = pipeAllTo ``begin`` sinks

    let pipeAllFrom (begins : IObservable<_> seq) (sink : ISinkable<_>) = 
        begins |> Seq.map (fun x -> x |~ sink) |> Array.ofSeq
    let (|~>) begins sink = pipeAllFrom begins sink
