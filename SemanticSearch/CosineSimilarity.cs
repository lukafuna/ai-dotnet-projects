using System.Numerics.Tensors;
using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;

var AIDotnetTitles  = new[]
{
    "Getting Started with Semantic Kernel in .NET",
    "Building a Local AI Chatbot with Ollama and C#",
    "Vector Databases Explained for .NET Developers",
    "Semantic Search with Embeddings in .NET",
    "RAG (Retrieval Augmented Generation) in .NET",
    "Fine-tuning vs Prompt Engineering - What to Choose?",
    "Building AI Agents with Semantic Kernel",
    "How to Use OpenAI API in .NET",
    "Understanding Tokens and Context Windows",
    "Cosine Similarity and Vector Search in C#",
    "Local AI Models with Ollama and .NET",
    "Prompt Engineering Best Practices for .NET Developers",
    "Building a Document Q&A App with RAG in .NET",
    "AI Memory and Conversation History in Semantic Kernel",
    "Streaming AI Responses in ASP.NET Core",
    "Function Calling with OpenAI in .NET",
    "How Quantization Makes AI Models Run Locally",
    "Microsoft.Extensions.AI - The New AI Abstraction Layer",
    "Comparing Embedding Models for Semantic Search",
    "Building a Semantic Cache with Vector Search",
    "Multi-Modal AI - Using Images with GPT-4 in .NET",
    "AI Observability with OpenTelemetry in .NET",
    "Rate Limiting AI API Calls with Polly",
    "Secure Your AI API Keys in .NET with User Secrets",
    "Background AI Processing with .NET Worker Services",
    "Building an AI Plugin for Semantic Kernel",
    "Chain of Thought Prompting in .NET Applications",
    "Few-Shot Learning with OpenAI in C#",
    "AI Gateway Pattern for .NET Microservices",
    "Caching AI Responses for Better Performance",
    "Testing AI Applications in .NET",
    "Building a Code Review Bot with Semantic Kernel",
    "How to Implement AI Feature Flags in .NET",
    "Structured Output with OpenAI in .NET",
    "Building a Local Copilot with Ollama and Semantic Kernel",
    "AI Health Checks in ASP.NET Core",
    "Versioning AI Prompts in .NET Applications",
    "CQRS Pattern for AI Request Handling in .NET",
    "GraphQL API for AI Services in .NET",
    "Error Handling Strategies for AI APIs in .NET",
    "Logging and Monitoring AI Calls in .NET",
    "Dependency Injection for AI Services in .NET",
    "Building a Text Classification API with .NET and Ollama",
    "How to Choose Between GPT-4 and Local Models",
    "AI Result Pattern - Handling Failures Gracefully",
    "Getting Started with Microsoft Copilot Extensions"
};


Kernel kernel = Kernel.CreateBuilder()
    .AddOllamaEmbeddingGenerator(
        endpoint: new Uri("http://127.0.0.1:11434"),
        modelId: "all-minilm")
    .Build();

var embeddingGenerator = kernel.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

Console.WriteLine("Generating embeddings for blog post titles...");
var candidateEmbeddings = await embeddingGenerator.GenerateAndZipAsync(AIDotnetTitles);
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



