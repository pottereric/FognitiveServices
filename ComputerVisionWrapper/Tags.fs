namespace FognitiveServices.Vision

open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models

module Tags = 
    type Tag = { Name : string ; Confidence : float option }

    let get (analysis : ImageAnalysis) =
        let makeTag (t : ImageTag) =
            match t.Name, t.Confidence.HasValue with
            | null, _ -> None
            | name, true -> Some { Name = name ; Confidence = Some t.Confidence.Value }
            | name, false -> Some { Name = name ; Confidence = None }

        match analysis.Tags with
        | null -> []
        | t -> 
            t
            |> Seq.choose makeTag
            |> List.ofSeq

