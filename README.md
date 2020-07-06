# Be.Vlaanderen.Basisregisters.Generators.Deterministic [![Build Status](https://github.com/Informatievlaanderen/deterministic-guid-generator/workflows/CI/badge.svg)](https://github.com/Informatievlaanderen/deterministic-guid-generator/actions)

## Goal

Create a deterministic GUID based on a namespace, a string and an optional version.

Although a new GUID is typically created in order to provide a unique ID, there are occasions when it’s useful for two different systems to generate the same GUID independently. [RFC 4122](https://www.ietf.org/rfc/rfc4122.txt) provides an algorithm for deterministic creation of a GUID based on a namespace ID (itself a GUID) and a name within that namespace. These name-based GUIDs will never collide with GUIDs from other sources (e.g., [Guid.NewGuid](https://docs.microsoft.com/en-us/dotnet/api/system.guid.newguid?redirectedfrom=MSDN&view=netcore-3.1#System_Guid_NewGuid)), and have a very (very) small chance of colliding with other name-based GUIDs. As per section 4.3:

* The UUIDs generated at different times from the same name in the same namespace MUST be equal.
* The UUIDs generated from two different names in the same namespace should be different (with very high probability).
* The UUIDs generated from the same name in two different namespaces should be different with (very high probability).
* If two UUIDs that were generated from names are equal, then they were generated from the same name in the same namespace (with very high probability).

Because .NET doesn’t provide a way to create these GUIDs, it’s tempting to create a custom solution (e.g., using a MD5 hash as a GUID, because it has the same number of bytes), but because that doesn’t follow the rules of GUID creation, it’s not guaranteed to be unique with respect to other GUIDs.

## Usage

Determine a namespace for your use case, this is just a random GUID you can store somewhere.

Using `Deterministic.Create` you can now pass in the namespace and a value and it will always return the same GUID.

By default a version 5 GUID is created.

```csharp
var namespaceGuid = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
var value = "hello.example.com";
var deterministicGuid = Deterministic.Create(namespaceGuid, value);
```

A version 3 GUID uses MD5 to compute a hash of the namespace and name.

```csharp
var namespaceGuid = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
var value = "hello.example.com";
var version = 5;
var deterministicGuid = Deterministic.Create(namespaceGuid, value, version);
```

A version 5 GUID uses SHA1 to compute a hash of the namespace and name.

```csharp
var namespaceGuid = new Guid("6ba7b810-9dad-11d1-80b4-00c04fd430c8");
var value = "hello.example.com";
var version = 5;
var deterministicGuid = Deterministic.Create(namespaceGuid, value, version);
```

## License

[MIT License](https://choosealicense.com/licenses/mit/)

## Credits

### Languages & Frameworks

* [.NET Core](https://github.com/Microsoft/dotnet/blob/master/LICENSE) - [MIT](https://choosealicense.com/licenses/mit/)
* [.NET Core Runtime](https://github.com/dotnet/coreclr/blob/master/LICENSE.TXT) - _CoreCLR is the runtime for .NET Core. It includes the garbage collector, JIT compiler, primitive data types and low-level classes._ - [MIT](https://choosealicense.com/licenses/mit/)
* [.NET Core APIs](https://github.com/dotnet/corefx/blob/master/LICENSE.TXT) - _CoreFX is the foundational class libraries for .NET Core. It includes types for collections, file systems, console, JSON, XML, async and many others._ - [MIT](https://choosealicense.com/licenses/mit/)
* [.NET Core SDK](https://github.com/dotnet/sdk/blob/master/LICENSE.TXT) - _Core functionality needed to create .NET Core projects, that is shared between Visual Studio and CLI._ - [MIT](https://choosealicense.com/licenses/mit/)
* [Roslyn and C#](https://github.com/dotnet/roslyn/blob/master/License.txt) - _The Roslyn .NET compiler provides C# and Visual Basic languages with rich code analysis APIs._ - [Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)
* [F#](https://github.com/fsharp/fsharp/blob/master/LICENSE) - _The F# Compiler, Core Library & Tools_ - [MIT](https://choosealicense.com/licenses/mit/)
* [F# and .NET Core](https://github.com/dotnet/netcorecli-fsc/blob/master/LICENSE) - _F# and .NET Core SDK working together._ - [MIT](https://choosealicense.com/licenses/mit/)

### Libraries

* [Paket](https://fsprojects.github.io/Paket/license.html) - _A dependency manager for .NET with support for NuGet packages and Git repositories._ - [MIT](https://choosealicense.com/licenses/mit/)
* [FAKE](https://github.com/fsharp/FAKE/blob/release/next/License.txt) - _"FAKE - F# Make" is a cross platform build automation system._ - [MIT](https://choosealicense.com/licenses/mit/)
* [xUnit](https://github.com/xunit/xunit/blob/master/license.txt) - _xUnit.net is a free, open source, community-focused unit testing tool for the .NET Framework._ - [Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/)
* [Shouldly](https://github.com/shouldly/shouldly/blob/master/LICENSE.txt) - _Should testing for .NET - the way Asserting *Should* be!_ - [BSD](https://choosealicense.com/licenses/bsd-3-clause/)
* [Faithlife.Utility](https://github.com/Faithlife/FaithlifeUtility/blob/master/LICENSE) - _Common .NET utility code in use at Faithlife_ - [MIT](https://choosealicense.com/licenses/mit/)

### Tooling

* [npm](https://github.com/npm/cli/blob/latest/LICENSE) - _A package manager for JavaScript._ - [Artistic License 2.0](https://choosealicense.com/licenses/artistic-2.0/)
* [semantic-release](https://github.com/semantic-release/semantic-release/blob/master/LICENSE) - _Fully automated version management and package publishing._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/changelog](https://github.com/semantic-release/changelog/blob/master/LICENSE) - _Semantic-release plugin to create or update a changelog file._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/commit-analyzer](https://github.com/semantic-release/commit-analyzer/blob/master/LICENSE) - _Semantic-release plugin to analyze commits with conventional-changelog._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/exec](https://github.com/semantic-release/exec/blob/master/LICENSE) - _Semantic-release plugin to execute custom shell commands._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/git](https://github.com/semantic-release/git/blob/master/LICENSE) - _Semantic-release plugin to commit release assets to the project's git repository._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/github](https://github.com/semantic-release/github/blob/master/LICENSE) - _Semantic-release plugin to publish a GitHub release._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/npm](https://github.com/semantic-release/npm/blob/master/LICENSE) - _Semantic-release plugin to publish a npm package._ - [MIT](https://choosealicense.com/licenses/mit/)
* [semantic-release/release-notes-generator](https://github.com/semantic-release/release-notes-generator/blob/master/LICENSE) - _Semantic-release plugin to generate changelog content with conventional-changelog._ - [MIT](https://choosealicense.com/licenses/mit/)
* [commitlint](https://github.com/conventional-changelog/commitlint/blob/master/license.md) - _Lint commit messages._ - [MIT](https://choosealicense.com/licenses/mit/)
* [commitizen/cz-cli](https://github.com/commitizen/cz-cli/blob/master/LICENSE) - _The commitizen command line utility._ - [MIT](https://choosealicense.com/licenses/mit/)
* [commitizen/cz-conventional-changelog](https://github.com/commitizen/cz-conventional-changelog/blob/master/LICENSE) _A commitizen adapter for the angular preset of conventional-changelog._ - [MIT](https://choosealicense.com/licenses/mit/)
* [husky](https://github.com/typicode/husky/blob/master/LICENSE) - _Git hooks made easy._  - [MIT](https://choosealicense.com/licenses/mit/)

### Flemish Government Libraries

* [Be.Vlaanderen.Basisregisters.Build.Pipeline](https://github.com/informatievlaanderen/build-pipeline/blob/main/LICENSE) - _Contains generic files for all Basisregisters Vlaanderen pipelines._ - [MIT](https://choosealicense.com/licenses/mit/)
