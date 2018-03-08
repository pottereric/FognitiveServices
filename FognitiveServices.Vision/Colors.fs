namespace FognitiveServices.Vision

open Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models
open System

module Colors = 

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
            if colorType.HasValue then
                match colorType.Value with
                | true -> BlackAndWhite
                | false -> Color
            else
                Unknown

        {Foreground = analysis.Color.DominantColorForeground;
         Background = analysis.Color.DominantColorBackground;
         Colors = analysis.Color.DominantColors |> List.ofSeq;
         Accent = analysis.Color.AccentColor;
         Type = makeType analysis.Color.IsBWImg
        }

        


