using Microsoft.Extensions.Configuration;
using OpenAI.API;
using OpenAI.API.Completions;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var apiKey = config["OpenAI:ApiKey"];
var api = new OpenAIAPI(apiKey);

var completions = await api.Completions.CreateCompletionAsync(new CompletionRequest
{
    Model = "gpt-3.5-turbo-instruct",
    Prompt = "What is dependency injection?",
    MaxTokens = 100,
    Temperature = 0.7
});

var completionsResult = completions.Completions;

if (completionsResult is not null && completionsResult.Count > 0)
{
    var answer = completionsResult[0].Text;
    Console.WriteLine(answer);
}
