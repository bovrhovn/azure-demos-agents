# 🧭 Getting Started

Use this guide to set up Azure resources, configure your local environment, and run the demos quickly.

---

## 1) ☁️ Prepare Azure resources

1. Create or open an Azure AI Foundry project.
2. Deploy a chat-capable model in that project.
3. Save:
   - Project endpoint as `ENDPOINT`
   - Deployment name as `DEPLOYMENTNAME`

Helpful docs:
- [Azure AI Foundry](https://learn.microsoft.com/azure/ai-foundry/)
- [Agent Service overview](https://learn.microsoft.com/azure/ai-foundry/agents/overview)

---

## 2) 💻 Prepare your local environment

```bash
cd src/MAF
az login
export ENDPOINT="<your-ai-foundry-project-endpoint>"
export DEPLOYMENTNAME="<your-model-deployment-name>"
```

---

## 3) 🏗️ Build the solution

```bash
dotnet build MAF.slnx
```

---

## 4) ▶️ Run the samples

See [`samples.md`](samples.md) for all run commands and scenario descriptions.
