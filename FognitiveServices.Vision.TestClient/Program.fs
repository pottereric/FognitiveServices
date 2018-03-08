open System
open System.Linq
open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models
open FognitiveServices.Vision
open System.Threading

let subscriptionKey = "<Your-Subscription-Key>" 
let region = AzureRegions.Southcentralus
let testFileName = @"c:\i\dog.jpg"

let printResults (analysis : ImageAnalysis) =
    printfn "Description: %s" (analysis.Description.Captions.FirstOrDefault().Text)

    printfn "tags:"
    Tags.get analysis
    |> Seq.iter (fun t -> printfn "%s %f" t.Name (Option.defaultValue -1.0 t.Confidence))

    printfn "colors:"
    let colors = Colors.get analysis
    colors.Colors |> Seq.iter (fun c -> printfn "%s" c)


[<EntryPoint>]
let main argv =
    let op = async {
         use client = Client.create subscriptionKey region
         let! analysis = Client.analyzeImageAsync client testFileName
         printResults analysis 
    }
    
    Async.StartWithContinuations(op, ignore, (fun e -> printfn "%A" e), ignore, CancellationToken.None)
    
    Console.ReadKey() |> ignore
    0 // return an integer exit code
