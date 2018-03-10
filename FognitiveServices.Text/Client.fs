namespace FognitiveServices.Text

module Client =
    open Microsoft.Azure.CognitiveServices.Language.TextAnalytics
    open Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models

    let create key region =
        use client = new TextAnalyticsAPI()
        client.AzureRegion <- region
        client.SubscriptionKey <- key
        client

    let getTags (client: TextAnalyticsAPI) inputs =
        client.KeyPhrases(new MultiLanguageBatchInput(inputs));

    let detectLanguage (client: TextAnalyticsAPI) inputs =
        client.DetectLanguage(new BatchInput(inputs))

    let sentimentAnalysis (client: TextAnalyticsAPI) inputs =
        client.Sentiment(new MultiLanguageBatchInput(inputs))
