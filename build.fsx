#load "packages/Be.Vlaanderen.Basisregisters.Build.Pipeline/Content/build-generic.fsx"

open Fake
open ``Build-generic``

let assemblyVersionNumber = (sprintf "%s.0")
let nugetVersionNumber = (sprintf "%s")

let build = buildSolution assemblyVersionNumber
let publish = publishSolution assemblyVersionNumber
let pack = packSolution nugetVersionNumber

Target "Clean" (fun _ ->
  CleanDir buildDir
)

// Library ------------------------------------------------------------------------

Target "Lib_Build" (fun _ -> build "Be.Vlaanderen.Basisregisters.Generators.Guid.Deterministic")

Target "Lib_Test" (fun _ ->
  [
    "test" @@ "Be.Vlaanderen.Basisregisters.Generators.Guid.Deterministic.Tests" ]
  |> List.iter testWithXunit
)

Target "Lib_Publish" (fun _ -> publish "Be.Vlaanderen.Basisregisters.Generators.Guid.Deterministic")
Target "Lib_Pack" (fun _ -> pack "Be.Vlaanderen.Basisregisters.Generators.Guid.Deterministic")

// --------------------------------------------------------------------------------

Target "PublishLibrary" DoNothing
Target "PublishAll" DoNothing

Target "PackageMyGet" DoNothing
Target "PackageAll" DoNothing

// Publish ends up with artifacts in the build folder
"DotNetCli" ==> "Clean" ==> "Restore" ==> "Lib_Build" ==> "Lib_Test" ==> "Lib_Publish" ==> "PublishLibrary"
"PublishLibrary" ==> "PublishAll"

// Package ends up with local NuGet packages
"PublishLibrary" ==> "Lib_Pack" ==> "PackageMyGet"
"PackageMyGet" ==> "PackageAll"

RunTargetOrDefault "Lib_Build"
