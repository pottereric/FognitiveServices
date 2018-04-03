namespace FognitiveServices.Vision

open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models

module Tags = 
    type Tag = { Name : string ; Confidence : float option }

    let get (analysis : ImageAnalysis) =
        let makeTag (t : ImageTag) =
            match t.Name with
            | null -> None
            | name -> Some { Name = name ; Confidence = Option.ofNullable t.Confidence }
        
        match analysis.Tags with
        | null -> []
        | t -> 
            t
            |> Seq.choose makeTag
            |> List.ofSeq

