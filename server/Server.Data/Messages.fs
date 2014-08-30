// adhere to Richardson Maturity Model level3
module Messages

open System

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