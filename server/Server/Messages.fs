module Messages

open System

module Requests =
    [<CLIMutable>]
    type AnalysisRequest = {
        Term: string
    }

    let EmptyAnalysisRequest = {Term = ""}

    let AnalysisRequest term = {Term = term}

module Responses =
    type Link = {
        Rel: string
        Uri: string
    }

    let Link rel uri = {Rel = rel; Uri = uri}

    type TaskInfoResponse = {
        Id: Guid
        Data: Link
    }

    let TaskInfoResponse id data = {Id = id; Data = data}

module Analysis = 
    open Requests
    open Responses

    type Data = {
        Total: int
        Date: DateTimeOffset
    }

    let Data total date = {Total = total; Date = date}

    type Task = {
        Id: Guid
        Request: AnalysisRequest
        Data: Data array
    }

    let Empty = {Id = Guid.Empty; Request = EmptyAnalysisRequest; Data=[||]}

    let Task id request = {Id = id; Request = request; Data = [||]}

    let CreateTask request = Task (Guid.NewGuid()) request

    let AppendData data (task:Task) = {task with Data = task.Data |> Array.append data}

    let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "/analysis/" (task.Id.ToString())