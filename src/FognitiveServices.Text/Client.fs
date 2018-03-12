namespace FognitiveServices.Text

module Client =
    open Microsoft.Azure.CognitiveServices.Language.TextAnalytics
    open Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models

    let create key region =
        use client = new TextAnalyticsAPI()
        client.AzureRegion <- region
        client.SubscriptionKey <- key
        client

    //let getTags (client: TextAnalyticsAPI) inputs =
    //    client.KeyPhrases(new MultiLanguageBatchInput(inputs));

    let getTags (client: TextAnalyticsAPI) (inputs : (string * string * string) list) =
        let multiLanguageInputs = inputs |> Seq.map (fun i -> 
            let language, id, text = i
            MultiLanguageInput(language, id, text))
        let multiLanguageInputCollection = System.Collections.Generic.List(multiLanguageInputs)
        client.KeyPhrases(new MultiLanguageBatchInput(multiLanguageInputCollection));

    //let detectLanguage (client: TextAnalyticsAPI) inputs =
    //    client.DetectLanguage(new BatchInput(inputs))

    let detectLanguage (client: TextAnalyticsAPI) (inputs : (string * string ) list) =
        let multiLanguageInputs = inputs |> Seq.map (fun i -> 
            let id, text = i
            Input(id, text))
        let multiLanguageInputCollection = System.Collections.Generic.List(multiLanguageInputs)
        client.DetectLanguage(new BatchInput(multiLanguageInputCollection));

    //let sentimentAnalysis (client: TextAnalyticsAPI) inputs =
    //    client.Sentiment(new MultiLanguageBatchInput(inputs))

    let sentimentAnalysis (client: TextAnalyticsAPI) (inputs : (string * string * string) list) =
        let multiLanguageInputs = inputs |> Seq.map (fun i -> 
            let language, id, text = i
            MultiLanguageInput(language, id, text))
        let multiLanguageInputCollection = System.Collections.Generic.List(multiLanguageInputs)
        client.Sentiment(new MultiLanguageBatchInput(multiLanguageInputCollection));

