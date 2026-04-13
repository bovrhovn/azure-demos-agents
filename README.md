# Azure Demos Agents

Sample .NET console apps that demonstrate agent scenarios with **Azure AI Foundry** and **Microsoft.Agents.AI**.

## What is in this repository

- `src/MAF/MAF.slnx` – solution with all samples
- `src/MAF/MAF.SimpleAgent` – basic single-turn agent run
- `src/MAF/MAF.Memory.SessionContext` – multi-turn conversation using a shared session
- `src/MAF/MAF.Memory.Compaction` – context compaction pipeline for longer chats
- `src/MAF/MAF.Memory.CustomChatProvider` – custom chat history provider pattern
- `src/MAF/MAF.Tools.Function` – function/tool calling from the agent
- `docs/` – detailed setup and sample walkthroughs

## Prerequisites

- .NET SDK `10.0`
- Azure subscription
- Azure AI Foundry project with a deployed model
- Azure login (for `DefaultAzureCredential`)

## Quick start

1. Go to solution folder:

   ```bash
   cd /home/runner/work/azure-demos-agents/azure-demos-agents/src/MAF
   ```

2. Sign in to Azure:

   ```bash
   az login
   ```

3. Set required environment variables:

   ```bash
   export ENDPOINT="<your-ai-foundry-project-endpoint>"
   export DEPLOYMENTNAME="<your-model-deployment-name>"
   ```

4. Run a sample:

   ```bash
   dotnet run --project MAF.SimpleAgent/MAF.SimpleAgent.csproj
   ```

## Samples summary

- Full setup guide: [`docs/getting-started.md`](docs/getting-started.md)
- Detailed sample descriptions and run commands: [`docs/samples.md`](docs/samples.md)

## Official sources

- Azure AI Foundry docs: https://learn.microsoft.com/azure/ai-foundry/
- Azure AI Foundry Agent Service overview: https://learn.microsoft.com/azure/ai-foundry/agents/overview
- Azure SDK for .NET (`Azure.AI.Projects`): https://learn.microsoft.com/dotnet/api/azure.ai.projects?view=azure-dotnet
- `DefaultAzureCredential` docs: https://learn.microsoft.com/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet
- Microsoft .NET + AI docs: https://learn.microsoft.com/dotnet/ai/
