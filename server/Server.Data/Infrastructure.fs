﻿module Infrastructure

open System

// adhere to Richardson Maturity Model level3

[<CLIMutable>]
type Link = {
    Rel: string
    Uri: string
}

let Link rel uri = {Rel = rel; Uri = uri}

[<CLIMutable>]
type AnalysisRequest = {
    Term: string
}

let AnalysisRequest term = {Term = term}

[<CLIMutable>]
type TaskInfoResponse = {
    Id: Guid
    Data: Link
}

let TaskInfoResponse id data = {Id = id; Data = data}

type AnalysisTask = {
    Id: Guid
    Request: AnalysisRequest
}

let AnalysisTask id request = {Id = id; Request = request}

//

let CreateTask request = AnalysisTask (Guid.NewGuid()) request

let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "TODO" ("/analysis/"+task.Id.ToString())

