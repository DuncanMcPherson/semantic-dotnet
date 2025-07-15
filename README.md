![NuGet Version](https://img.shields.io/nuget/v/SemanticRelease.Dotnet)
![NuGet Downloads](https://img.shields.io/nuget/dt/SemanticRelease.Dotnet)

# SemanticRelease.DotNet

Inspired by the Node.js [semantic-release](https://github.com/semantic-release/github) tool, this was designed to make versioning
and releasing .NET packages easy

## Overview

This plugin serves as the tool for running `dotnet` commands (`build`, `restore`, `pack`, `publish`) based on configured options.

## Features

- Configurable commands
- Automatic use of the updated version (if used after determining the next version)

## Requirements

- .NET Standard 2.1 or later
- C# 8.0 or later

## Usage

The base [semantic-release](https://nuget.org/packages/dotnet-semantic-release) tool handles all package resolution automatically 
(including downloading plugins from NuGet!) All you have to do is configure the plugin!

- Using the default configuration
```json
{
  // base configuration
  "pluginConfigs": [
    "SemanticRelease.DotNet"
  ]
}
```
- Using custom configuration
```json
{
  "pluginConfigs": [
    {
      "name": "SemanticRelease.DotNet",
      "options": {
        // Options should be provided here
      }
    }
  ]
}
```

### Options

| Name            | Description                                                                                  | Default Value | Required |
|-----------------|----------------------------------------------------------------------------------------------|---------------|----------|
| `build`         | Whether the plugin should build the project                                                  | `true`        | ❌        |
| `restore`       | Whether the plugin should restore NuGet packages                                             | `true`        | ❌        |
| `publish`       | Whether the plugin should try to publish publishable projects                                | `false`       | ❌        |
| `pack`          | Whether the plugin should try to pack projects into NuGet packages                           | `false`       | ❌        |
| `useSkipFlags`  | Injects `--no-build` and `--no-restore` into `dotnet` commands                               | `true`        | ❌        |
| `configuration` | Which configuration to use for `build`, `pack`, and `publish`                                | `null`        | ✅        |
| `outputDir`     | Where `pack` should put the associated package files                                         | `null`        | ❌        |
| `slnOrProject`  | The solution or project that you want to build. Glob patterns are not accepted at this time  | `null`        | ❌        |

**NOTE**: `outputDir` is **NOT** required and your artifacts will be placed in the appropriate location based on the `pack` command.
The default location is `bin/Release/<Framework>/` per the behavior of `dotnet pack`.

## License

This project is licensed under the MIT License—see the [LICENSE](LICENSE) file for more details