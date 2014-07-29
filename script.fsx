#I "./tools/"
#r "FSharp.Data.dll"
open System
open System.IO
open FSharp.Data
Environment.CurrentDirectory <- __SOURCE_DIRECTORY__




type TrainingLog = CsvProvider<"schema.csv">
let lastNight = TrainingLog.Load("2014-07-28.csv")

let ``volume of last night's squats`` = 
  lastNight.Rows 
  |> Seq.filter (fun r -> r.Exercise = "Squat")
  |> Seq.sumBy (fun r -> r.Weight)

let ``volume of last night's bench press`` = 
  lastNight.Rows 
  |> Seq.filter (fun r -> r.Exercise = "Bench Press")
  |> Seq.sumBy (fun r -> r.Weight)

let ``volume of last night's sumo deadlift`` = 
  lastNight.Rows 
  |> Seq.filter (fun r -> r.Exercise = "Sumo Deadlift")
  |> Seq.sumBy (fun r -> r.Weight)

Directory.GetFiles("./")
|> Seq.filter (fun s -> s.EndsWith(".csv"))
|> Seq.collect (fun s -> TrainingLog.Load(s).Rows |> Seq.map (fun a -> Path.GetFileNameWithoutExtension(s), a))
