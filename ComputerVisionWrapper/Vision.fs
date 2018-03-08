namespace FognitiveServices

module Vision =
    open Microsoft.Azure.CognitiveServices.Vision.ComputerVision
    open System.IO
    open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models
    
    let createClient apiKey region =
        let client = new ComputerVisionAPI( ApiKeyServiceClientCredentials(apiKey))
        client.AzureRegion <- region
        client

    let getInfoForImage (client : ComputerVisionAPI )imagePath =
        async {
            use stream = new FileStream(imagePath, FileMode.Open)

            let featureTypes = [|
                VisualFeatureTypes.Description
                VisualFeatureTypes.Categories
                VisualFeatureTypes.Color
                VisualFeatureTypes.Faces
                VisualFeatureTypes.ImageType
                VisualFeatureTypes.Tags
            |]

            // Analyze the image.
            return! client.AnalyzeImageInStreamAsync(stream, featureTypes) |> Async.AwaitTask
        }
