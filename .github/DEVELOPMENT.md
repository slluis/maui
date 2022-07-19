# Development Guide

This page contains steps to build and run the .NET MAUI repository from source. If you are looking to build apps with .NET MAUI please head over to the links in the [README](https://github.com/dotnet/maui/blob/main/README.md) to get started.

## Requirements

### .NET 6 SDK

In most cases, when you have Visual Studio installed with the .NET workloads checked, these steps are not required.

1. Install the latest .NET 6:  
   <!--- [Win (x64)](https://aka.ms/dotnet/6.0.2xx/daily/dotnet-sdk-win-x64.exe)   -->
   - [Install the latest Public Preview of Visual Studio](https://docs.microsoft.com/en-us/dotnet/maui/get-started/installation/)
   - [macOS](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)  
2. If you're on a Windows development machine, install [SDK 20348](https://go.microsoft.com/fwlink/?linkid=2164145)
3. If you're on a MacOS development machine, install [PowerShell](https://docs.microsoft.com/powershell/scripting/install/installing-powershell-on-macos)
   
### iOS / MacCatalyst

iOS and MacCatalyst will require Xcode 13.3 Stable. You can get this [here](https://developer.apple.com/download/more/?name=Xcode).

### Android

Android API-31 (Android 12) is now the default in .NET 6.

## Running

### Compile using a local `bin\dotnet`

This method ensures that the workloads installed by Visual Studio won't get changed. This is usually the best method to use if you want to preserve the global state of your machine. This method will also use the versions that are specific to the branch you are on which is a good way to ensure compatibility.

#### Cake

You can run a `Cake` target to bootstrap .NET 6 in `bin\dotnet` and launch Visual Studio:

```dotnetcli
dotnet tool restore
dotnet cake --target=VS
```

#### Testing branch against your project
`--sln=<Path to SLN>`
- This will pack .NET and then open a VS instance using the local pack. This is useful if you want to check to see if the changes in a branch will address your particular issues. Pack only runs the first time so you will need to explicitly add the `--pack` flag if you make changes and need to repack.

```dotnetcli
dotnet tool restore
dotnet cake --sln="<download_directory>\MauiApp2\MauiApp2.sln" --target=VS
```

#### Pack
`--pack`
- This creates .NET MAUI packs inside the local dotnet install. This lets you use the CLI commands with the local dotnet to create/deploy with any changes that have been made on that branch (including template changes).

```dotnetcli
dotnet tool restore
dotnet cake --target=VS --pack --sln="<download_directory>\MauiApp2\MauiApp2.sln"
```

Create new .NET MAUI app using your new packs
```dotnetcli
dotnet tool restore
dotnet cake --pack
mkdir MyMauiApp
cd MyMauiApp
..\bin\dotnet\dotnet new maui
..\bin\dotnet\dotnet build -t:Run -f net6.0-android
```

You can also run commands individually:
```dotnetcli
# install local tools required to build (cake, pwsh, etc..)
dotnet tool restore
# Provision .NET 6 in bin\dotnet
dotnet build src\DotNet\DotNet.csproj
# Builds Maui MSBuild tasks
.\bin\dotnet\dotnet build Microsoft.Maui.BuildTasks.slnf
# Builds the rest of Maui
.\bin\dotnet\dotnet build Microsoft.Maui.sln
# Launch Visual Studio
dotnet cake --target=VS
```

### Compile with globally installed `dotnet`

- Try this first. This will build using the workloads installed by VS. If you receive a build failure related to workloads we recommend using a [local dotnet build](https://github.com/dotnet/maui/blob/main/.github/DEVELOPMENT.md#compile-using-a-local-bindotnet). If you want to keep on this path, proceed to the next step and then try to run these commands again.

```dotnetcli
dotnet tool restore
dotnet cake --target=VS --workloads=global
```

- If you need/want to update your global workloads to the latest workloads. 

> **Warning**
> This will replace what Visual Studio has installed for your workloads so now your entire machine will be using the workloads you have installed here.

#### main branch

> You'll probably need to run these commands with elevated privileges.

> **Warning**
> This is going to contain the "stable" versions of the packages, so you will have to clear the NuGet cache when this feed changes and when .NET ships. The various `darc-pub-dotnet-*` feeds are temporary and are generated on various builds. These feeds may disappear and be replaced with new ones as new builds come out. Make sure to verify that you are on the latest here and clear the nuget cache if it changes.
> ```
> dotnet nuget locals all --clear
> ```

Windows:

```bat
dotnet workload install maui `
  --from-rollback-file https://aka.ms/dotnet/maui/net6.0.json `
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/darc-pub-dotnet-runtime-a21b9a2d/nuget/v3/index.json `
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/darc-pub-dotnet-emsdk-52e9452f-3/nuget/v3/index.json `
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet6/nuget/v3/index.json `
  --source https://api.nuget.org/v3/index.json
```

MacOS:

```bash
dotnet workload install maui \
  --from-rollback-file https://aka.ms/dotnet/maui/net6.0.json \
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/darc-pub-dotnet-runtime-a21b9a2d/nuget/v3/index.json \
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/darc-pub-dotnet-emsdk-52e9452f-3/nuget/v3/index.json \
  --source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet6/nuget/v3/index.json \
  --source https://api.nuget.org/v3/index.json
```


#### MacOS

All of the above cake commands should work fine on `MacOS`.

If you aren't using the cake scripts and the `Microsoft.Maui-mac.slnf` isn't working for you try `_omnisharp.sln`

### Additional Cake Commands

#### Clean
`--clean`
- This will do a recursive delete of all your obj/bin folders. This is helpful if for some reason your repository is in a bad state and you don't want to go as scorched earth as `git clean -xdf`

### Blazor Desktop

To build and run Blazor Desktop samples, check out the [Blazor Desktop](https://github.com/dotnet/maui/wiki/Blazor-Desktop) wiki topic.

### Android

To workaround a performance issue, all `Resource.designer.cs`
generation is disabled for class libraries in this repo.

If you need to add a new `@(AndroidResource)` value to be used from C#
code in .NET MAUI:

1. Comment out the `<PropertyGroup>` in `Directory.Build.targets` that
   sets `$(AndroidGenerateResourceDesigner)` and
   `$(AndroidUseIntermediateDesignerFile)` to `false`.

2. Build .NET MAUI as you normally would. You will get compiler errors
   about duplicate fields, but `obj\Debug\net6.0-android\Resource.designer.cs`
   should now be generated.

3. Open `obj\Debug\net6.0-android\Resource.designer.cs`, and find the
   field you need such as:

```csharp
// aapt resource value: 0x7F010000
public static int foo = 2130771968;
```

4. Copy this field to the `Resource.designer.cs` checked into source
   control, such as: `src\Controls\src\Core\Platform\Android\Resource.designer.cs`

5. Restore the commented code in `Directory.Build.targets`.

## What branch should I use?

- net7.0
  - I want to use the net7.0 sdk and make changes that will be released with the .NET 7 release of MAUI
- net6.0
  - This PR seems like it should go out with a net6.0 service release
- main (start here if you don't know what to use)
  - I want to use the net6.0 sdk and make changes that will be released with the .NET 7 release of MAUI

## Repository projects

### Samples
 ```
├── Controls 
│   ├── samples
│   │   ├── Maui.Controls.Sample
│   │   ├── Maui.Controls.Sample.Sandbox
├── Essentials 
│   ├── samples
│   │   ├── Essentials.Sample
├── BlazorWebView 
│   ├── samples
│   │   ├── BlazorWinFormsApp
│   │   ├── BlazorWpfApp
```

- *Maui.Controls.Sample*: Full gallery sample with all of the controls and features of .NET MAUI
- *Maui.Controls.Sample.Sandbox*: Empty project useful for testing reproductions or use cases
- *Essentials.Sample*: Full gallery demonstrating  the library previously known as essentials. These are all the non UI related MAUI APIs.

### Device Test Projects

These are tests that will run on an actual device

 ```
├── Controls 
│   ├── test
│   │   ├── Controls.DeviceTests
├── Core 
│   ├── test
│   │   ├── Core.DeviceTests
├── Essentials 
│   ├── test
│   │   ├── Essentials.DeviceTests
├── BlazorWebView 
│   ├── test
│   │   ├── MauiBlazorWebView.DeviceTests
```

- *Controls.DeviceTests*: .NET MAUI Controls Visual Runner for running device based xunit tests. This is useful for tests that require XAML features
- *Core.DeviceTests*: .NET MAUI Core Visual Runner for running device based xunit tests. This is for tests that don't require any MAUI Controls based features
- *Essentials.DeviceTests*: Visual Runner running all the .NET MAUI essentials xunit tests.
- *MauiBlazorWebView.DeviceTests*: Visual Runner for BlazorWebView tests. 

### Unit Test Projects

These are tests that will not run on a device. This is useful for testing device independent logic.

 ```
├── Controls 
│   ├── test
│   │   ├── Controls.Core.UnitTests
├── Core 
│   ├── test
│   │   ├── Core.UnitTests
├── Essentials 
│   ├── test
│   │   ├── Essentials.UnitTests
```

## Stats

<img src="https://repobeats.axiom.co/api/embed/f917a77cbbdeee19b87fa1f2f932895d1df18b71.svg" />
