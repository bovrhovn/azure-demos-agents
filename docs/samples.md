# Samples

All samples are in `src/MAF` and require `ENDPOINT` and `DEPLOYMENTNAME` environment variables.

## 1) MAF.SimpleAgent

- Project: `MAF.SimpleAgent/MAF.SimpleAgent.csproj`
- What it contains:
  - Basic `AIAgent` creation with `AIProjectClient`
  - Single question/answer run (`RunAsync`)
- Run:

```bash
cd src/MAF
dotnet run --project MAF.SimpleAgent/MAF.SimpleAgent.csproj
```

## 2) MAF.Memory.SessionContext

- Project: `MAF.Memory.SessionContext/MAF.Memory.SessionContext.csproj`
- What it contains:
  - Session creation (`CreateSessionAsync`)
  - Multi-turn chat loop sharing one session context
  - Follow-up memory check (`"What do you remember about me?"`)
- Run:

```bash
cd src/MAF
dotnet run --project MAF.Memory.SessionContext/MAF.Memory.SessionContext.csproj
```

## 3) MAF.Memory.Compaction

- Project: `MAF.Memory.Compaction/MAF.Memory.Compaction.csproj`
- What it contains:
  - Pipeline-based compaction strategy
  - Tool-result, sliding-window, and truncation compaction triggers
- Run:

```bash
cd src/MAF
dotnet run --project MAF.Memory.Compaction/MAF.Memory.Compaction.csproj
```

## 4) MAF.Memory.CustomChatProvider

- Project: `MAF.Memory.CustomChatProvider/MAF.Memory.CustomChatProvider.csproj`
- What it contains:
  - Custom `ChatHistoryProvider` implementation (`SimpleInMemoryChatHistoryProvider`)
  - Session serialization example for persistence
  - Extra `VectorChatHistoryProvider` example for vector-backed storage
- Run:

```bash
cd src/MAF
dotnet run --project MAF.Memory.CustomChatProvider/MAF.Memory.CustomChatProvider.csproj
```

## 5) MAF.Tools.Function

- Project: `MAF.Tools.Function/MAF.Tools.Function.csproj`
- What it contains:
  - Tool/function registration through `AIFunctionFactory`
  - Sample function: `TaxMethod.CalculateTaxMethod`
  - Agent tool-calling usage pattern
- Run:

```bash
cd src/MAF
dotnet run --project MAF.Tools.Function/MAF.Tools.Function.csproj
```

## Useful references

- Azure AI Foundry docs: https://learn.microsoft.com/azure/ai-foundry/
- Azure AI Foundry Agent Service: https://learn.microsoft.com/azure/ai-foundry/agents/overview
- Azure.AI.Projects API docs: https://learn.microsoft.com/dotnet/api/azure.ai.projects?view=azure-dotnet
- Microsoft .NET AI docs: https://learn.microsoft.com/dotnet/ai/
