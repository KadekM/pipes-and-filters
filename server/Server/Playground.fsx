open System
#load "Messages.fs"
#load "DomainModel.fs"

open DomainModel



let x = async {
    do! Async.Sleep(2000)
    printfn "IN"
}

printfn "A"
Async.Start(x)
printfn "B"