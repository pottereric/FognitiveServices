open System

open Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models

let azureRegion = AzureRegions.Westcentralus


let TestKeyPhrase _ = 
    let inputs = [("en", "1",  "What is the most important phrase in this sentance?");
                    ("en", "2",  "This is the test text that I am analyzing")]
    let client = FognitiveServices.Text.Client.create Config.subscriptionKey azureRegion
    let results = FognitiveServices.Text.Client.getTags client inputs
    
    results.Documents |> Seq.iter (fun r -> r.KeyPhrases |> Seq.iter (fun kp -> printfn "%s %s" r.Id kp))

let TestLanguageDetection _ =
    let inputs = [("1", "This is a document written in English.");
                    ( "2", "Este es un document escrito en Español.") ]

    let client = FognitiveServices.Text.Client.create Config.subscriptionKey azureRegion
    let results = FognitiveServices.Text.Client.detectLanguage client inputs

    results.Documents |> Seq.iter (fun r -> printfn "%s %s" r.Id ((r.DetectedLanguages |> Seq.head).Name))

let TestSentimentAnalysis _ =

    let inputs = [("en", "0", "I had the best day of my life.");
                    ("en", "1", "This was a waste of my time. The speaker put me to sleep.");
                    ("es", "2", "No tengo dinero ni nada que dar...");
                    ("it", "3", "L'hotel veneziano era meraviglioso. È un bellissimo pezzo di architettura.")]

    let client = FognitiveServices.Text.Client.create Config.subscriptionKey azureRegion
    let results = FognitiveServices.Text.Client.sentimentAnalysis client inputs

    results.Documents |> Seq.iter (fun d -> printfn "%s %f" d.Id d.Score.Value)


[<EntryPoint>]
let main argv =
    TestKeyPhrase()
    TestLanguageDetection()
    TestSentimentAnalysis()

    System.Console.ReadKey() |> ignore
    0 // return an integer exit code
