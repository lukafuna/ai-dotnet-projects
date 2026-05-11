# .NET AI Experiments

A collection of simple .NET projects exploring AI integrations using Ollama, OpenAI, and Semantic Kernel.

## Projects

### ChatGPT
Basic OpenAI API integration using the `gpt-3.5-turbo-instruct` model.
- Sends prompts and receives completions
- Uses .NET User Secrets for API key management

### OllamaBasic
Local AI model experiments using Ollama and Semantic Kernel.
- Runs models locally (no internet required)
- Includes cosine similarity and embedding generation
- Uses `llama3.2` and `all-minilm` models

### SemanticSearch
Semantic search implementation using embeddings.
- Vector similarity search
- Embedding generation with Ollama
- Uses `all-minilm` model for embeddings

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Ollama](https://ollama.com) (for local models)

## Getting Started

### 1. Clone the repo
```bash
git clone https://github.com/yourusername/dotnet-ai-experiments.git
cd dotnet-ai-experiments
```

### 2. Pull required Ollama models
```bash
ollama pull llama3.2
ollama pull all-minilm
```

### 3. Set up OpenAI API key (ChatGPT project only)
```bash
cd ChatGPT
dotnet user-secrets set "OpenAI:ApiKey" "your-api-key-here"
```

### 4. Run a project
```bash
dotnet run --project ChatGPT
dotnet run --project OllamaBasic
dotnet run --project SemanticSearch
```
