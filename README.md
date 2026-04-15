# 🤖 Azure Demos Agents

> Practical .NET console demos for building agents with **Azure AI Foundry** and **Microsoft.Agents.AI**.

---

## ✨ What’s inside

| Path | Purpose |
|---|---|
| `src/MAF/MAF.slnx` | Solution containing all samples |
| `src/MAF/MAF.SimpleAgent` | Basic single-turn agent run |
| `src/MAF/MAF.SimpleAgent.StructuredApproach` | Structured-output agent response pattern |
| `src/MAF/MAF.SimpleAgent.BackgroundResponses` | Background/async response polling pattern |
| `src/MAF/MAF.Memory.SessionContext` | Multi-turn chat with shared session memory |
| `src/MAF/MAF.Memory.Compaction` | Context compaction pipeline for longer chats |
| `src/MAF/MAF.Memory.CustomChatProvider` | Custom chat history provider patterns |
| `src/MAF/MAF.Tools.Function` | Function/tool calling from the agent |
| `src/MAF/MAF.Tools.MCP` | Agent tool-calling through an MCP endpoint |
| `src/MAF/MAF.Tools.MCPServer` | Standalone MCP server sample (HTTP + container-ready) |
| `src/MAF/MAF.Tools.WebSearch` | Agent with Bing grounding/web search tool |
| `src/MAF/MAF.Middleware` | Agent middleware / content-filtering guardrail pattern |
| `src/MAF/MAF.Rag.TextSearchProvider` | RAG pattern using a text search context provider |
| `src/MAF/MAF.Skills.FileBased` | File-based skill discovery with `AgentSkillsProvider` |
| `src/MAF/MAF.Skills.InlineSkill` | Inline skill definition with dynamic resources |
| `src/MAF/MAF.DevUI` | Multi-agent web host with DevUI, workflows, and OpenAI API surface |
| `docs/` | Setup and sample walkthroughs |
| `tests/MAF.Tests` | Unit tests for business logic across the samples |

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

## 🧪 Tests

Unit tests live in `tests/MAF.Tests` and cover the testable business logic extracted from the samples.

```bash
cd tests/MAF.Tests
dotnet test
```

See [`docs/testing.md`](docs/testing.md) for details on what is tested and how to run tests locally.

---

## 📚 Documentation

- Start here: [`docs/getting-started.md`](docs/getting-started.md)
- Sample catalog: [`docs/samples.md`](docs/samples.md)
- Testing guide: [`docs/testing.md`](docs/testing.md)

---

## 🔗 Official resources

- [Azure AI Foundry docs](https://learn.microsoft.com/azure/ai-foundry/)
- [Azure AI Foundry Agent Service overview](https://learn.microsoft.com/azure/ai-foundry/agents/overview)
- [Azure SDK for .NET (`Azure.AI.Projects`)](https://learn.microsoft.com/dotnet/api/azure.ai.projects?view=azure-dotnet)
- [`DefaultAzureCredential` docs](https://learn.microsoft.com/dotnet/api/azure.identity.defaultazurecredential?view=azure-dotnet)
- [Microsoft .NET + AI docs](https://learn.microsoft.com/dotnet/ai/)
- [Azure Container Registry docs](https://learn.microsoft.com/azure/container-registry/)
- [Azure Container Apps docs](https://learn.microsoft.com/azure/container-apps/)
