using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

Kernel kernel = Kernel.CreateBuilder()
    .AddOllamaChatCompletion(
        endpoint: new Uri("http://127.0.0.1:11434"),
        modelId: "llama3.2")
    .Build();

var chat = kernel.GetRequiredService<IChatCompletionService>();

var result = await chat.GetChatMessageContentAsync("What is semantic search?");

Console.WriteLine(result.Content);