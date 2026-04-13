# Getting started

## 1) Prepare Azure resources

1. Create or open an Azure AI Foundry project.
2. Deploy a chat-capable model in that project.
3. Capture:
   - Project endpoint (`ENDPOINT`)
   - Model deployment name (`DEPLOYMENTNAME`)

Use the official setup docs:
- https://learn.microsoft.com/azure/ai-foundry/
- https://learn.microsoft.com/azure/ai-foundry/agents/overview

## 2) Prepare local environment

```bash
cd /home/runner/work/azure-demos-agents/azure-demos-agents/src/MAF
az login
export ENDPOINT="<your-ai-foundry-project-endpoint>"
export DEPLOYMENTNAME="<your-model-deployment-name>"
```

## 3) Build the solution

```bash
dotnet build MAF.slnx
```

## 4) Run samples

See [`samples.md`](samples.md) for all commands and what each sample demonstrates.
