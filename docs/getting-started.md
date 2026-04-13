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

---

## 5) 📦 Host the MCP server on Azure Container Apps with Azure Container Registry

Use this flow for the `MAF.Tools.MCPServer` sample when you want a hosted MCP endpoint for `MAF.Tools.MCP`.

### 5.1) Build and push the image to ACR

```bash
cd src/MAF

# Set your names
export RESOURCE_GROUP="<your-resource-group>"
export LOCATION="<your-location>"
export ACR_NAME="<your-acr-name>"
export IMAGE_NAME="maf-tools-mcpserver"
export IMAGE_TAG="v1"

az group create --name "$RESOURCE_GROUP" --location "$LOCATION"
az acr create --resource-group "$RESOURCE_GROUP" --name "$ACR_NAME" --sku Basic
az acr build \
  --registry "$ACR_NAME" \
  --image "$IMAGE_NAME:$IMAGE_TAG" \
  --file MAF.Tools.MCPServer/Dockerfile \
  .
```

### 5.2) Deploy from ACR to Azure Container Apps

```bash
export ACA_ENV_NAME="<your-container-apps-environment>"
export ACA_APP_NAME="<your-container-app-name>"

az containerapp env create \
  --name "$ACA_ENV_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --location "$LOCATION"

az containerapp create \
  --name "$ACA_APP_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --environment "$ACA_ENV_NAME" \
  --image "$ACR_NAME.azurecr.io/$IMAGE_NAME:$IMAGE_TAG" \
  --target-port 80 \
  --ingress external \
  --registry-server "$ACR_NAME.azurecr.io"
```

### 5.3) Configure the MCP client sample

Get the Container App URL and set `McpEndpoint`:

```bash
export ACA_FQDN=$(az containerapp show \
  --name "$ACA_APP_NAME" \
  --resource-group "$RESOURCE_GROUP" \
  --query properties.configuration.ingress.fqdn -o tsv)

export McpEndpoint="https://$ACA_FQDN/mcp"
```

Then run:

```bash
cd src/MAF
dotnet run --project MAF.Tools.MCP/MAF.Tools.MCP.csproj
```

Helpful docs:
- [Azure Container Registry overview](https://learn.microsoft.com/azure/container-registry/container-registry-intro)
- [Quickstart: Build and push image with ACR Tasks](https://learn.microsoft.com/azure/container-registry/container-registry-quickstart-task-cli)
- [Azure Container Apps overview](https://learn.microsoft.com/azure/container-apps/overview)
- [Deploy containers from ACR to Azure Container Apps](https://learn.microsoft.com/azure/container-apps/managed-identity-image-pull)
