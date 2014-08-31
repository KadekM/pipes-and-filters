open System
#load "Messages.fs"
#load "DomainModel.fs"

open DomainModel

let t = Analysis.Empty

let random = System.Random()
let data = Analysis.Data (random.Next(1,100)) (DateTimeOffset.Now.AddDays(float(random.Next(-10,10))))
let newTask = Analysis.AppendData [| data |] t
let newTask2 = Analysis.AppendData [| data |] newTask


