open System
open System.Linq
open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models
open FognitiveServices
open System.Threading

let printResults (results : ImageAnalysis ) =
    printfn "Description: %s" (results.Description.Captions.FirstOrDefault().Text)

    results.Tags |> Seq.iter (fun t -> printfn "%s %f" t.Name t.Confidence.Value)

    printfn "colors:"
    results.Color.DominantColors |> Seq.iter (fun c -> printfn "%s" c)


[<EntryPoint>]
let main argv =
    let op = async {
         use client = Vision.createClient "<Your-Subscription-Key>" AzureRegions.Southcentralus
         let! tags = Vision.getInfoForImage client @"<Your-Image-Path>"
         printResults tags
    }
    
    Async.StartWithContinuations(op, ignore, (fun e -> printfn "%A" e), ignore, CancellationToken.None)
    
    Console.ReadKey() |> ignore
    0 // return an integer exit code
