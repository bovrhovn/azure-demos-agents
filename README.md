# 🤖 Azure Demos Agents

> Practical .NET console demos for building agents with **Azure AI Foundry** and **Microsoft.Agents.AI**.

---

## ✨ What’s inside

| Path | Purpose |
|---|---|
| `src/MAF/MAF.slnx` | Solution containing all samples |
| `src/MAF/MAF.SimpleAgent` | Basic single-turn agent run |
| `src/MAF/MAF.Memory.SessionContext` | Multi-turn chat with shared session memory |
| `src/MAF/MAF.Memory.Compaction` | Context compaction pipeline for longer chats |
| `src/MAF/MAF.Memory.CustomChatProvider` | Custom chat history provider patterns |
| `src/MAF/MAF.Tools.Function` | Function/tool calling from the agent |
| `docs/` | Setup and sample walkthroughs |

---

## ✅ Prerequisites

- .NET SDK `10.0`
- Azure subscription
- Azure AI Foundry project with a deployed model
- Azure login for `DefaultAzureCredential`

---

## 🚀 Quick start

1. Move to the solution directory

```bash
cd src/MAF
```

2. Sign in to Azure

```bash
az login
```

3. Set environment variables

```bash
export ENDPOINT="<your-ai-foundry-project-endpoint>"
export DEPLOYMENTNAME="<your-model-deployment-name>"
```

4. Run a sample

```bash
dotnet run --project MAF.SimpleAgent/MAF.SimpleAgent.csproj
```

---

## 📚 Documentation

- Start here: [`docs/getting-started.md`](docs/getting-started.md)
- Sample catalog: [`docs/samples.md`](docs/samples.md)

---

## 🔗 Official resources

- [Azure AI Foundry docs](https://learn.microsoft.com/azure/ai-foundry/)
- [Azure AI Foundry Agent Service overview](https://learn.microsoft.com/azure/ai-foundry/agents/overview)
- [Azure SDK for .NET (`Azure.AI.Projects`)](https://learn.microsoft.com/dotnet/api/azure.ai.projects?view=azure-dotnet)
- [`DefaultAzureCredential` docs](https://learn.microsoft.com/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet)
- [Microsoft .NET + AI docs](https://learn.microsoft.com/dotnet/ai/)
