# 🧪 Samples

All samples live under `src/MAF`.

Most samples require:
- `ENDPOINT`
- `DEPLOYMENTNAME`

---

## 1) 🤖 MAF.SimpleAgent

- **Project:** `MAF.SimpleAgent/MAF.SimpleAgent.csproj`
- **Highlights:**
  - Basic `AIAgent` creation with `AIProjectClient`
  - Single question/answer run (`RunAsync`)
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.SimpleAgent/MAF.SimpleAgent.csproj
```

---

## 2) 🧱 MAF.SimpleAgent.StructuredApproach

- **Project:** `MAF.SimpleAgent.StructuredApproach/MAF.SimpleAgent.StructuredApproach.csproj`
- **Highlights:**
  - Strongly-typed structured response model (`PersonInfo`)
  - `RunAsync<PersonInfo>` output extraction
  - Simple prompt-to-object mapping pattern
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.SimpleAgent.StructuredApproach/MAF.SimpleAgent.StructuredApproach.csproj
```

---

## 3) 🧠 MAF.Memory.SessionContext

- **Project:** `MAF.Memory.SessionContext/MAF.Memory.SessionContext.csproj`
- **Highlights:**
  - Session creation (`CreateSessionAsync`)
  - Multi-turn chat loop with shared session context
  - Memory follow-up prompt (`"What do you remember about me?"`)
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.Memory.SessionContext/MAF.Memory.SessionContext.csproj
```

---

## 4) 🗜️ MAF.Memory.Compaction

- **Project:** `MAF.Memory.Compaction/MAF.Memory.Compaction.csproj`
- **Highlights:**
  - Pipeline-based compaction strategy
  - Tool-result, sliding-window, and truncation compaction triggers
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.Memory.Compaction/MAF.Memory.Compaction.csproj
```

---

## 5) 🧩 MAF.Memory.CustomChatProvider

- **Project:** `MAF.Memory.CustomChatProvider/MAF.Memory.CustomChatProvider.csproj`
- **Highlights:**
  - Custom `ChatHistoryProvider` (`SimpleInMemoryChatHistoryProvider`)
  - Session serialization example for persistence
  - Optional `VectorChatHistoryProvider` pattern for vector-backed storage
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.Memory.CustomChatProvider/MAF.Memory.CustomChatProvider.csproj
```

---

## 6) 🛠️ MAF.Tools.Function

- **Project:** `MAF.Tools.Function/MAF.Tools.Function.csproj`
- **Highlights:**
  - Tool/function registration via `AIFunctionFactory`
  - Sample function: `TaxMethod.CalculateTaxMethod`
  - Agent tool-calling usage pattern
- **Run:**

```bash
cd src/MAF
dotnet run --project MAF.Tools.Function/MAF.Tools.Function.csproj
```

---

## 7) 🔌 MAF.Tools.MCP

- **Project:** `MAF.Tools.MCP/MAF.Tools.MCP.csproj`
- **Requires additional env var:** `McpEndpoint`
- **Highlights:**
  - Connects to an MCP server over HTTP transport
  - Discovers tools dynamically via `ListToolsAsync`
  - Passes MCP tools to the agent for tool calling
- **Run:**

```bash
cd src/MAF
export McpEndpoint="http://localhost:8080/mcp"
dotnet run --project MAF.Tools.MCP/MAF.Tools.MCP.csproj
```

---

## 8) 🧰 MAF.Tools.MCPServer

- **Project:** `MAF.Tools.MCPServer/MAF.Tools.MCPServer.csproj`
- **Highlights:**
  - Minimal ASP.NET Core MCP server
  - MCP endpoint at `/mcp`
  - Health endpoint at `/health`
  - Includes Dockerfile for container deployment
- **Run locally:**

```bash
cd src/MAF
dotnet run --project MAF.Tools.MCPServer/MAF.Tools.MCPServer.csproj --urls http://localhost:8080
```

Then point the MCP client sample to:

```bash
export McpEndpoint="http://localhost:8080/mcp"
```

---

## 9) 🌐 MAF.Tools.WebSearch

- **Project:** `MAF.Tools.WebSearch/MAF.Tools.WebSearch.csproj`
- **Requires additional env var:** `BingConnectionId`
- **Highlights:**
  - Uses persistent agents client and Bing grounding tool
  - Citation-aware output formatting
  - Demonstrates web-grounded answers with source links
- **Run:**

```bash
cd src/MAF
export BingConnectionId="<your-bing-connection-id>"
dotnet run --project MAF.Tools.WebSearch/MAF.Tools.WebSearch.csproj
```

---

## 🔗 Useful references

- [Azure AI Foundry docs](https://learn.microsoft.com/azure/ai-foundry/)
- [Azure AI Foundry Agent Service](https://learn.microsoft.com/azure/ai-foundry/agents/overview)
- [Azure.AI.Projects API docs](https://learn.microsoft.com/dotnet/api/azure.ai.projects?view=azure-dotnet)
- [Microsoft .NET AI docs](https://learn.microsoft.com/dotnet/ai/)
- [Model Context Protocol docs](https://modelcontextprotocol.io/)
- [Azure Container Registry docs](https://learn.microsoft.com/azure/container-registry/)
- [Azure Container Apps docs](https://learn.microsoft.com/azure/container-apps/)
