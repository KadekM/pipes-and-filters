module DomainModel
open Messages

open System

type AnalysisTask = {
    Id: Guid
    Request: AnalysisRequest
}

let AnalysisTask id request = {Id = id; Request = request}

//

let CreateTask request = AnalysisTask (Guid.NewGuid()) request

let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "TODO" ("/analysis/"+task.Id.ToString())