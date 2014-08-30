open System

[<CLIMutable>]
type Link = {
    rel: string
    uri: string
}

let Link rel uri = {rel = rel; uri = uri}

[<CLIMutable>]
type AnalysisRequest = {
    url: string
}

let AnalysisRequest url = {url = url}

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


//

let request = AnalysisRequest "www.gogole.sk"
let task=  CreateTask request
let x = AsTaskInfoResponse task

