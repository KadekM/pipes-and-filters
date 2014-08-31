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

    let Empty = {Id = Guid.Empty; Request = EmptyAnalysisRequest; Data=[||]}

    let Task id request = {Id = id; Request = request; Data = [||]}

    let CreateTask request = Task (Guid.NewGuid()) request

    let AppendData data (task:Task) = {task with Data = task.Data |> Array.append data}

    let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "TODO" ("/analysis/"+task.Id.ToString())
