// adhere to Richardson Maturity Model level3
module Messages

open System

type Link = {
    Rel: string
    Uri: string
}

let Link rel uri = {Rel = rel; Uri = uri}
// to module
[<CLIMutable>]
type AnalysisRequest = {
    Term: string
}

let EmptyAnalysisRequest = {Term = ""}

let AnalysisRequest term = {Term = term}

type TaskInfoResponse = {
    Id: Guid
    Data: Link
}

let TaskInfoResponse id data = {Id = id; Data = data}

