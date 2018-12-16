# Be.Vlaanderen.Basisregisters.Generators.Deterministic

Create a deterministic GUID based on namespace Guid, a string and an optional version.

## Usage

```csharp
var namespaceGuid = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
var value = "hello.example.com";
var deterministicGuid = Deterministic.Create(namespaceGuid, value);
```

```csharp
var namespaceGuid = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
var value = "hello.example.com";
var version = 5;
var deterministicGuid = Deterministic.Create(namespaceGuid, value, version);
```
