# 🧪 Testing

This document explains how to run the unit tests included in the repository and what they cover.

---

## Test project location

```
tests/
└── MAF.Tests/
    ├── MAF.Tests.csproj
    ├── TaxMethodTests.cs
    ├── PersonInfoTests.cs
    ├── GuardrailHelperTests.cs
    └── DocumentSearchAdapterTests.cs
```

The test project targets `net10.0` and uses **xUnit** as the test framework.

---

## Running the tests

From the repository root:

```bash
cd tests/MAF.Tests
dotnet test
```

Or from anywhere using an explicit path:

```bash
dotnet test tests/MAF.Tests/MAF.Tests.csproj
```

To get a verbose summary:

```bash
dotnet test --logger "console;verbosity=normal"
```

> **No Azure credentials are required.** All tests exercise pure business logic and run fully offline.

---

## What is tested

| Test file | Covered class | Sample project |
|---|---|---|
| `TaxMethodTests.cs` | `MAF.Tools.Function.Helpers.TaxMethod` | `MAF.Tools.Function` |
| `PersonInfoTests.cs` | `MAF.SimpleAgent.StructuredApproach.PersonInfo` | `MAF.SimpleAgent.StructuredApproach` |
| `GuardrailHelperTests.cs` | `MAF.Middleware.Helpers.GuardrailHelper` | `MAF.Middleware` |
| `DocumentSearchAdapterTests.cs` | `MAF.Rag.TextSearchProvider.Helpers.DocumentSearchAdapter` | `MAF.Rag.TextSearchProvider` |

### TaxMethod

Tests the `CalculateTaxMethod(customerName, months)` function which calculates a tax amount based on a base rate and number of months. Covers:

- Single month, multi-month, and zero-month scenarios
- Verifying that customer name has no effect on the calculated amount
- Data-driven `[Theory]` tests for multiple customer/month combinations

### PersonInfo

Tests the `PersonInfo` model used for structured agent output. Covers:

- Default null values on a new instance
- Setting and retrieving each property (`Name`, `Age`, `Occupation`)
- Nullable `Age` accepting `null`

### GuardrailHelper

Tests the content-filtering guardrail logic extracted from the middleware sample. Covers:

- Detecting blocked keywords (`password`, `secret`, `credentials`) case-insensitively
- Returning `null` for safe messages (including an empty string)
- `GetBlockedWords()` returning a non-empty list that includes all expected keywords

### DocumentSearchAdapter

Tests the mock document search back-end used in the RAG sample. Covers:

- Return/refund queries returning the Contoso policy result
- Unrelated queries returning an empty collection
- `null` query throwing `ArgumentNullException`
- Result records containing non-empty `SourceName`, `SourceLink`, and `Text`

---

## Useful references

- [xUnit documentation](https://xunit.net/)
- [.NET testing with xUnit](https://learn.microsoft.com/dotnet/core/testing/unit-testing-with-dotnet-test)
- [dotnet test CLI](https://learn.microsoft.com/dotnet/core/tools/dotnet-test)
- [Microsoft.NET.Test.Sdk on NuGet](https://www.nuget.org/packages/Microsoft.NET.Test.Sdk)
- [Azure AI Foundry Agent Service](https://learn.microsoft.com/azure/ai-foundry/agents/overview)
- [Microsoft.Agents.AI NuGet package](https://www.nuget.org/packages/Microsoft.Agents.AI.Foundry)
