open System
open System.Linq
open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models
open FognitiveServices.Vision
open System.Threading
open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models

let region = AzureRegions.Southcentralus
let testFileName = @"c:\i\both.jpg"

let printResults (analysis : ImageAnalysis) =
    let caption = analysis.Description.Captions.FirstOrDefault() |> Option.ofObj
    let captionText = match caption with
                        | None -> "no caption available"
                        | Some head -> head.Text

    printfn "Description: %s" captionText

    printfn "tags:"
    FognitiveServices.Vision.Tags.get analysis
    |> List.iter (fun t -> printfn "%s %f" t.Name (Option.defaultValue -1.0 t.Confidence))

    printfn "colors:"
    let colors = Colors.get analysis
    colors.Colors 
    |> List.iter (fun c -> printfn "%s" c)


[<EntryPoint>]
let main argv =
    let imageList = [@"c:\i\cat.jpg";@"c:\i\dog.jpg";@"c:\i\both.jpg"] 
    
    use client = Client.create Config.subscriptionKey region
    let analyizeImage = Client.analyzeImageAsync client

    imageList 
            |> List.map analyizeImage 
            |> Async.Parallel
            |> Async.RunSynchronously
            |> Array.iter printResults

    Console.ReadKey() |> ignore
    0 // return an integer exit code
