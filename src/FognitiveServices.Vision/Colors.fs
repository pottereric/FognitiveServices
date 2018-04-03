namespace FognitiveServices.Vision

open System

module Colors = 
    open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models

    type BlackWhiteInfo =
        | Color
        | BlackAndWhite
        | Unknown

    type Color = { 
        Foreground : string 
        Background : string 
        Colors : string list
        Accent : string 
        Type : BlackWhiteInfo
    }

    let get (analysis : ImageAnalysis) =
        let makeType (colorType : Nullable<bool>) =
            
            match colorType |> Option.ofNullable  with
            | Some cto ->
                if cto then BlackAndWhite else Color
            | None ->
                Unknown

        {Foreground = analysis.Color.DominantColorForeground;
         Background = analysis.Color.DominantColorBackground;
         Colors = analysis.Color.DominantColors |> List.ofSeq;
         Accent = analysis.Color.AccentColor;
         Type = makeType analysis.Color.IsBWImg
        }

        


