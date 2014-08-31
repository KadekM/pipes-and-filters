open System
#load "Messages.fs"
#load "DomainModel.fs"

type Agent<'T> = Microsoft.FSharp.Control.MailboxProcessor<'T>

let agent = new Agent<int>(fun inbox -> 
    let rec loop() =
        async {
        let! msg = inbox.Receive()
        printfn "%A" msg
        do! Async.Sleep 500
        return! loop()}
    loop())
agent.Start()


agent.Post(1)
agent.Post(2)
agent.Post(3)