module Filters

open DomainModel
open PipesAndFilters

module SwearTermsFilter = 
    let swearWords = [| "fuck"; "shit"; "damn" |]
    
    let filter (task : Analysis.Task) = 
        if (swearWords |> Seq.exists (fun x -> task.Request.Term.Contains x)) then None
        else Some(task)
    
    let Create() = new Filter<_, _>(filter)

// random data, later use service
open Analysis
open System

let random = Random()

module GoogleFilter = 
    let filter (task : Analysis.Task) = 
        let data = Data (random.Next(1, 100)) (DateTimeOffset.Now.AddDays(float (random.Next(1, 30))))
        task
        |> AppendData [| data |]
        |> Option.Some
    
    let Create() = new Filter<_, _>(filter)

module BingFilter = 
    let filter (task : Analysis.Task) = 
        let data = Data (random.Next(1, 30)) (DateTimeOffset.Now.AddDays(float (random.Next(1, 15))))
        task
        |> AppendData [| data |]
        |> Option.Some
    
    let Create() = new Filter<_, _>(filter)

module GoogleBingAggregator = 
    let transform task = task.Id
    let merge t1 t2 = t1 |> Analysis.AppendData t2.Data
    let Create(bag) = new Aggregator<Analysis.Task, Guid>(bag, transform, merge)
