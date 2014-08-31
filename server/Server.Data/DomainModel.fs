module DomainModel
open Messages

open System

module Analysis = 

    type Data = {
        Total: int
        Date: DateTimeOffset
    }

    type Task = {
        Id: Guid
        Request: AnalysisRequest
        Data: Data array
    }

    let Task id request = {Id = id; Request = request; Data = [||]}

    let CreateTask request = Task (Guid.NewGuid()) request

    let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "TODO" ("/analysis/"+task.Id.ToString())
