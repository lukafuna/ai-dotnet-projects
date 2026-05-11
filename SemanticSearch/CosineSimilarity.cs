using System.Numerics.Tensors;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;

var blogPostTitles = new[]
{
    "Debug and Test Multi-Environment Postgres Db in .NET with Aspire + Neon",
    "Simplifying Integration with the Adapter Pattern",
    "Getting Started with OpenTelemetry in .NET",
    "Saga Orchestration Pattern",
    ".NET 9 - New LINQ Methods",
    "HybridCache in ASP.NET Core - .NET 9",
    "Chain Responsibility Pattern",
    "Exploring C# 13",
    "Feature Flags in .NET 8 with Azure Feature Management",
    "Securing Secrets in .NET 8 with Azure Key Vault",
    "LINQ Performance Optimization Tips & Tricks",
    "Using Singleton in Multithreading in .NET",
    "How to create .NET Custom Guard Clause",
    "How to implement CQRS without MediatR",
    "4 Entity Framework Tips to improve performances",
    "REPR Pattern - For C# developers",
    "Refit - The .NET Rest API you should know about",
    "6 ways to elevate your 'clean' code",
    "Deep dive into Source Generators",
    "3 Tips to Elevate your Swagger UI",
    "Memory Caching in .NET",
    "Solving HttpClient Authentication with Delegating Handlers",
    "Strategy Design Pattern will help you refactor code",
    "How to implement API Key Authentication",
    "Live loading appsettings.json configuration file",
    "Retry Failed API calls with Polly",
    "How and why I create my own mapper (avoid Automapper)?",
    "The ServiceCollection Extension Pattern",
    "3 things you should know about Strings",
    "API Gateways - The secure bridge for exposing your API",
    "5 cool features in C# 12",
    "Allow specific users to access your API - Part 2",
    "Allow specific users to access your API - Part 1",
    "Response Compression in ASP.NET",
    "API Gateway with Ocelot",
    "Health Checks in .NET 8",
    "MediatR Pipeline Behavior",
    "Getting Started with PLINQ",
    "Get Started with GraphQL in .NET",
    "Better Error Handling with Result object",
    "Background Tasks in .NET 8",
    "Pre-Optimized EF Query Techniques 5 Steps to Success",
    "Improve EF Core Performance with Compiled Queries",
    "How do I implement a workflow using a .NET workflow engine?",
    "What is and why do you need API Versioning?",
    "Compile-time logging source generation for highly performant logging"
};


Kernel kernel = Kernel.CreateBuilder()
    .AddOllamaEmbeddingGenerator(
        endpoint: new Uri("http://127.0.0.1:11434"),
        modelId: "all-minilm")
    .Build();

var embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

Console.WriteLine("Generating embeddings for blog post titles...");
var candidateEmbeddings = await embeddingGenerator.GenerateAndZipAsync(blogPostTitles);
Console.WriteLine("Embeddings generated successfully.");

while (true)
{
    Console.WriteLine("\nEnter your query (or press Enter to exit):");
    var userInput = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(userInput))
    {
        break;
    }

    
    var userEmbedding = await embeddingGenerator.GenerateAsync(userInput);
    
    
    var topMatches = candidateEmbeddings
        .Select(candidate => new
        {
            Text = candidate.Value,
            Similarity = TensorPrimitives.CosineSimilarity(
                candidate.Embedding.Vector.Span, userEmbedding.Vector.Span)
        })
        .OrderByDescending(match => match.Similarity)
        .Take(3);

    Console.WriteLine("\nTop matching blog post titles:");
    foreach (var match in topMatches)
    {
        Console.WriteLine($"Similarity: {match.Similarity:F4} - {match.Text}");
    }
}



