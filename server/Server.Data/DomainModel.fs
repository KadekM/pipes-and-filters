module DomainModel
open Messages

open System
open System.Collections

type AnalysisTask = {
    Id: Guid
    Request: AnalysisRequest
}

let AnalysisTask id request = {Id = id; Request = request}

//

let CreateTask request = AnalysisTask (Guid.NewGuid()) request

let AsTaskInfoResponse task = TaskInfoResponse task.Id <| Link "TODO" ("/analysis/"+task.Id.ToString())
(*
type IAnalysisTasks =
    inherit seq<AnalysisTask> 

type AnalysisTasksInMemory(analysisTasks: seq<AnalysisTask>) =
    interface IAnalysisTasks with
        member this.GetEnumerator() =
            analysisTasks.GetEnumerator()
        member this.GetEnumerator() =
            (this :> seq<AnalysisTask>).GetEnumerator() :> IEnumerable<>

let ToAnalysisTasks tasks = 
    AnalysisTasksInMemory(tasks)
    *)