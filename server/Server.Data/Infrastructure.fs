module Infrastructure

open System

// adhere to Richardson Maturity Model level3

[<CLIMutable>]
type Link = {
    rel: string
    uri: string
}

let Link rel uri = {rel = rel; uri = uri}

[<CLIMutable>]
type AnalysisRequest = {
    term: string
}

let AnalysisRequest term = {term = term}

[<CLIMutable>]
type TaskInfoResponse = {
    guid: Guid
    data: Link
}

let TaskInfoResponse guid data = {guid = guid; data = data}

type Task = {
    request: AnalysisRequest
    guid: Guid
}

let Task request guid = {request = request; guid = guid}

//

let CreateTask request = Task request <| Guid.NewGuid()

let AsTaskInfoResponse task = TaskInfoResponse task.guid <| Link "TODO" ("/analysis/"+task.guid.ToString())

