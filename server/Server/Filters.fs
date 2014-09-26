module Filters

open PipesAndFilters
open DomainModel


module SwearTermsFilter =
    let swearWords = [| "fuck"; "shit"; "damn" |]
    let filter (task:Analysis.Task) = 
        if (swearWords |> Seq.exists (fun x -> task.Request.Term.Contains x)  ) then
            None
        else
            Some(task)
    
    let Create() = new Filter<_,_>(filter)


// random data, later use service
open System
open Analysis
let random = Random()
 
module GoogleFilter =
    let filter (task:Analysis.Task) = 
      let data = Data (random.Next(1, 100)) (DateTimeOffset.Now.AddDays(float(random.Next(1,30))))
      task |> AppendData [| data |] |> Option.Some
       
    
    let Create() = new Filter<_,_>(filter) 

module BingFilter =
    let filter (task:Analysis.Task) = 
      let data = Data (random.Next(1, 30)) (DateTimeOffset.Now.AddDays(float(random.Next(1,15))))
      task |> AppendData [| data |] |> Option.Some
    
    let Create() = new Filter<_,_>(filter)


module GoogleBingAggregator =
    let transform (task: Analysis.Task) = task.Id
    let Create(bag) = new Aggregator<Analysis.Task, Guid>(bag, transform)
